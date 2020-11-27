namespace xofz.Tests.Framework.Login
{
    using System;
    using System.Security;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using Xunit;

    public class TimerHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new TimerHandler(
                    this.web);
                this.ui = A.Fake<LoginUi>();
                this.ac = A.Fake<AccessController>();
                this.settings = new SettingsHolder();
                this.labels = new Labels();
                this.uiRW = new UiReaderWriter();
                this.fixture = new Fixture();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.settings);
                w.RegisterDependency(
                    this.ac);
            }

            protected readonly MethodWeb web;
            protected readonly TimerHandler handler;
            protected readonly LoginUi ui;
            protected readonly AccessController ac;
            protected readonly SettingsHolder settings;
            protected readonly Labels labels;
            protected readonly UiReaderWriter uiRW;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void
                If_no_access_sets_ui_TimeRemaining_to_labels_NotLoggedIn()
            {
                this.handler.Handle(
                    this.ui);

                Assert.Equal(
                    this.labels.NotLoggedIn,
                    this.ui.TimeRemaining);
            }

            [Fact]
            public void Also_sets_settings_CurrentPassword_to_null()
            {
                this.settings.CurrentPassword =
                    this.fixture.Create<SecureString>();

                this.handler.Handle(
                    this.ui);

                Assert.Null(
                    this.settings.CurrentPassword);
            }

            [Fact]
            public void Sets_ui_CurrentAccessLevel_to_ac_CurrentAccessLevel()
            {
                var level = AccessLevel.Level13;
                A
                    .CallTo(() => this.ac.CurrentAccessLevel)
                    .Returns(level);
                this.ui.CurrentAccessLevel = AccessLevel.None;

                this.handler.Handle(
                    this.ui);

                Assert.Equal(
                    this.ac.CurrentAccessLevel,
                    this.ui.CurrentAccessLevel);
            }

            [Fact]
            public void Sets_ui_TimeRemaining()
            {
                A
                    .CallTo(() => this.ac.TimeRemaining)
                    .Returns(this.fixture.Create<TimeSpan>());
                this.ui.TimeRemaining = null;

                this.handler.Handle(
                    this.ui);

                Assert.NotNull(
                    this.ui.TimeRemaining);
                Assert.NotEmpty(
                    this.ui.TimeRemaining);
            }
        }
    }
}
