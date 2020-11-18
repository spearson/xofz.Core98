namespace xofz.Tests.Framework.LogIn
{
    using System.Security;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using Xunit;

    public class AccessLevelChangedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new AccessLevelChangedHandler(
                    this.web);
                this.ui = A.Fake<LoginUi>();
                this.uiRW = new UiReaderWriter();
                this.timer = A.Fake<Timer>();
                this.raiser = A.Fake<EventRaiser>();
                this.fixture = new Fixture();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.timer,
                    DependencyNames.Timer);
                w.RegisterDependency(
                    this.raiser);
            }

            protected readonly MethodWeb web;
            protected readonly AccessLevelChangedHandler handler;
            protected readonly LoginUi ui;
            protected readonly UiReaderWriter uiRW;
            protected readonly Timer timer;
            protected readonly EventRaiser raiser;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void If_no_access_sets_ui_CurrentPassword_to_null()
            {
                this.ui.CurrentPassword = this.fixture.Create<SecureString>();

                this.handler.Handle(
                    this.ui,
                    AccessLevel.None);

                Assert.Null(
                    this.ui.CurrentPassword);
            }

            [Fact]
            public void Raises_timer_Elapsed()
            {
                this.handler.Handle(
                    this.ui,
                    AccessLevel.Level1);

                A
                    .CallTo(() => this.raiser.Raise(
                        this.timer,
                        nameof(this.timer.Elapsed)))
                    .MustHaveHappened();
            }
        }
    }
}
