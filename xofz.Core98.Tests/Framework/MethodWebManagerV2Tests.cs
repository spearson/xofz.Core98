namespace xofz.Tests.Framework
{
    using System.Threading;
    using xofz.Framework;
    using Xunit;

    public class MethodWebManagerV2Tests
    {
        public class Context
        {
            protected Context()
            {
                this.manager = new MethodWebManagerV2();
            }

            protected readonly MethodWebManagerV2 manager;

        }

        public class When_stressed : Context
        {
            [Fact]
            public void Goes_hard()
            {
                var mwm = this.manager;
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
                        mwm.RemoveWeb(null);
                    }
                });

                ThreadPool.QueueUserWorkItem(
                    o =>
                    {
                        for (var i = 0; i < 0xFFFFFF; ++i)
                        {
                            mwm.AccessWeb<MethodWebV2>(
                                web => web.Unregister<object>());
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
}
