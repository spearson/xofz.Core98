namespace xofz.Tests.Framework.Log
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using xofz.UI.Log;
    using xofz.UI.LogStatistics;
    using Xunit;

    public class CurrentWeekKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new CurrentWeekKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LogUiV3>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.subscribe = A.Fake<Do>();
                this.unsubscribe = A.Fake<Do>();
                this.uiRW = new UiReaderWriter();
                this.provider = A.Fake<TimeProvider>();
                this.statsUi = A.Fake<LogStatisticsUi>();
                this.eventRaiser = A.Fake<EventRaiser>();
                this.samsUltimateDate = this.fixture.Create<DateTime>().Date;
                A
                    .CallTo(() => this.provider.Now())
                    .Returns(samsUltimateDate);

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.provider);
                w.RegisterDependency(
                    this.statsUi,
                    this.name);
                w.RegisterDependency(
                    this.eventRaiser);
            }

            protected readonly MethodWeb web;
            protected readonly CurrentWeekKeyTappedHandler handler;
            protected readonly LogUiV3 ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly Do subscribe;
            protected readonly Do unsubscribe;
            protected readonly UiReaderWriter uiRW;
            protected readonly TimeProvider provider;
            protected readonly LogStatisticsUi statsUi;
            protected readonly EventRaiser eventRaiser;
            protected readonly DateTime samsUltimateDate;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Calls_TimeProvider_Now()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                A
                    .CallTo(() => this.provider.Now())
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_unsubscribe()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                A
                    .CallTo(() => this.unsubscribe.Invoke())
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_ui_StartDate()
            {
                this.ui.StartDate = default;

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    this.samsUltimateDate.AddDays(-6),
                    this.ui.StartDate);
            }

            [Fact]
            public void Sets_ui_EndDate()
            {
                this.ui.EndDate = default;

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    this.samsUltimateDate,
                    this.ui.EndDate);
            }

            [Fact]
            public void Sets_statsUi_StartDate()
            {
                this.statsUi.StartDate = default;

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    this.samsUltimateDate.AddDays(-6),
                    this.statsUi.StartDate);
            }

            [Fact]
            public void Sets_statsUi_EndDate()
            {
                this.statsUi.EndDate = default;

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    this.samsUltimateDate,
                    this.ui.EndDate);
            }
        }
    }
}
