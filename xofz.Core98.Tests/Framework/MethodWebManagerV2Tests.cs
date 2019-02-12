namespace xofz.Tests.Framework
{
    using System.Threading;
    using xofz.Framework;
    using Xunit;

    public class MethodWebManagerV2Tests
    {
        [Fact]
        public void GoHard()
        {
            var mwm = new MethodWebManagerV2();
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (var i = 0; i < 0xFFFFF; ++i)
                {
                    mwm.AddWeb(new MethodWebV2());
                }
            });

            ThreadPool.QueueUserWorkItem(o =>
            {
                for (var i = 0; i < 0xFFFFF; ++i)
                {
                    mwm.RemoveWeb(nameof(MethodWebNameConsts.Default));
                }
            });

            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    for (var i = 0; i < 0xFFFFFF; ++i)
                    {
                        mwm.AccessWeb(web => web.Unregister<object>());
                    }
                });
            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    for (var i = 0; i < 0xFFFFFF; ++i)
                    {
                        mwm.AccessWeb(
                            web => web.RegisterDependency(new object()));
                    }
                });
            for (var i = 0; i < 0xFFFF; ++i)
            {
                var currentIndex = i;
                mwm.RunWeb<object>(o =>
                {
                    var hc = o.GetHashCode();
                    hc ^= hc >> 1;
                });
            }
        }
    }
}
