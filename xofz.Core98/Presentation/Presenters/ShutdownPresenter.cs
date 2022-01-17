namespace xofz.Presentation.Presenters
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
            const long one = 1;
            if (Interlocked.Exchange(
                ref this.setupIf1,
                one) == one)
            {
                return;
            }

            var r = this.runner;
            r?.Run<Navigator>(nav => 
                nav.RegisterPresenter(this));
        }

        public override void Start()
        {
            var r = this.runner;
            r?.Run<StartHandler>(handler =>
            {
                handler.Handle();
            });
        }

        private long setupIf1;
        private readonly MethodRunner runner;
    }
}
