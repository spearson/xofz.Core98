namespace xofz.Tests.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using Xunit;

    public class ThreadSafeNavigatorTests
    {
        public class EmptyPresenter : Presenter
        {
            public EmptyPresenter() 
                : base(null, null)
            {
            }

            public override void Start()
            {
            }
        }

        [Fact]
        public void GoHard()
        {
            var w = new MethodWeb();
            var n = new ThreadSafeNavigator(w);
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (var i = 0; i < 0xFFFF; ++i)
                {
                    n.RegisterPresenter(new EmptyPresenter());
                }
            });

            for (var i = 0; i < 0xFF; ++i)
            {
                n.Present<EmptyPresenter>();
            }
        }
    }
}
