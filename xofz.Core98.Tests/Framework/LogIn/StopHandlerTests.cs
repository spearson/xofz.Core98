namespace xofz.Tests.Framework.LogIn
{
    using System.Security;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using Xunit;

    public class StopHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new StopHandler(
                    this.web);
                this.ui = A.Fake<LoginUi>();
                this.uiRW = new UiReaderWriter();
                this.settings = new SettingsHolder();
                this.latch = A.Fake<LatchHolder>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.settings);
                w.RegisterDependency(
                    this.latch,
                    DependencyNames.Latch);

                this.fixture = new Fixture();
            }

            protected readonly MethodWeb web;
            protected readonly StopHandler handler;
            protected readonly LoginUi ui;
            protected readonly UiReaderWriter uiRW;
            protected readonly SettingsHolder settings;
            protected readonly LatchHolder latch;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Sets_ui_CurrentPassword_to_settings_CurrentPassword()
            {
                var pw = this.fixture.Create<SecureString>();
                this.settings.CurrentPassword = pw;

                this.handler.Handle(
                    this.ui);

                Assert.Same(
                    this.settings.CurrentPassword,
                    this.ui.CurrentPassword);
            }

            [Fact]
            public void Calls_ui_Hide()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.ui.Hide())
                    .MustHaveHappened();
            }

            [Fact]
            public void Accesses_login_latch()
            {
                // ... to set it
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.latch.Latch)
                    .MustHaveHappened();
            }
        }
    }
}
