namespace xofz.Tests.Framework
{
    using xofz.Framework;
    using Xunit;

    public class ManagerLocatorTests
    {
        public class Context
        {
            protected Context()
            {
                this.locator = new ManagerLocator();
                var l = this.locator;
                l.AddManager(
                    new MethodWebManagerV2(),
                    name);
                l.AccessManager(mwm =>
                {
                    mwm.AddWeb(
                        new MethodWebV2());
                },
                name);
            }

            protected readonly ManagerLocator locator;
            protected const string name = nameof(Locator);
        }

        public class When_ManagerNames_is_called : Context
        {
            [Fact]
            public void Returns_the_manager_names()
            {
                Assert.Contains(
                    name,
                    this.locator.ManagerNames());
            }
        }

        public class When_AddManager_is_called : Context
        {
            [Fact]
            public void Adds_the_manager()
            {
                const byte one = 1;
                const byte zero = 0;

                Assert.Equal(
                    one,
                    this.locator.ManagerNames()
                    ?.Count
                    ?? zero);
            }
        }

        public class When_AccessManager_is_called : Context
        {
            [Fact]
            public void Accesses_the_manager()
            {
                bool accessed = false;
                this.locator.AccessManager(manager =>
                {
                    accessed = true;
                },
                name);

                Assert.True(
                    accessed);
            }
        }

        public class When_generic_AccessManager_is_called : Context
        {
            [Fact]
            public void Accesses_the_manager()
            {
                var accessed = false;
                this.locator.AccessManager<MethodWebManagerV2>(
                    mwmV2 =>
                    {
                        const byte zero = 0;
                        mwmV2.RemoveWeb(
                            null);
                        Assert.Equal(
                            zero,
                            mwmV2.WebNames()?.Count ?? zero);
                        accessed = true;
                    },
                    name);

                Assert.True(accessed);
            }
        }

        public class When_RemoveManager_is_called : Context
        {
            [Fact]
            public void Removes_the_manager()
            {
                byte one = 1, zero = 0;
                Assert.Equal(
                    one,
                    this.locator.ManagerNames()?.Count
                        ?? zero);

                this.locator.RemoveManager(
                        name);

                Assert.Equal(
                    zero,
                    this.locator.ManagerNames()?.Count
                        ?? one);
            }
        }

        public class When_Locate1_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                    });
                },
                name);


                bool located = false;
                l.Locate<object>(o =>
                {
                    located = true;
                },
                name);

                Assert.True(
                    located);
            }
        }

        public class When_Locate2_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new long());
                    });
                },
                name);


                bool located = false;
                l.Locate<long, object>((number, o) =>
                {
                    located = true;
                },
                name);

                Assert.True(
                    located);
            }
        }

        public class When_Locate3_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new long());
                        web.RegisterDependency(
                            new int());
                    });
                },
                name);


                bool located = false;
                l.Locate<long, int, object>((number, i, o) =>
                {
                    located = true;
                },
                name);

                Assert.True(
                    located);
            }
        }

        public class When_Locate4_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new long());
                        web.RegisterDependency(
                            new int());
                        web.RegisterDependency(
                            new object());
                    });
                },
                name);


                bool located = false;
                l.Locate<int, long, object, object>((number, o, o2, i) =>
                {
                    located = true;
                },
                name);

                Assert.True(
                    located);
            }
        }

        public class When_Locate5_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new long());
                        web.RegisterDependency(
                            new int());
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new byte());
                    });
                },
                name);


                bool located = false;
                l.Locate<int, byte, long, object, object>((i, b, number, o, o2) =>
                {
                    located = true;
                },
                name);

                Assert.True(
                    located);
            }
        }

        public class When_Locate6_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new long());
                        web.RegisterDependency(
                            new int());
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new byte());
                        web.RegisterDependency(
                            new object());
                    });
                },
                name);


                bool located = false;
                l.Locate<int, byte, long, object, object, object>((i, b, number, o, o2, o3) =>
                {
                    located = true;
                },
                name);

                Assert.True(
                    located);
            }
        }

        public class When_Locate7_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new long());
                        web.RegisterDependency(
                            new int());
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new byte());
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new byte());
                    });
                },
                name);


                bool located = false;
                l.Locate<int, byte, long, byte, object, object, object>(
                    (i, b, number, b2, o, o2, o3) =>
                    {
                        located = true;
                    },
                    name);

                Assert.True(
                    located);
            }
        }

        public class When_Locate8_is_called
            : Context
        {
            [Fact]
            public void Locates_correctly()
            {
                var l = this.locator;
                l.AccessManager(mwm =>
                {
                    mwm.AccessWeb(web =>
                    {
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new long());
                        web.RegisterDependency(
                            new int());
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new byte());
                        web.RegisterDependency(
                            new object());
                        web.RegisterDependency(
                            new byte());
                        web.RegisterDependency(
                            new long());
                    });
                },
                name);


                bool located = false;
                l.Locate<int, long, byte, long, byte, object, object, object>(
                    (i, number, b, number2, b2, o, o2, o3) =>
                    {
                        located = true;
                    },
                    name);

                Assert.True(
                    located);
            }
        }
    }
}
