namespace xofz.Tests.Framework.Log
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using xofz.UI.Log;
    using Xunit;

    public class SetupHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();

                this.handler = new SetupHandler(
                    this.web);
                this.settings = new SettingsHolder();
                this.uiRW = new UiReaderWriter();
                this.provider = A.Fake<TimeProvider>();
                this.applier = A.Fake<LabelApplier>();
                this.currentHandler = A.Fake<CurrentWeekKeyTappedHandler>();
                this.ui = A.Fake<LogUi>();

                A
                    .CallTo(() => this.provider.Now())
                    .Returns(DateTime.Now);

                var w = this.web;
                w.RegisterDependency(
                    this.settings,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.provider);
                w.RegisterDependency(
                    this.applier);
                w.RegisterDependency(
                    this.currentHandler);
            }

            protected readonly MethodWeb web;
            protected readonly SetupHandler handler;
            protected readonly SettingsHolder settings;
            protected readonly UiReaderWriter uiRW;
            protected readonly TimeProvider provider;
            protected readonly LabelApplier applier;
            protected readonly CurrentWeekKeyTappedHandler currentHandler;
            protected readonly LogUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void If_EditLevel_is_none_shows_add_key()
            {
                this.ui.AddKeyVisible = false;
                this.settings.EditLevel = AccessLevel.None;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.True(
                    this.ui.AddKeyVisible);
            }

            [Fact]
            public void Otherwise_hides_add_key()
            {
                this.ui.AddKeyVisible = true;
                this.settings.EditLevel = AccessLevel.Level10;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.False(
                    this.ui.AddKeyVisible);
            }

            [Fact]
            public void If_ClearLevel_is_none_shows_clear_key()
            {
                this.ui.ClearKeyVisible = false;
                this.settings.ClearLevel = AccessLevel.None;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.True(
                    this.ui.ClearKeyVisible);
            }

            [Fact]
            public void Otherwise_hides_clear_key()
            {
                this.ui.ClearKeyVisible = true;
                this.settings.ClearLevel = AccessLevel.Level10;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.False(
                    this.ui.ClearKeyVisible);
            }

            [Fact]
            public void If_ui_is_LogUiV2_calls_applier_Apply()
            {
                var ui2 = A.Fake<LogUiV2>();

                this.handler.Handle(
                    ui2,
                    this.name);

                A
                    .CallTo(() => this.applier.Apply(
                        ui2))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_ui_LogUiV3_calls_currentHandler_Handle()
            {
                var ui3 = A.Fake<LogUiV3>();

                this.handler.Handle(
                    ui3,
                    this.name);

                A
                    .CallTo(() => this.currentHandler.Handle(
                        ui3,
                        this.name,
                        null,
                        null))
                    .MustHaveHappened();
            }
        }
    }
}
