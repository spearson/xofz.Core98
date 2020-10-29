namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using Xunit;
    using SettingsHolder = xofz.Framework.Log.SettingsHolder;

    public class StartHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new StartHandler(
                    this.web);
                this.settings = new SettingsHolder();
                this.uiRW = new UiReaderWriter();
                this.ui = A.Fake<LogStatisticsUi>();
                this.readLogUi = A.Fake<Gen<LogUi>>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.resetter = A.Fake<DateResetter>();

                var w = this.web;
                w.RegisterDependency(
                    this.settings,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.resetter);
            }

            protected readonly MethodWeb web;
            protected readonly StartHandler handler;
            protected readonly SettingsHolder settings;
            protected readonly UiReaderWriter uiRW;
            protected readonly LogStatisticsUi ui;
            protected readonly Gen<LogUi> readLogUi;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly DateResetter resetter;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void If_ResetOnStart_is_true_calls_resetter_Reset()
            {
                this.settings.ResetOnStart = true;

                this.handler.Handle(
                    this.ui,
                    this.readLogUi,
                    this.name);

                A
                    .CallTo(() => this.resetter.Reset(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_filter_content()
            {
                this.handler.Handle(
                    this.ui,
                    this.readLogUi,
                    this.name);

                A
                    .CallTo(() => this.ui.FilterContent)
                    .MustHaveHappened();
            }

            [Fact]
            public void
                If_filter_content_is_missing_reads_log_ui_filterContent()
            {
                this.ui.FilterContent = null;
                var logUi = A.Fake<LogUi>();
                A
                    .CallTo(() => this.readLogUi.Invoke())
                    .Returns(logUi);

                this.handler.Handle(
                    this.ui,
                    this.readLogUi,
                    this.name);

                A
                    .CallTo(() => logUi.FilterContent)
                    .MustHaveHappened();
            }

            [Fact]
            public void Then_applies_it()
            {
                this.ui.FilterContent = null;
                var logUi = A.Fake<LogUi>();
                logUi.FilterContent = this.fixture.Create<string>();
                A
                    .CallTo(() => this.readLogUi.Invoke())
                    .Returns(logUi);

                this.handler.Handle(
                    this.ui,
                    this.readLogUi,
                    this.name);

                Assert.Equal(
                    logUi.FilterContent,
                    this.ui.FilterContent);
            }
        }
    }
}
