namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;

    public sealed class LoginPresenter : Presenter
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

        /// <summary>
        /// Requires an EventSubscriber, SetupHandler, and a Navigator
        /// </summary>
        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            w.Run<EventSubscriber>(subscriber =>
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
                w.Run<AccessController>(ac =>
                {
                    subscriber.Subscribe<AccessLevel>(
                        ac,
                        nameof(ac.AccessLevelChanged),
                        this.accessLevelChanged);
                });
                w.Run<xofz.Framework.Timer>(t =>
                    {
                        subscriber.Subscribe(
                            t,
                            nameof(t.Elapsed),
                            this.timer_Elapsed);
                    },
                    "LoginTimer");
            });

            w.Run<SetupHandler>(handler =>
            {
                handler.Handle(this.ui);
            });

            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        /// <summary>
        /// Requires a StartHandler
        /// </summary>
        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return;
            }

            var w = this.web;
            w.Run<StartHandler>(handler => handler.Handle(this.ui));
        }

        /// <summary>
        /// Requires a StopHandler
        /// </summary>
        public override void Stop()
        {
            if (Interlocked.Read(ref this.setupIf1) != 1)
            {
                return;
            }

            var w = this.web;
            w.Run<StopHandler>(handler => handler.Handle(this.ui));
        }

        private void ui_BackspaceKeyTapped()
        {
            var w = this.web;
            w.Run<BackspaceKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void ui_LoginKeyTapped()
        {
            var w = this.web;
            w.Run<LoginKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private void timer_Elapsed()
        {
            var h = this.timerHandlerFinished;
            h.Reset();

            var w = this.web;
            w.Run<TimerHandler>(handler => handler.Handle(this.ui));

            h.Set();
        }

        private void accessLevelChanged(AccessLevel newAccessLevel)
        {
            var w = this.web;
            w.Run<AccessLevelChangedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    newAccessLevel);
            });
        }

        private void ui_KeyboardKeyTapped()
        {
            var w = this.web;
            w.Run<KeyboardKeyTappedHandler>(handler =>
            {
                handler.Handle(this.ui);
            });
        }

        private long setupIf1;
        private readonly LoginUi ui;
        private readonly MethodWeb web;
        private readonly ManualResetEvent timerHandlerFinished;
    }
}
