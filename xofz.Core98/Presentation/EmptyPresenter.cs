namespace xofz.Presentation
{
    using System.Threading;
    using xofz.Framework;

    public sealed class EmptyPresenter
        : Presenter
    {
        public EmptyPresenter(
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

            var r = this.runner;
            r.Run<Navigator>(nav => 
                nav.RegisterPresenter(this));
        }

        public override void Start()
        {
        }

        private long setupIf1;
        private readonly MethodRunner runner;
    }
}
