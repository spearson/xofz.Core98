namespace xofz.Tests.Framework.Login
{
    using System.Security;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using xofz.UI.Login;
    using Xunit;

    public class StartHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new StartHandler(
                    this.web);
                this.uiRW = new UiReaderWriter();
                this.settings = new SettingsHolder();
                this.ui = A.Fake<LoginUi>();
                this.fixture = new Fixture();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.settings);
            }

            protected readonly MethodWeb web;
            protected readonly StartHandler handler;
            protected readonly UiReaderWriter uiRW;
            protected readonly SettingsHolder settings;
            protected readonly LoginUi ui;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Sets_settings_CurrentPassword_to_ui_CurrentPassword()
            {
                var pw = this.fixture.Create<SecureString>();
                this.ui.CurrentPassword = pw;

                this.handler.Handle(
                    this.ui);

                Assert.Same(
                    pw,
                    this.settings.CurrentPassword);
            }

            [Fact]
            public void Calls_ui_Display()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.ui.Display())
                    .MustHaveHappened();
            }
        }
    }
}
