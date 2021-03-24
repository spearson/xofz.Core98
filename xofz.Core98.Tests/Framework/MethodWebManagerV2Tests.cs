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
            protected const string webV3Name = nameof(webV3Name);

            protected class WebV3 : MethodWebV2
            {
            }
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

        public class When_generic_Shuffle_is_called : Context
        {
            public When_generic_Shuffle_is_called()
            {
                this.webV3 = new WebV3();
                var m = this.manager;
                m.AddWeb(
                    new MethodWebV2());
                m.AddWeb(
                    this.webV3, 
                    webV3Name);
            }

            [Fact]
            public void If_types_match_returns_the_web()
            {
                Assert.Same(
                    this.webV3,
                    this.manager.Shuffle<WebV3>());
            }

            [Fact]
            public void Otherwise_returns_default()
            {
                var m = this.manager;
                m.RemoveWeb(webV3Name);

                Assert.Same(
                    default(WebV3),
                    this.manager.Shuffle<WebV3>());
            }

            [Fact]
            public void Returns_web_with_least_deps()
            {
                var m = this.manager;
                const string testName = nameof(testName);
                var testWeb = new MethodWebV2();
                m.AddWeb(
                    testWeb,
                    testName);
                m.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                    }, webV3Name);
                m.AccessWeb(web =>
                {
                    web.RegisterDependency(
                        new object());
                });

                Assert.Same(
                    testWeb,
                    m.Shuffle<MethodWebV2>());
            }

            protected readonly MethodWeb webV3;
        }

        public class When_Shuffle_is_called : Context
        {
            public When_Shuffle_is_called()
            {
                this.webV2 = new MethodWebV2();
                var m = this.manager;
                m.AddWeb(
                    new MethodWeb());
                m.AddWeb(
                    this.webV2,
                    webV2Name);
            }

            [Fact]
            public void If_types_match_returns_the_web()
            {
                Assert.Same(
                    this.webV2,
                    this.manager.Shuffle<MethodWebV2>());
            }

            [Fact]
            public void Otherwise_returns_default()
            {
                var m = this.manager;
                m.RemoveWeb(webV2Name);

                Assert.Same(
                    default(MethodWebV2),
                    this.manager.Shuffle<MethodWebV2>());
            }

            [Fact]
            public void Returns_web_with_least_deps()
            {
                var m = this.manager;
                const string testName = nameof(testName);
                var testWeb = new WebV3();
                m.AddWeb(
                    testWeb,
                    testName);
                m.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                    },
                    webV2Name);

                Assert.Same(
                    testWeb,
                    m.Shuffle<MethodWebV2>());
            }

            protected readonly MethodWeb webV2;
            protected const string webV2Name = nameof(webV2Name);
        }
    }
}
