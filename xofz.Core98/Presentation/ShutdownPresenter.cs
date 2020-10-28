namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Shutdown;

    public sealed class ShutdownPresenter 
        : Presenter
    {
        public ShutdownPresenter(
            MethodRunner runner)
            : base(null, null)
        {
            this.runner = runner;
        }

        public void Setup()
        {
            if (Interlocked.Exchange(
                ref this.setupIf1,
                1) == 1)
            {
                return;
            }

            this.runner.Run<Navigator>(
                nav => nav.RegisterPresenter(this));
        }

        public override void Start()
        {
            var r = this.runner;
            r.Run<StartHandler>(handler =>
            {
                handler.Handle();
            });
        }

        private long setupIf1;
        private readonly MethodRunner runner;
    }
}
