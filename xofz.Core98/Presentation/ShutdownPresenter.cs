namespace xofz.Presentation
{
    using System.Diagnostics;
    using System.Threading;
    using UI;
    using xofz.Framework;

    public sealed class ShutdownPresenter : Presenter
    {
        public ShutdownPresenter(
            Ui mainUi,
            Do cleanup,
            MethodWeb web)
            : base(mainUi, null)
        {
            this.mainUi = mainUi;
            this.cleanup = cleanup;
            this.web = web;
        }

        public ShutdownPresenter(
            Do cleanup,
            MethodWeb web)
            : base(null, null)
        {
            this.mainUi = default(Ui);
            this.cleanup = cleanup;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.web.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            var mUi = this.mainUi;
            var c = this.cleanup;
            if (mUi != default(Ui))
            {
                UiHelpers.WriteSync(mUi, c);
                Process.GetCurrentProcess().Kill();
                return;
            }

            c();
            Process.GetCurrentProcess().Kill();
        }

        private long setupIf1;
        private readonly Ui mainUi;
        private readonly Do cleanup;
        private readonly MethodWeb web;
    }
}
