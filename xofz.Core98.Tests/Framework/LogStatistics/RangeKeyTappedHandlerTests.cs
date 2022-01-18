namespace xofz.Tests.Framework.LogStatistics
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using xofz.UI.LogStatistics;
    using Xunit;

    public class RangeKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new RangeKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LogStatisticsUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.uiRW = new UiReaderWriter();
                this.setter = A.Fake<FilterSetter>();
                this.stats = A.Fake<LogStatistics>();
                this.settings = new SettingsHolder();
                this.labels = new Labels();
                this.displayer = A.Fake<StatsDisplayer>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.setter);
                w.RegisterDependency(
                    this.stats,
                    this.name);
                w.RegisterDependency(
                    this.settings,
                    this.name);
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.displayer);
            }

            protected readonly MethodWeb web;
            protected readonly RangeKeyTappedHandler handler;
            protected readonly LogStatisticsUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly UiReaderWriter uiRW;
            protected readonly FilterSetter setter;
            protected readonly LogStatistics stats;
            protected readonly SettingsHolder settings;
            protected readonly Labels labels;
            protected readonly StatsDisplayer displayer;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Reads_StartDate()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.StartDate)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_EndDate()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.EndDate)
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_setter_Set()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.setter.Set(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_stats_ComputeRange()
            {
                this.ui.StartDate = this.fixture.Create<DateTime>();
                this.ui.EndDate = this.fixture.Create<DateTime>();

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.stats.ComputeRange(
                        this.ui.StartDate,
                        this.ui.EndDate))
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_ui_Title()
            {
                this.ui.Title = null;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.NotNull(
                    this.ui.Title);
            }

            [Fact]
            public void Calls_displayer_Display()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.displayer.Display(
                        this.ui,
                        this.stats,
                        false))
                    .MustHaveHappened();
            }
        }
    }
}
