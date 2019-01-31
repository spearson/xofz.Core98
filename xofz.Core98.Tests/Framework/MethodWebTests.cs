namespace xofz.Tests.Framework
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using Xunit;

    public class MethodWebTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.fixture = new Fixture();
            }

            protected readonly MethodWeb web;
            protected readonly Fixture fixture;
        }

        public interface SampleDependencyContract
        {
            void Execute1();

            void Execute2();
        }

        public class When_RegisterDependency_is_called : Context
        {
            [Fact]
            public void Returns_false_if_dependency_is_null()
            {
                Assert.False(this.web.RegisterDependency(null));
            }

            [Fact]
            public void Makes_the_dependency_available_for_Run()
            {
                var dependency = A.Fake<SampleDependencyContract>();
                var w = this.web;
                w.RegisterDependency(dependency);

                var registered = false;
                w.Run<SampleDependencyContract>(dep =>
                {
                    registered = true;
                });

                Assert.True(registered);
            }

            [Fact]
            public void Supports_names_for_dependencies()
            {
                var dependency = A.Fake<SampleDependencyContract>();
                var w = this.web;
                var name = this.fixture.Create<string>();
                w.RegisterDependency(
                    dependency,
                    name);

                var registered = false;
                w.Run<SampleDependencyContract>(dep =>
                    {
                        registered = true;
                    },
                    name);

                Assert.True(registered);
            }
        }

        public class When_Run_1_T_is_called : Context
        {
            [Fact]
            public void Returns_default_T_if_dependency_not_found()
            {
                var dependency = this.web.Run<SampleDependencyContract>();

                Assert.Equal(dependency, default(SampleDependencyContract));
            }

            [Fact]
            public void Returns_default_T_if_named_dependency_not_found()
            {
                var w = this.web;
                var f = this.fixture;
                w.RegisterDependency(
                    A.Fake<SampleDependencyContract>(),
                    f.Create<string>());

                var dependency = w.Run<SampleDependencyContract>(
                    dep =>
                    {
                    },
                    f.Create<string>());
                Assert.Equal(dependency, default(SampleDependencyContract));
            }

            [Fact]
            public void Returns_the_dependency_if_found()
            {
                var w = this.web;
                var dependency = A.Fake<SampleDependencyContract>();
                w.RegisterDependency(
                    dependency);

                var result = w.Run<SampleDependencyContract>();

                Assert.Same(dependency, result);
            }

            [Fact]
            public void Returns_the_named_dependency_if_found()
            {
                var w = this.web;
                var f = this.fixture;
                var namedDependency = A.Fake<SampleDependencyContract>();
                var name = f.Create<string>();
                w.RegisterDependency(namedDependency, name);
                w.RegisterDependency(A.Fake<SampleDependencyContract>());

                var result = w.Run<SampleDependencyContract>(
                    dep => { },
                    name);

                Assert.Same(namedDependency, result);
            }

            [Fact]
            public void If_dependency_found_Invokes_the_passed_in_method_on_the_dependency()
            {
                var w = this.web;
                var dependency = A.Fake<SampleDependencyContract>();
                var invoked = false;
                A.CallTo(() => dependency.Execute1())
                    .Invokes(() => invoked = true);
                w.RegisterDependency(dependency);

                w.Run<SampleDependencyContract>(dep =>
                {
                    dep.Execute1();
                });

                Assert.True(invoked);
            }

            [Fact]
            public void If_dependency_not_found_does_not_throw()
            {
                var w = this.web;
                var threw = false;
                try
                {
                    w.Run<SampleDependencyContract>(dependency =>
                    {
                        var x = new object();
                    });
                }
                catch
                {
                    threw = true;
                }

                Assert.False(threw);
            }

            [Fact]
            public void If_dependency_not_found_does_not_execute_the_passed_in_method()
            {
                var w = this.web;
                long shouldStayZero = 0;
                w.Run<SampleDependencyContract>(dependency =>
                {
                    shouldStayZero++;
                    shouldStayZero += 0xFFFF;
                });

                Assert.Equal(shouldStayZero, 0);
            }
        }

        public abstract class AnotherDependency
        {
            public abstract void Harness(ushort word);

            public abstract void GeneralHarness<T>(T item)
                where T : struct;
        }

        public class When_Run_2_T_U_is_called : Context
        {
            [Fact]
            public void Returns_a_tuple_containing_found_dependencies_if_one_is_missing()
            {
                var dep1 = A.Fake<SampleDependencyContract>();
                var w = this.web;
                w.RegisterDependency(dep1);

                var result = w.Run<SampleDependencyContract, AnotherDependency>();

                Assert.Same(
                    dep1,
                    result.Item1);
                Assert.Equal(
                    default(AnotherDependency),
                    result.Item2);
            }

            [Fact]
            public void Returns_a_tuple_of_the_dependencies_if_found()
            {
                var dep1 = A.Fake<SampleDependencyContract>();
                var dep2 = A.Fake<AnotherDependency>();
                var w = this.web;
                w.RegisterDependency(dep1);
                w.RegisterDependency(dep2);

                var result = w.Run<
                    AnotherDependency,
                    SampleDependencyContract>();

                Assert.Same(dep2, result.Item1);
                Assert.Same(dep1, result.Item2);
            }

            [Fact]
            public void If_dependencies_found_invokes_the_passed_in_method_on_the_dependencies()
            {
                var dep1 = A.Fake<SampleDependencyContract>();
                var dep2 = A.Fake<AnotherDependency>();
                var w = this.web;
                w.RegisterDependency(dep1);
                w.RegisterDependency(dep2);
                var execute2Called = false;
                var generalHarnessCalled = false;
                A.CallTo(() => dep1.Execute2())
                    .Invokes(() => execute2Called = true);
                A.CallTo(() => dep2.GeneralHarness<long>(0xFFFF))
                    .Invokes(() => generalHarnessCalled = true);

                w.Run<SampleDependencyContract, AnotherDependency>(
                    (sdc, ad) =>
                    {
                        sdc.Execute1();
                        sdc.Execute2();
                        ad.GeneralHarness<long>(0xFFFF);
                    });

                A.CallTo(() => dep1.Execute1())
                    .MustHaveHappened();
                Assert.True(execute2Called);
                Assert.True(generalHarnessCalled);
            }
        }

        public interface SampleObject
        {
            void Operate1();
        }

        public class When_Run_3_T_U_V_is_called : Context
        {
            [Fact]
            public void Does_not_throw_if_dependency_not_found()
            {
                var w = this.web;
                w.RegisterDependency(A.Fake<SampleObject>());
                bool throws;
                try
                {
                    w.Run<
                        SampleObject,
                        SampleDependencyContract,
                        AnotherDependency>(
                        (so, sdc, ad) =>
                        {
                            so.Operate1();
                            sdc.Execute1();
                            ad.GeneralHarness(0xFF);
                        });
                    throws = false;
                }
                catch
                {
                    throws = true;
                }

                Assert.False(throws);
            }

            [Fact]
            public void Does_not_execute_passed_in_method_if_dependency_not_found()
            {
                var w = this.web;
                w.RegisterDependency(A.Fake<SampleObject>());
                w.RegisterDependency(A.Fake<AnotherDependency>());
                bool executed = false;
                w.Run<
                    SampleObject, 
                    SampleDependencyContract, 
                    AnotherDependency>(
                    (so, sdc, ad) =>
                    {
                        so.Operate1();
                        sdc.Execute2();
                        ad.GeneralHarness(0xD34DB33F);
                        executed = true;
                    });

                Assert.False(executed);
            }

            [Fact]
            public void If_dependencies_found_executes_passed_in_method()
            {
                var w = this.web;
                var sampleObject = A.Fake<SampleObject>();
                var sampleDependency = A.Fake<SampleDependencyContract>();
                var sampleDependency2 = A.Fake<AnotherDependency>();
                w.RegisterDependency(sampleObject);
                w.RegisterDependency(sampleDependency);
                w.RegisterDependency(sampleDependency2);
                w.Run<
                    SampleDependencyContract,
                    AnotherDependency, 
                    SampleObject>(
                    (sdc, ad, so) =>
                    {
                        sdc.Execute1();

                        so.Operate1();

                        ad.Harness(0xFED5);
                    });

                A.CallTo(() => sampleObject.Operate1())
                    .MustHaveHappened();
                A.CallTo(() => sampleDependency.Execute1())
                    .MustHaveHappened();
                A.CallTo(() => sampleDependency2.Harness(
                        0xFED5))
                    .MustHaveHappened();
            }
        }

        public interface SampleService
        {
            void Start();
        }

        public class When_Process_4T_is_called : Context
        {
            [Fact]
            public void Does_not_throw_if_dependency_not_found()
            {
                var w = this.web;
                w.RegisterDependency(A.Fake<SampleObject>());
                bool throws;
                try
                {
                    w.Run<
                        SampleObject,
                        SampleDependencyContract,
                        AnotherDependency,
                        SampleService>(
                        (so, sdc, ad, ss) =>
                        {
                            so.Operate1();
                            sdc.Execute1();
                            ad.GeneralHarness(0xFF);
                        });
                    throws = false;
                }
                catch
                {
                    throws = true;
                }

                Assert.False(throws);
            }

            [Fact]
            public void Does_not_execute_passed_in_method_if_dependency_not_found()
            {
                var w = this.web;
                w.RegisterDependency(A.Fake<SampleObject>());
                w.RegisterDependency(A.Fake<AnotherDependency>());
                bool executed = false;
                w.Run<
                    SampleObject,
                    SampleDependencyContract,
                    AnotherDependency,
                    SampleService>(
                    (so, sdc, ad, ss) =>
                    {
                        so.Operate1();
                        sdc.Execute2();
                        ad.GeneralHarness(0xD34DB33F);
                        executed = true;
                    });

                Assert.False(executed);
            }

            [Fact]
            public void If_dependencies_found_executes_passed_in_method()
            {
                var w = this.web;
                var sampleObject = A.Fake<SampleObject>();
                var sampleDependency = A.Fake<SampleDependencyContract>();
                var sampleDependency2 = A.Fake<AnotherDependency>();
                var sampleService = A.Fake<SampleService>();
                w.RegisterDependency(sampleObject);
                w.RegisterDependency(sampleDependency);
                w.RegisterDependency(sampleDependency2);
                w.RegisterDependency(sampleService);
                w.Run<
                    SampleDependencyContract,
                    AnotherDependency,
                    SampleObject,
                    SampleService>(
                    (sdc, ad, so, ss) =>
                    {
                        sdc.Execute1();

                        so.Operate1();

                        ad.Harness(0xFED5);

                        ss.Start();
                    });

                A.CallTo(() => sampleObject.Operate1())
                    .MustHaveHappened();
                A.CallTo(() => sampleDependency.Execute1())
                    .MustHaveHappened();
                A.CallTo(() => sampleDependency2.Harness(
                        0xFED5))
                    .MustHaveHappened();
                A.CallTo(() => sampleService.Start())
                    .MustHaveHappened();
            }
        }
    }
}
