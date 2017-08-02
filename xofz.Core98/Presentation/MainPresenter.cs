namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
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

        public void Setup(
            AccessLevel shutdownLevel = AccessLevel.None)
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.shutdownLevel = shutdownLevel;
            this.ui.ShutdownRequested += this.ui_ShutdownRequested;
            var w = this.web;
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
        }

        private void ui_ShutdownRequested()
        {
            var w = this.web;
            var cal = w.Run<AccessController, AccessLevel>(
                ac => ac.CurrentAccessLevel);

            if (cal >= this.shutdownLevel)
            {
                w.Run<Navigator>(n => n.Present<ShutdownPresenter>());
            }
            else
            {
                w.Run<Navigator>(n => n.LoginFluidly());
                cal = w.Run<AccessController, AccessLevel>(
                    ac => ac.CurrentAccessLevel);
                if (cal >= this.shutdownLevel)
                {
                    w.Run<EventRaiser>(
                        er => er.Raise(
                            this.ui,
                            "ShutdownRequested"));
                }
            }
        }

        private int setupIf1;
        private AccessLevel shutdownLevel;
        private readonly MainUi ui;
        private readonly MethodWeb web;
    }
}
