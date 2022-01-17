namespace xofz.Presentation.Presenters
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
            const long one = 1;
            if (Interlocked.Exchange(
                    ref this.setupIf1, 
                    one) == one)
            {
                return;
            }

            var r = this.runner;
            r?.Run<EventSubscriber>(sub =>
            {
                sub.Subscribe(
                    this.ui,
                    nameof(this.ui.ShutdownRequested),
                    this.ui_ShutdownRequested);
            });
            
            r?.Run<Navigator>(nav => 
                nav.RegisterPresenter(this));
        }

        public override void Start()
        {
        }

        private void ui_ShutdownRequested()
        {
            var r = this.runner;
            r?.Run<Navigator>(nav =>
            {
                Do logIn = nav.LoginFluidly;
                Do shutdown = nav.Present<ShutdownPresenter>;

                r.Run<ShutdownRequestedHandler>(handler =>
                {
                    handler.Handle(
                        this.ui,
                        logIn,
                        shutdown);
                });
            });
        }

        private long setupIf1;
        private readonly MainUi ui;
        private readonly MethodRunner runner;
    }
}
