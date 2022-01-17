namespace xofz.Tests.Presentation.Presenters
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.Presentation.Presenters;
    using Xunit;

    public class EmptyPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.presenter = new EmptyPresenter(
                    this.web);

                this.nav = A.Fake<Navigator>();
                var w = this.web;
                w.RegisterDependency(this.nav);
            }

            protected readonly MethodWeb web;
            protected readonly EmptyPresenter presenter;
            protected readonly Navigator nav;
        }

        public class When_Setup_is_called : Context
        {
            [Fact]
            public void Registers_itself_with_the_navigator()
            {
                this.presenter.Setup();

                A
                    .CallTo(() => this.nav.RegisterPresenter(this.presenter))
                    .MustHaveHappened();
            }
        }
    }
}
