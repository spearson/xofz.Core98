namespace xofz.Tests.Framework.LogStatistics
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using xofz.UI.LogStatistics;
    using Xunit;

    public class DateResetterTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.resetter = new DateResetter(
                    this.web);
                this.ui = A.Fake<LogStatisticsUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.uiRW = new UiReaderWriter();
                this.stats = A.Fake<LogStatistics>();
                this.displayer = A.Fake<StatsDisplayer>();
                this.provider = A.Fake<TimeProvider>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.stats,
                    this.name);
                w.RegisterDependency(
                    this.displayer);
                w.RegisterDependency(
                    this.provider);

                A
                    .CallTo(() => this.provider.Now())
                    .Returns(DateTime.Now);
            }

            protected readonly MethodWeb web;
            protected readonly DateResetter resetter;
            protected readonly LogStatisticsUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly UiReaderWriter uiRW;
            protected readonly LogStatistics stats;
            protected readonly StatsDisplayer displayer;
            protected readonly TimeProvider provider;
        }

        public class When_Reset_is_called : Context
        {
            [Fact]
            public void Reads_provider_Now()
            {
                this.resetter.Reset(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.provider.Now())
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_ui_StartDate()
            {
                var now = this.fixture.Create<DateTime>();
                A
                    .CallTo(() => this.provider.Now())
                    .Returns(now);
                this.ui.StartDate = default;

                this.resetter.Reset(
                    this.ui,
                    this.name);

                Assert.Equal(
                    now.Date.Subtract(TimeSpan.FromDays(6)),
                    this.ui.StartDate);

            }

            [Fact]
            public void Sets_ui_EndDate()
            {
                var now = this.fixture.Create<DateTime>();
                A
                    .CallTo(() => this.provider.Now())
                    .Returns(now);
                this.ui.EndDate = default;

                this.resetter.Reset(
                    this.ui,
                    this.name);

                Assert.Equal(
                    now.Date,
                    this.ui.EndDate);
            }

            [Fact]
            public void Resets_stats()
            {
                this.resetter.Reset(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.stats.Reset())
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_displayer_Display()
            {
                this.resetter.Reset(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.displayer.Display(
                        this.ui,
                        this.stats,
                        true))
                    .MustHaveHappened();
            }
        }
    }
}
