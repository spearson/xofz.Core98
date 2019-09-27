namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Main;
    using xofz.UI;

    public sealed class MainPresenter 
        : Presenter
    {
        public MainPresenter(
            MainUi ui,
            MethodRunner runner)
            : base(ui, null)
        {
            this.ui = ui;
            this.runner = runner;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(
                    ref this.setupIf1, 
                    1, 
                    0) == 1)
            {
                return;
            }

            var r = this.runner;
            r.Run<EventSubscriber>(sub =>
            {
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.ShutdownRequested),
                    this.ui_ShutdownRequested);
            });
            
            r.Run<Navigator>(nav => 
                nav.RegisterPresenter(this));
        }

        public override void Start()
        {
        }

        private void ui_ShutdownRequested()
        {
            var r = this.runner;
            Do logIn = null, shutdown = null;
            r.Run<Navigator>(nav =>
            {
                logIn = nav.LoginFluidly;
                shutdown = nav.Present<ShutdownPresenter>;
            });

            r.Run<ShutdownRequestedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    logIn,
                    shutdown);
            });
        }

        private long setupIf1;
        private readonly MainUi ui;
        private readonly MethodRunner runner;
    }
}
