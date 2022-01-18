namespace xofz.Tests.Presentation.Presenters
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Main;
    using xofz.Presentation;
    using xofz.Presentation.Presenters;
    using xofz.UI;
    using xofz.UI.Main;
    using Xunit;

    public class MainPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<MainUi>();
                this.web = new MethodWebV2();
                this.presenter = new MainPresenter(
                    this.ui,
                    this.web);
                this.sub = new EventSubscriber();
                this.nav = A.Fake<Navigator>();
                this.shutdownHandler = A.Fake<ShutdownRequestedHandler>();

                var w = this.web;
                w.RegisterDependency(
                    this.sub);
                w.RegisterDependency(
                    this.nav);
                w.RegisterDependency(
                    this.shutdownHandler);
            }

            protected readonly MainUi ui;
            protected readonly MethodWebV2 web;
            protected readonly MainPresenter presenter;
            protected EventSubscriber sub;
            protected readonly Navigator nav;
            protected readonly ShutdownRequestedHandler shutdownHandler;
        }

        public class When_Setup_is_called : Context
        {
            public When_Setup_is_called()
            {
                this.sub = A.Fake<EventSubscriber>();
                var w = this.web;
                w.Unregister<EventSubscriber>();
                w.RegisterDependency(
                    this.sub);
            }

            [Fact]
            public void Subscribes_to_ShutdownRequested()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.sub.Subscribe(
                        this.ui,
                        nameof(this.ui.ShutdownRequested),
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_with_the_navigator()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.nav.RegisterPresenter(
                        this.presenter))
                    .MustHaveHappened();
            }
        }

        public class When_a_shutdown_is_requested : Context
        {
            [Fact]
            public void Calls_ShutdownRequestedHandler_Handle()
            {
                this.presenter.Setup();

                this.ui.ShutdownRequested += Raise.FreeForm.With();

                A
                    .CallTo(() => this.shutdownHandler.Handle(
                        this.ui,
                        A<Do>.Ignored,
                        A<Do>.Ignored))
                    .MustHaveHappened();
            }
        }
    }
}
