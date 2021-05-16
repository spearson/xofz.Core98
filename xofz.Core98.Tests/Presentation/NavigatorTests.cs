namespace xofz.Tests.Presentation
{
    using System;
    using System.Threading;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.Presentation;
    using Xunit;

    public class NavigatorTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.navigator = new Navigator(
                    this.web);
                this.fixture = new Fixture();
            }

            protected readonly MethodWeb web;
            protected readonly Navigator navigator;
            protected readonly Fixture fixture;
        }

        public class When_Present_is_called_on_a_Presenter : Context
        {
            [Fact]
            public void If_it_finds_the_presenter_stops_all_presenters()
            {
                var n = this.navigator;
                var p1 = A.Fake<Presenter>();
                var p2 = A.Fake<Presenter>();
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);
                n.Present<Presenter>();

                A.CallTo(() => p1.Stop())
                    .MustHaveHappened();
                A.CallTo(() => p2.Stop())
                    .MustHaveHappened();
            }

            [Fact]
            public void If_it_finds_the_presenter_starts_it_in_a_new_thread()
            {
                var n = this.navigator;
                var p1 = A.Fake<Presenter>();
                var p2 = A.Fake<Presenter>();
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);
                var e = new ManualResetEvent(false);
                var started = false;
                A.CallTo(() => p1.Start())
                    .Invokes(() =>
                    {
                        started = true;
                        e.Set();
                    });

                n.Present<Presenter>();
                e.WaitOne();

                Assert.True(started);
            }
        }

        public class When_Present_is_called_on_a_NamedPresenter : Context
        {
            [Fact]
            public void If_it_finds_a_matching_NamedPresenter_stops_all_presenters()
            {
                var f = this.fixture;
                var p1 = A.Fake<NamedPresenter>();
                p1.Name = f.Create<string>();
                var p2 = A.Fake<Presenter>();
                var n = this.navigator;
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);

                n.Present<NamedPresenter>(
                    p1.Name);

                A.CallTo(() => p1.Stop())
                    .MustHaveHappened();
                A.CallTo(() => p2.Stop())
                    .MustHaveHappened();
            }

            [Fact]
            public void If_it_finds_a_matching_NamedPresenter_starts_it_in_a_new_thread()
            {
                var f = this.fixture;
                var p1 = A.Fake<NamedPresenter>();
                p1.Name = f.Create<string>();
                var p2 = A.Fake<Presenter>();
                var n = this.navigator;
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);
                var e = new ManualResetEvent(false);
                var started = false;
                A.CallTo(() => p1.Start())
                    .Invokes(() =>
                    {
                        started = true;
                        e.Set();
                    });

                n.Present<NamedPresenter>(
                    p1.Name);
                e.WaitOne();

                Assert.True(started);
            }
        }

        public class When_PresentFluidly_is_called_on_a_Presenter : Context
        {
            [Fact]
            public void If_it_finds_the_presenter_does_not_stop_any_presenter()
            {
                var p1 = A.Fake<Presenter>();
                var p2 = A.Fake<Presenter>();
                var p3 = A.Fake<Presenter>();
                var n = this.navigator;
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);
                n.RegisterPresenter(p3);

                n.PresentFluidly<Presenter>();

                A.CallTo(() => p1.Stop())
                    .MustNotHaveHappened();
                A.CallTo(() => p2.Stop())
                    .MustNotHaveHappened();
                A.CallTo(() => p3.Stop())
                    .MustNotHaveHappened();
            }

            [Fact]
            public void If_it_finds_the_presenter_starts_it_in_a_new_thread()
            {
                var p1 = A.Fake<Presenter>();
                var p2 = A.Fake<Presenter>();
                var n = this.navigator;
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);
                var e = new ManualResetEvent(false);
                var started = false;
                A.CallTo(() => p1.Start())
                    .Invokes(() =>
                    {
                        started = true;
                        e.Set();
                    });

                n.PresentFluidly<Presenter>();
                e.WaitOne();

                Assert.True(started);
            }
        }

        public class When_PresentFluidly_is_called_on_a_NamedPresenter : Context
        {
            [Fact]
            public void If_it_finds_a_matching_NamedPresenter_does_not_stop_any_presenters()
            {
                var f = this.fixture;
                var p1 = A.Fake<NamedPresenter>();
                p1.Name = f.Create<string>();
                var p2 = A.Fake<Presenter>();
                var n = this.navigator;
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);

                n.PresentFluidly<NamedPresenter>(
                    p1.Name);

                A.CallTo(() => p1.Stop())
                    .MustNotHaveHappened();
                A.CallTo(() => p2.Stop())
                    .MustNotHaveHappened();
            }

            [Fact]
            public void If_it_finds_a_matching_NamedPresenter_starts_it_in_a_new_thread()
            {
                var f = this.fixture;
                var p1 = A.Fake<NamedPresenter>();
                p1.Name = f.Create<string>();
                var p2 = A.Fake<Presenter>();
                var n = this.navigator;
                n.RegisterPresenter(p1);
                n.RegisterPresenter(p2);
                var e = new ManualResetEvent(false);
                var started = false;
                A.CallTo(() => p1.Start())
                    .Invokes(() =>
                    {
                        started = true;
                        e.Set();
                    });

                n.PresentFluidly<NamedPresenter>(
                    p1.Name);
                e.WaitOne();

                Assert.True(started);
            }
        }

        public class When_LoginFluidly_is_called : Context
        {
            [Fact]
            public void Accesses_the_login_latch_twice()
            {
                // once to reset
                // and once to wait on it to get set again
                var w = A.Fake<MethodWeb>();
                var n = new Navigator(w);

                n.LoginFluidly();

                A.CallTo(() => w.Run(
                        A<xofz.Do<LatchHolder>>.Ignored,
                        DependencyNames.Latch))
                    .MustHaveHappened(Repeated.Exactly.Twice);
            }

            [Fact]
            public void Presents_a_LoginPresenter_fluidly_once()
            {
                var n = new TestNavigator(
                    A.Fake<MethodWeb>());
                n.FluidlyPresentedCount = 0;

                n.LoginFluidly();

                Assert.Equal(
                    typeof(LoginPresenter),
                    n.LastFluidlyPresentedType);
                Assert.Equal(
                    1,
                    n.FluidlyPresentedCount);
            }
        }

        public class TestNavigator : Navigator
        {
            public TestNavigator(
                MethodRunner runner)
                : base(runner)
            {
            }

            public virtual byte FluidlyPresentedCount { get; set; }

            public virtual Type LastFluidlyPresentedType { get; protected set; }

            public override void PresentFluidly<T>()
            {
                base.PresentFluidly<T>();
                ++this.FluidlyPresentedCount;
                this.LastFluidlyPresentedType = typeof(T);
            }
        }
    }
}
