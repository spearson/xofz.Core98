namespace xofz.Tests.Framework
{
    using System.Threading;
    using Ploeh.AutoFixture;
    using xofz.Framework.Lots;
    using Xunit;

    public class MethodWebV2Tests
    {
        public class Context
        {
            protected Context()
            {
                this.v2 = new xofz.Framework.MethodWebV2();
                this.fixture = new Fixture();
            }

            protected xofz.Framework.MethodWebV2 v2;
            protected readonly Fixture fixture;
            protected const bool
                truth = true,
                falsity = false;
        }

        public class When_RegisterDependency_is_called : Context
        {
            [Fact]
            public void Does_not_register_null()
            {
                Assert.False(
                    this.v2.RegisterDependency(
                        null,
                        0xFFF.ToString()));
            }

            [Fact]
            public void Registers_the_dependency_otherwise()
            {
                var name = 0xFFFF.ToString();
                Assert.True(
                    this.v2.RegisterDependency(
                        new object(),
                        name));

                var registered = falsity;
                this.v2.Run<object>(o => { registered = truth; },
                    name);

                Assert.True(registered);
            }
        }

        public class When_Unregister_is_called : Context
        {
            [Fact]
            public void Does_not_unregister_if_not_found()
            {
                var name = 0x1FF.ToString();
                this.v2.RegisterDependency(
                    new object());
                this.v2.RegisterDependency(
                    new object(),
                    name);

                Assert.False(
                    this.v2.Unregister<object>(
                        0x2FF.ToString()));

                bool
                    stillRegistered1 = falsity,
                    stillRegistered2 = falsity;
                this.v2.Run<object>(o1 =>
                    {
                        this.v2.Run<object>(o2 =>
                        {
                            stillRegistered1 = truth;
                        });

                        stillRegistered2 = truth;
                    },
                    name);

                Assert.True(
                    stillRegistered1);
                Assert.True(
                    stillRegistered2);
            }

            [Fact]
            public void Unregisters_otherwise()
            {
                var name = 0x3FF.ToString();
                this.v2.RegisterDependency(
                    new object(),
                    name);
                this.v2.RegisterDependency(
                    new object());

                Assert.True(
                    this.v2.Unregister<object>(name));

                var stillRegistered2 = falsity;
                this.v2.Run<object>(o2 => { stillRegistered2 = truth; });

                Assert.True(
                    stillRegistered2);
            }
        }

        public class When_Run_is_called : Context
        {
            [Fact]
            public void Runs_the_dependencies_if_found()
            {
                this.v2.RegisterDependency(
                    new object());
                this.v2.RegisterDependency(
                    new object(),
                    name1);
                this.v2.RegisterDependency(
                    new object(),
                    name2);
                this.v2.RegisterDependency(
                    new object(),
                    name3);
                this.v2.RegisterDependency(
                    new object(),
                    name4);
                this.v2.RegisterDependency(
                    new object(),
                    name5);
                this.v2.RegisterDependency(
                    new object(),
                    name6);
                this.v2.RegisterDependency(
                    new object(),
                    name7);

                var allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object, object, object,
                        object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5,
                            o6,
                            o7,
                            o8) =>
                        {
                            allRegistered = truth;
                        },
                        name7,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2,
                        name1);

                Assert.True(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5,
                            o6,
                            o7) =>
                        {
                            allRegistered = truth;
                        },
                        name1,
                        name2,
                        name3,
                        name4,
                        name5,
                        name6,
                        name7);

                Assert.True(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5,
                            o6) =>
                        {
                            allRegistered = truth;
                        },
                        name6,
                        name5,
                        name4,
                        name3,
                        name2,
                        name1);

                Assert.True(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5) =>
                        {
                            allRegistered = truth;
                        },
                        name1,
                        name2,
                        name3,
                        name4,
                        name5);

                Assert.True(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4) =>
                        {
                            allRegistered = truth;
                        },
                        name4,
                        name3,
                        name2,
                        name1);

                Assert.True(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object>(
                        (o1,
                            o2,
                            o3) =>
                        {
                            allRegistered = truth;
                        },
                        name1,
                        name2,
                        name3);

                Assert.True(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object>(
                        (o1,
                            o2) =>
                        {
                            allRegistered = truth;
                        },
                        name2,
                        name1);

                Assert.True(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object>(
                        o1 => { allRegistered = truth; },
                        name1);

                Assert.True(
                    allRegistered);
            }

            [Fact]
            public void Otherwise_does_not()
            {
                this.v2.RegisterDependency(
                    new object());
                this.v2.RegisterDependency(
                    new object(),
                    name1);
                this.v2.RegisterDependency(
                    new object(),
                    name2);
                this.v2.RegisterDependency(
                    new object(),
                    name3);
                this.v2.RegisterDependency(
                    new object(),
                    name4);
                this.v2.RegisterDependency(
                    new object(),
                    name5);
                this.v2.RegisterDependency(
                    new object(),
                    name6);
                this.v2.RegisterDependency(
                    new object(),
                    name7);

                var allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object, object, object,
                        object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5,
                            o6,
                            o7,
                            o8) =>
                        {
                            allRegistered = truth;
                        },
                        name8,
                        name7,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2,
                        name1);

                Assert.False(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5,
                            o6,
                            o7) =>
                        {
                            allRegistered = truth;
                        },
                        name8,
                        name7,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2);

                Assert.False(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5,
                            o6) =>
                        {
                            allRegistered = truth;
                        },
                        name8,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2);

                Assert.False(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4,
                            o5) =>
                        {
                            allRegistered = truth;
                        },
                        name8,
                        name6,
                        name5,
                        name4,
                        name3);

                Assert.False(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object, object>(
                        (o1,
                            o2,
                            o3,
                            o4) =>
                        {
                            allRegistered = truth;
                        },
                        name8,
                        name5,
                        name4,
                        name3);

                Assert.False(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object, object, object>(
                        (o1,
                            o2,
                            o3) =>
                        {
                            allRegistered = truth;
                        },
                        name8,
                        name5,
                        name4);

                allRegistered = falsity;
                this.v2.
                    Run<object, object>(
                        (o1,
                            o2) =>
                        {
                            allRegistered = truth;
                        },
                        name8,
                        name4);

                Assert.False(
                    allRegistered);

                allRegistered = falsity;
                this.v2.
                    Run<object>(
                        o1 => { allRegistered = truth; },
                        name8);

                Assert.False(
                    allRegistered);
            }

            [Fact]
            public void Returns_full_XTuple_if_found()
            {
                this.v2.RegisterDependency(
                    new object());
                this.v2.RegisterDependency(
                    new object(),
                    name1);
                this.v2.RegisterDependency(
                    new object(),
                    name2);
                this.v2.RegisterDependency(
                    new object(),
                    name3);
                this.v2.RegisterDependency(
                    new object(),
                    name4);
                this.v2.RegisterDependency(
                    new object(),
                    name5);
                this.v2.RegisterDependency(
                    new object(),
                    name6);
                this.v2.RegisterDependency(
                    new object(),
                    name7);

                var xT8 = this.v2.
                    Run<object, object, object, object, object, object, object,
                        object>(
                        null,
                        name7,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2,
                        name1);

                var xT7 = this.v2.
                    Run<object, object, object, object, object, object, object>(
                        null,
                        name7,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2);

                var xT6 = this.v2.
                    Run<object, object, object, object, object, object>(
                        null,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2);

                var xT5 = this.v2.
                    Run<object, object, object, object, object>(
                        null,
                        name6,
                        name5,
                        name4,
                        name3);

                var xT4 = this.v2.
                    Run<object, object, object, object>(
                        null,
                        name6,
                        name5,
                        name4);

                var xT3 = this.v2.
                    Run<object, object, object>(
                        null,
                        name5,
                        name4);

                var xT2 = this.v2.Run<object, object>(
                    null,
                    name4);

                var o = this.v2.Run<object>();

                Assert.NotNull(
                    xT8.Item1);
                Assert.NotNull(
                    xT8.Item2);
                Assert.NotNull(
                    xT8.Item3);
                Assert.NotNull(
                    xT8.Item4);
                Assert.NotNull(
                    xT8.Item5);
                Assert.NotNull(
                    xT8.Item6);
                Assert.NotNull(
                    xT8.Item7);
                Assert.NotNull(
                    xT8.Item8);

                Assert.NotNull(
                    xT7.Item1);
                Assert.NotNull(
                    xT7.Item2);
                Assert.NotNull(
                    xT7.Item3);
                Assert.NotNull(
                    xT7.Item4);
                Assert.NotNull(
                    xT7.Item5);
                Assert.NotNull(
                    xT7.Item6);
                Assert.NotNull(
                    xT7.Item7);

                Assert.NotNull(
                    xT6.Item1);
                Assert.NotNull(
                    xT6.Item2);
                Assert.NotNull(
                    xT6.Item3);
                Assert.NotNull(
                    xT6.Item4);
                Assert.NotNull(
                    xT6.Item5);
                Assert.NotNull(
                    xT6.Item6);

                Assert.NotNull(
                    xT5.Item1);
                Assert.NotNull(
                    xT5.Item2);
                Assert.NotNull(
                    xT5.Item3);
                Assert.NotNull(
                    xT5.Item4);
                Assert.NotNull(
                    xT5.Item5);

                Assert.NotNull(
                    xT4.Item1);
                Assert.NotNull(
                    xT4.Item2);
                Assert.NotNull(
                    xT4.Item3);
                Assert.NotNull(
                    xT4.Item4);

                Assert.NotNull(
                    xT3.Item1);
                Assert.NotNull(
                    xT3.Item2);
                Assert.NotNull(
                    xT3.Item3);

                Assert.NotNull(
                    xT2.Item1);
                Assert.NotNull(
                    xT2.Item2);

                Assert.NotNull(
                    o);
            }

            [Fact]
            public void Otherwise_does_not_return_full_one()
            {
                this.v2.RegisterDependency(
                    new object());
                this.v2.RegisterDependency(
                    new object(),
                    name1);
                this.v2.RegisterDependency(
                    new object(),
                    name2);
                this.v2.RegisterDependency(
                    new object(),
                    name3);
                this.v2.RegisterDependency(
                    new object(),
                    name4);
                this.v2.RegisterDependency(
                    new object(),
                    name5);
                this.v2.RegisterDependency(
                    new object(),
                    name6);
                this.v2.RegisterDependency(
                    new object(),
                    name7);

                var xT8 = this.v2.
                    Run<object, object, object, object, object, object, object,
                        object>(
                        null,
                        name1,
                        name2,
                        name3,
                        name4,
                        name5,
                        name6,
                        name7,
                        name8);

                var xT7 = this.v2.
                    Run<object, object, object, object, object, object, object>(
                        null,
                        name8,
                        name7,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2);

                var xT6 = this.v2.
                    Run<object, object, object, object, object, object>(
                        null,
                        name8,
                        name6,
                        name5,
                        name4,
                        name3,
                        name2);

                var xT5 = this.v2.
                    Run<object, object, object, object, object>(
                        null,
                        name8,
                        name6,
                        name5,
                        name4,
                        name3);

                var xT4 = this.v2.
                    Run<object, object, object, object>(
                        null,
                        name8,
                        name5,
                        name4,
                        name3);

                var xT3 = this.v2.
                    Run<object, object, object>(
                        null,
                        name8,
                        name5,
                        name4);

                var xT2 = this.v2.
                    Run<object, object>(
                        (o1,
                            o2) =>
                        {
                        },
                        name8,
                        name4);

                var o = this.v2.
                    Run<object>(
                        null,
                        name8);


                Assert.NotNull(
                    xT8.Item1);
                Assert.NotNull(
                    xT8.Item2);
                Assert.NotNull(
                    xT8.Item3);
                Assert.NotNull(
                    xT8.Item4);
                Assert.NotNull(
                    xT8.Item5);
                Assert.NotNull(
                    xT8.Item6);
                Assert.NotNull(
                    xT8.Item7);
                Assert.Null(
                    xT8.Item8);

                Assert.Null(
                    xT7.Item1);
                Assert.NotNull(
                    xT7.Item2);
                Assert.NotNull(
                    xT7.Item3);
                Assert.NotNull(
                    xT7.Item4);
                Assert.NotNull(
                    xT7.Item5);
                Assert.NotNull(
                    xT7.Item6);
                Assert.NotNull(
                    xT7.Item7);

                Assert.Null(
                    xT6.Item1);
                Assert.NotNull(
                    xT6.Item2);
                Assert.NotNull(
                    xT6.Item3);
                Assert.NotNull(
                    xT6.Item4);
                Assert.NotNull(
                    xT6.Item5);
                Assert.NotNull(
                    xT6.Item6);

                Assert.Null(
                    xT5.Item1);
                Assert.NotNull(
                    xT5.Item2);
                Assert.NotNull(
                    xT5.Item3);
                Assert.NotNull(
                    xT5.Item4);
                Assert.NotNull(
                    xT5.Item5);

                Assert.Null(
                    xT4.Item1);
                Assert.NotNull(
                    xT4.Item2);
                Assert.NotNull(
                    xT4.Item3);
                Assert.NotNull(
                    xT4.Item4);

                Assert.Null(
                    xT3.Item1);
                Assert.NotNull(
                    xT3.Item2);
                Assert.NotNull(
                    xT3.Item3);

                Assert.Null(
                    xT2.Item1);
                Assert.NotNull(
                    xT2.Item2);

                Assert.Null(
                    o);
            }

            protected const string name1 = nameof(name1);
            protected const string name2 = nameof(name2);
            protected const string name3 = nameof(name3);
            protected const string name4 = nameof(name4);
            protected const string name5 = nameof(name5);
            protected const string name6 = nameof(name6);
            protected const string name7 = nameof(name7);
            protected const string name8 = nameof(name8);
        }

        public class Sync_tests 
            : Context
        {
            [Fact]
            public void Does_not_throw_when_going_hard()
            {
                var w = this.v2;
                ThreadPool.QueueUserWorkItem(state =>
                {
                    for (short i = 0; i < 0xFFF; ++i)
                    {
                        w.Run<object>();
                        w.RegisterDependency(new object());
                        w.Run<object>();
                    }
                });

                ThreadPool.QueueUserWorkItem(state =>
                {
                    for (short i = 0; i < 0xFFF; ++i)
                    {
                        w.Run<object>();
                        w.Run<object>();
                        w.Unregister<object>();
                    }
                });

                Thread.Sleep(0xFF);
            }
        }
    }
}
