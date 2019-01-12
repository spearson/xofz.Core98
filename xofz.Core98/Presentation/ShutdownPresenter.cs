namespace xofz.Presentation
{
    using System.Diagnostics;
    using System.Threading;
    using UI;
    using xofz.Framework;

    public sealed class ShutdownPresenter : Presenter
    {
        public ShutdownPresenter(
            MethodWeb web)
            : base(null, null)
        {
            this.mainUi = null;
            this.cleanup = null;
            this.web = web;
        }

        public ShutdownPresenter(
            Do cleanup,
            MethodWeb web)
            : base(null, null)
        {
            this.mainUi = null;
            this.cleanup = cleanup;
            this.web = web;
        }

        public ShutdownPresenter(
            Ui mainUi,
            Do cleanup,
            MethodWeb web)
            : base(null, null)
        {
            this.mainUi = mainUi;
            this.cleanup = cleanup;
            this.web = web;
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

            this.web.Run<Navigator>(
                nav => nav.RegisterPresenter(this));
        }

        public override void Start()
        {
            var c = this.cleanup;
            if (c == null)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

            var ui = this.mainUi;
            if (ui == null)
            {
                c();
                Process.GetCurrentProcess().Kill();
                return;
            }

            UiHelpers.WriteSync(ui, c);
            Process.GetCurrentProcess().Kill();
        }

        private long setupIf1;
        private readonly Ui mainUi;
        private readonly Do cleanup;
        private readonly MethodWeb web;
    }
}
