namespace xofz.Tests.Presentation
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Shutdown;
    using xofz.Presentation;
    using Xunit;

    public class ShutdownPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.presenter = new ShutdownPresenter(
                    this.web);
                this.nav = A.Fake<Navigator>();
                this.startHandler = A.Fake<StartHandler>();

                var w = this.web;
                w.RegisterDependency(
                    this.nav);
                w.RegisterDependency(
                    this.startHandler);
            }

            protected readonly MethodWeb web;
            protected readonly ShutdownPresenter presenter;
            protected readonly Navigator nav;
            protected readonly StartHandler startHandler;
        }

        public class When_Setup_is_called : Context
        {
            [Fact]
            public void Registers_itself_with_the_Navigator()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.nav.RegisterPresenter(
                        this.presenter))
                    .MustHaveHappened();
            }
        }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Calls_StartHandler_Handle()
            {
                this.presenter.Start();

                A
                    .CallTo(() => this.startHandler.Handle())
                    .MustHaveHappened();
            }
        }
    }
}
