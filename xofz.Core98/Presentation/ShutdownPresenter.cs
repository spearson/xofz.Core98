namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Shutdown;

    public sealed class ShutdownPresenter : Presenter
    {
        public ShutdownPresenter(
            MethodWeb web)
            : base(null, null)
        {
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
            var w = this.web;
            w.Run<StartHandler>(handler =>
            {
                handler.Handle();
            });
        }

        private long setupIf1;
        private readonly MethodWeb web;
    }
}
