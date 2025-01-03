﻿namespace xofz.Presentation.Presenters
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI.Login;

    public sealed class LoginPresenter
        : Presenter
    {
        public LoginPresenter(
            LoginUi ui,
            MethodRunner runner)
            : base(ui, null)
        {
            this.ui = ui;
            this.runner = runner;
        }

        /// <summary>
        /// Requires an EventSubscriber, SetupHandler, and a Navigator
        /// </summary>
        public void Setup()
        {
            const long one = 1;
            if (Interlocked.Exchange(
                    ref this.setupIf1,
                    one) == one)
            {
                return;
            }

            var r = this.runner;
            r?.Run<EventSubscriber>(subscriber =>
            {
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
                r.Run<AccessController>(ac =>
                {
                    subscriber.Subscribe<AccessLevel>(
                        ac,
                        nameof(ac.AccessLevelChanged),
                        this.accessLevelChanged);
                });
                r.Run<xofz.Framework.Timer>(t =>
                    {
                        subscriber.Subscribe(
                            t,
                            nameof(t.Elapsed),
                            this.timer_Elapsed);
                    },
                    DependencyNames.Timer);
            });

            r?.Run<SetupHandler>(handler =>
            {
                handler.Handle(this.ui);
            });

            r?.Run<Navigator>(nav =>
                nav.RegisterPresenter(this));
        }

        /// <summary>
        /// Requires a StartHandler
        /// </summary>
        public override void Start()
        {
            var r = this.runner;
            r?.Run<StartHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        /// <summary>
        /// Requires a StopHandler
        /// </summary>
        public override void Stop()
        {
            var r = this.runner;
            r?.Run<StopHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void ui_BackspaceKeyTapped()
        {
            var r = this.runner;
            r?.Run<BackspaceKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void ui_LoginKeyTapped()
        {
            var r = this.runner;
            r?.Run<LoginKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void timer_Elapsed()
        {
            var r = this.runner;
            r?.Run<TimerHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void accessLevelChanged(
            AccessLevel newAccessLevel)
        {
            var r = this.runner;
            r?.Run<AccessLevelChangedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    newAccessLevel);
            });
        }

        private void ui_KeyboardKeyTapped()
        {
            var r = this.runner;
            r?.Run<KeyboardKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private long setupIf1;
        private readonly LoginUi ui;
        private readonly MethodRunner runner;
    }
}