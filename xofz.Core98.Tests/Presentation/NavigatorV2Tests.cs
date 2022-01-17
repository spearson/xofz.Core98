namespace xofz.Tests.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.Presentation.Presenters;
    using Xunit;

    public class NavigatorV2Tests
    {
        [Fact]
        public void GoHard()
        {
            var w = new MethodWeb();
            var n = new NavigatorV2(w);
            w.RegisterDependency(n);
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (var i = 0; i < 0xFFFF; ++i)
                {
                    new EmptyPresenter(
                        w)
                        .Setup();
                }
            });

            ThreadPool.QueueUserWorkItem(o =>
            {
                for (var i = 0; i < 0xFF; ++i)
                {
                    n.Present<EmptyPresenter>();
                }
            });

            for (var i = 0; i < 0xFFF; ++i)
            {
                n.Unregister<EmptyPresenter>();
            }
        }
    }
}
