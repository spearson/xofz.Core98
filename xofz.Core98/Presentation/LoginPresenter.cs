﻿namespace xofz.Presentation
{
    using System.Text;
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;

    public class LoginPresenter : Presenter
    {
        public LoginPresenter(
            LoginUi ui,
            MethodWeb web)
            : base(ui, null)
        {
            this.ui = ui;
            this.web = web;
            this.timerHandlerFinished = new ManualResetEvent(true);
        }

        public virtual void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            var subscriberRegistered = false;
            w.Run<EventSubscriber>(subscriber =>
            {
                subscriberRegistered = true;
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.LoginKeyTapped),
                    this.ui_LoginKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.CancelKeyTapped),
                    this.Stop);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.BackspaceKeyTapped),
                    this.ui_BackspaceKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.KeyboardKeyTapped),
                    this.ui_KeyboardKeyTapped);
            });

            if (!subscriberRegistered)
            {
                this.ui.LoginKeyTapped
                    += this.ui_LoginKeyTapped;
                this.ui.CancelKeyTapped
                    += this.Stop;
                this.ui.BackspaceKeyTapped
                    += this.ui_BackspaceKeyTapped;
                this.ui.KeyboardKeyTapped
                    += this.ui_KeyboardKeyTapped;
            }

            UiHelpers.Write(
                this.ui,
                () =>
                {
                    this.ui.TimeRemaining = "Not logged in";
                    this.ui.KeyboardKeyVisible = false;
                });

            w.Run<KeyboardLoader>(loader =>
            {
                UiHelpers.Write(
                    this.ui,
                    () => { this.ui.KeyboardKeyVisible = true; });
            });

            w.Run<AccessController>(ac =>
            {
                var cal = ac.CurrentAccessLevel;
                UiHelpers.Write(
                    this.ui,
                    () => this.ui.CurrentAccessLevel = cal);
                if (subscriberRegistered)
                {
                    w.Run<EventSubscriber>(sub =>
                    {
                        sub.Subscribe<AccessLevel>(
                            ac,
                            nameof(ac.AccessLevelChanged),
                            this.accessLevelChanged);
                    });
                    return;
                }

                ac.AccessLevelChanged += this.accessLevelChanged;
            });

            w.Run<xofz.Framework.Timer>(t =>
                {
                    if (subscriberRegistered)
                    {
                        w.Run<EventSubscriber>(subscriber =>
                        {
                            subscriber.Subscribe(
                                t,
                                nameof(t.Elapsed),
                                this.timer_Elapsed);
                        });

                        goto start;
                    }

                    t.Elapsed += this.timer_Elapsed;

                    start:
                    t.Start(1000);
                },
                "LoginTimer");

            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return;
            }

            var w = this.web;
            this.currentPassword = UiHelpers.Read(
                this.ui,
                () => this.ui.CurrentPassword);
            UiHelpers.WriteSync(this.ui, this.ui.Display);
        }

        public override void Stop()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return;
            }

            var w = this.web;
            var cp = this.currentPassword;
            UiHelpers.WriteSync(this.ui, () =>
            {
                this.ui.CurrentPassword = cp;
                this.ui.Hide();
            });

            w.Run<LatchHolder>(
                latch => latch.Latch.Set(),
                "LoginLatch");
        }

        private void ui_BackspaceKeyTapped()
        {
            var cPw = UiHelpers.Read(
                this.ui,
                () => this.ui.CurrentPassword);
            var newPw = StringHelpers.RemoveEndChars(
                cPw,
                1);
            UiHelpers.Write(this.ui,
                () =>
                {
                    this.ui.CurrentPassword = newPw;
                    this.ui.FocusPassword();
                });
        }

        private void ui_LoginKeyTapped()
        {
            var password = UiHelpers.Read(
                this.ui, () => this.ui.CurrentPassword);
            var w = this.web;
            var newCal = AccessLevel.None;
            w.Run<AccessController, LoginSettings>(
                (ac, settings) =>
                {
                    var previousCal = ac.CurrentAccessLevel;
                    ac.InputPassword(
                        password,
                        settings.LoginDuration);
                    newCal = ac.CurrentAccessLevel;
                    if (previousCal == newCal)
                    {
                        w.Run<xofz.Framework.Timer, EventRaiser>(
                            (t, er) =>
                            {
                                er.Raise(t, nameof(t.Elapsed));
                            },
                            "LoginTimer");
                    }
                });

            if (newCal > AccessLevel.None)
            {
                this.setCurrentPassword(password);
                this.Stop();
                return;
            }

            UiHelpers.Write(
                this.ui,
                () => this.ui.FocusPassword());
        }

        private void setCurrentPassword(string oldPassword)
        {
            this.currentPassword = oldPassword;
        }

        private void timer_Elapsed()
        {
            var h = this.timerHandlerFinished;
            h.Reset();

            var w = this.web;
            w.Run<AccessController>(ac =>
            {
                var cal = ac.CurrentAccessLevel;
                string timeRemaining;
                if (cal > AccessLevel.None)
                {
                    var tr = ac.TimeRemaining;
                    var sb = new StringBuilder();
                    sb.Append((int)tr.TotalHours)
                        .Append(':')
                        .Append(tr
                            .Minutes
                            .ToString()
                            .PadLeft(2, '0'))
                        .Append(':')
                        .Append(tr
                            .Seconds
                            .ToString()
                            .PadLeft(2, '0'))
                        .Append('.')
                        .Append(tr
                            .Milliseconds
                            .ToString()
                            .PadLeft(3, '0'));
                    timeRemaining = sb.ToString();
                }
                else
                {
                    timeRemaining = "Not logged in";
                }

                var noAccess = cal == AccessLevel.None;
                if (noAccess)
                {
                    this.setCurrentPassword(null);
                }

                UiHelpers.Write(this.ui,
                    () =>
                    {
                        this.ui.CurrentAccessLevel = cal;
                        this.ui.TimeRemaining = timeRemaining;
                    });
            });

            h.Set();
        }

        private void accessLevelChanged(AccessLevel newAccessLevel)
        {
            var w = this.web;
            if (newAccessLevel == AccessLevel.None)
            {
                UiHelpers.Write(
                    this.ui,
                    () => this.ui.CurrentPassword = null);
            }

            w.Run<xofz.Framework.Timer, EventRaiser>(
                (t, er) =>
                {
                    er.Raise(
                        t,
                        nameof(t.Elapsed));
                },
                "LoginTimer");
        }

        private void ui_KeyboardKeyTapped()
        {
            var w = this.web;
            w.Run<KeyboardLoader>(loader => loader.Load());
            UiHelpers.Write(
                this.ui,
                () => this.ui.FocusPassword());
        }

        private long setupIf1;
        private string currentPassword;
        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly ManualResetEvent timerHandlerFinished;
    }
}
