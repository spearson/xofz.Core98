namespace xofz.Presentation
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using UI;
    using xofz.Framework;

    public sealed class ShutdownPresenter : Presenter
    {
        public ShutdownPresenter(
            Ui mainUi,
            Action cleanup,
            MethodWeb web)
            : base(mainUi, null)
        {
            this.mainUi = mainUi;
            this.cleanup = cleanup;
            this.web = web;
        }

        public ShutdownPresenter(
            Action cleanup,
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
                UiHelpers.Write(mUi, c);
                mUi.WriteFinished.WaitOne();
                Process.GetCurrentProcess().Kill();
                return;
            }

            c();
            Process.GetCurrentProcess().Kill();
        }

        private int setupIf1;
        private readonly Ui mainUi;
        private readonly Action cleanup;
        private readonly MethodWeb web;
    }
}
