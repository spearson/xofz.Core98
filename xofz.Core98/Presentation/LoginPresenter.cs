namespace xofz.Presentation
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.UI;
    using Timer = xofz.Framework.Timer;

    public class LoginPresenter : Presenter, IDisposable
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

        public virtual void Setup(
            int loginDurationMinutes)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.loginDurationMinutes = loginDurationMinutes;

            this.ui.LoginKeyTapped += this.ui_LoginKeyTapped;
            this.ui.CancelKeyTapped += this.Stop;
            this.ui.BackspaceKeyTapped += this.ui_BackspaceKeyTapped;
            UiHelpers.Write(
                this.ui, 
                () => this.ui.TimeRemaining = "Not logged in");

            var w = this.web;
            w.Run<Timer>(
                t => t.Elapsed += this.timer_Elapsed,
                "LoginTimer");
            w.Run<Navigator>(n => n.RegisterPresenter(this));
            w.Run<Timer>(t => t.Start(1000), "LoginTimer");
        }

        public override void Start()
        {
            var w = this.web;
            this.oldPassword = UiHelpers.Read(this.ui, () => this.ui.CurrentPassword);
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.Display();
            });
            this.ui.WriteFinished.WaitOne();
        }

        public override void Stop()
        {
            var w = this.web;

            UiHelpers.Write(this.ui, () =>
            {
                this.ui.CurrentPassword = this.oldPassword;
                this.ui.Hide();
            });
            this.ui.WriteFinished.WaitOne();

            w.Run<LatchHolder>(
                latch => latch.Latch.Set(),
                "LoginLatch");
        }

        public void Dispose()
        {
            this.web.Run<Timer>(t => t.Stop(), "LoginTimer");
            this.timerHandlerFinished.WaitOne();
        }

        private void ui_BackspaceKeyTapped()
        {
            UiHelpers.Write(this.ui,
                () => this.ui.CurrentPassword = StringHelpers.RemoveEndChars(this.ui.CurrentPassword, 1));
        }

        private void ui_LoginKeyTapped()
        {
            var password = UiHelpers.Read(
                this.ui, () => this.ui.CurrentPassword);
            this.web.Run<AccessController>(
                ac => ac.InputPassword(password));
            var cal = this.web.Run<AccessController, AccessLevel>(
                ac => ac.CurrentAccessLevel);

            if (cal == AccessLevel.None)
            {
                // show login failed message
                this.setOldPassword(string.Empty);
                return;
            }

            this.setOldPassword(password);
            this.setElapsedSeconds(0);

            this.Stop();
        }

        private void setElapsedSeconds(long elapsedSeconds)
        {
            this.elapsedSeconds = elapsedSeconds;
        }

        private void setOldPassword(string oldPassword)
        {
            this.oldPassword = oldPassword;
        }

        private void timer_Elapsed()
        {
            this.timerHandlerFinished.Reset();
            var elapsed = this.elapsedSeconds + 1;
            this.setElapsedSeconds(elapsed);
            var cal = this.web.Run<AccessController, AccessLevel>(
                ac => ac.CurrentAccessLevel);
            var duration = this.loginDurationMinutes * 60;
            UiHelpers.Write(this.ui,
                () =>
                {
                    this.ui.CurrentAccessLevel = cal;
                    this.ui.TimeRemaining = cal > AccessLevel.None
                        ? TimeSpan.FromSeconds(duration - elapsed).ToString()
                        : "Not logged in";
                });

            if (elapsed != duration)
            {
                this.timerHandlerFinished.Set();
                return;
            }

            UiHelpers.Write(this.ui, () => this.ui.CurrentPassword = string.Empty);
            this.ui.WriteFinished.WaitOne();
            this.setOldPassword(string.Empty);
            this.setElapsedSeconds(0);
            this.timerHandlerFinished.Set();
        }

        private int setupIf1;
        private int loginDurationMinutes;
        private long elapsedSeconds;
        private string oldPassword;
        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly ManualResetEvent timerHandlerFinished;
    }
}
