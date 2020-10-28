namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
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
                this.ui = A.Fake<LogUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.settings = new SettingsHolder();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.handler);
                w.RegisterDependency(
                    this.settings,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly AccessLevelChangedHandler handler;
            protected readonly LogUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly SettingsHolder settings;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void If_newLevel_bigger_than_edit_level_shows_add_key()
            {
                this.settings.EditLevel = AccessLevel.Level10;
                this.ui.AddKeyVisible = false;

                this.handler.Handle(
                    this.ui,
                    this.settings.EditLevel,
                    this.name);

                Assert.True(
                    this.ui.AddKeyVisible);
            }

            [Fact]
            public void Otherwise_hides_add_key()
            {
                this.settings.EditLevel = AccessLevel.Level6;
                this.ui.AddKeyVisible = true;

                this.handler.Handle(
                    this.ui,
                    AccessLevel.Level5,
                    this.name);

                Assert.False(
                    this.ui.AddKeyVisible);
            }

            [Fact]
            public void If_newLevel_bigger_than_clear_level_shows_clear_key()
            {
                this.settings.ClearLevel = AccessLevel.Level17;
                this.ui.ClearKeyVisible = false;

                this.handler.Handle(
                    this.ui,
                    this.settings.ClearLevel,
                    this.name);

                Assert.True(
                    this.ui.ClearKeyVisible);
            }

            [Fact]
            public void Otherwise_hides_clear_key()
            {
                this.settings.ClearLevel = AccessLevel.Level12;
                this.ui.ClearKeyVisible = true;

                this.handler.Handle(
                    this.ui,
                    AccessLevel.Level11,
                    this.name);

                Assert.False(
                    this.ui.ClearKeyVisible);
            }
        }
    }
}
