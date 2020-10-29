namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using Xunit;

    public class StatsDisplayerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.displayer = new StatsDisplayer(
                    this.web);
                this.ui = A.Fake<LogStatisticsUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.reset = this.fixture.Create<bool>();
                this.stats = A.Fake<LogStatistics>();
                this.stats.LogDependencyName = this.name;
                this.settings = new SettingsHolder();
                this.labels = new Labels();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.displayer);
                w.RegisterDependency(
                    this.stats,
                    this.name);
                w.RegisterDependency(
                    this.settings,
                    this.name);
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly StatsDisplayer displayer;
            protected readonly LogStatisticsUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly bool reset;
            protected readonly LogStatistics stats;
            protected readonly SettingsHolder settings;
            protected readonly Labels labels;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Display_is_called : Context
        {
            [Fact]
            public void Reads_stats_OldestTimestamp()
            {
                this.displayer.Display(
                    this.ui,
                    this.name,
                    this.reset);

                A
                    .CallTo(() => this.stats.OldestTimestamp)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_stats_NewestTimestamp()
            {
                this.displayer.Display(
                    this.ui,
                    this.name,
                    this.reset);

                A
                    .CallTo(() => this.stats.NewestTimestamp)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_stats_EarliestTimestamp()
            {
                this.displayer.Display(
                    this.ui,
                    this.name,
                    this.reset);

                A
                    .CallTo(() => this.stats.EarliestTimestamp)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_stats_LatestTimestamp()
            {
                this.displayer.Display(
                    this.ui,
                    this.name,
                    this.reset);

                A
                    .CallTo(() => this.stats.LatestTimestamp)
                    .MustHaveHappened();
            }

            [Fact]
            public void If_reset_is_false_reads_stats_AvgEntriesPerDay()
            {
                this.displayer.Display(
                    this.ui,
                    this.name,
                    false);

                A
                    .CallTo(() => this.stats.AvgEntriesPerDay)
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_ui_TotalEntryCount()
            {
                this.ui.TotalEntryCount = null;

                this.displayer.Display(
                    this.ui,
                    this.stats,
                    this.reset);

                Assert.NotNull(
                    this.ui.TotalEntryCount);
            }

            [Fact]
            public void Sets_ui_OldestTimestamp()
            {
                this.ui.OldestTimestamp = null;

                this.displayer.Display(
                    this.ui,
                    this.stats,
                    this.reset);

                Assert.NotNull(
                    this.ui.OldestTimestamp);
            }

            [Fact]
            public void Sets_ui_NewestTimestamp()
            {
                this.ui.NewestTimestamp = null;

                this.displayer.Display(
                    this.ui,
                    this.stats,
                    this.reset);

                Assert.NotNull(
                    this.ui.NewestTimestamp);
            }

            [Fact]
            public void Sets_ui_EarliestTimestamp()
            {
                this.ui.EarliestTimestamp = null;

                this.displayer.Display(
                    this.ui,
                    this.name,
                    this.reset);

                Assert.NotNull(
                    this.ui.EarliestTimestamp);
            }

            [Fact]
            public void Sets_ui_LatestTimestamp()
            {
                this.ui.LatestTimestamp = null;

                this.displayer.Display(
                    this.ui,
                    this.name,
                    this.reset);

                Assert.NotNull(
                    this.ui.LatestTimestamp);
            }

            [Fact]
            public void Sets_ui_AvgEntriesPerDay()
            {
                this.ui.AvgEntriesPerDay = null;

                this.displayer.Display(
                    this.ui,
                    this.name,
                    this.reset);

                Assert.NotNull(
                    this.ui.AvgEntriesPerDay);
            }
        }
    }
}
