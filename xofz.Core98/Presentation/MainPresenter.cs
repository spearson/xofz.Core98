namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Main;
    using xofz.UI;

    public sealed class MainPresenter : Presenter
    {
        public MainPresenter(
            MainUi ui,
            MethodWeb web)
            : base(ui, null)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            w.Run<EventSubscriber>(sub =>
            {
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.ShutdownRequested),
                    this.ui_ShutdownRequested);
            });
            
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
        }

        private void ui_ShutdownRequested()
        {
            var w = this.web;
            Do logIn = null, shutdown = null;
            w.Run<Navigator>(nav =>
            {
                logIn = nav.LoginFluidly;
                shutdown = nav.Present<ShutdownPresenter>;
            });

            w.Run<ShutdownRequestedHandler>(handler =>
            {
                handler.Handle(
                    this.ui,
                    logIn,
                    shutdown);
            });
        }

        private long setupIf1;
        private readonly MainUi ui;
        private readonly MethodWeb web;
    }
}
