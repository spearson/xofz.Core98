namespace xofz.Tests.Framework.Log
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using Xunit;

    public class NextWeekKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new NextWeekKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LogUiV3>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.unsubscribe = A.Fake<Do>();
                this.subscribe = A.Fake<Do>();
                this.uiRW = new UiReaderWriter();
                this.provider = new TimeProvider();
                this.statsUi = A.Fake<LogStatisticsUi>();
                this.raiser = A.Fake<EventRaiser>();

                var w = this.web;
                w.RegisterDependency(
                    uiRW);
                w.RegisterDependency(
                    this.statsUi,
                    this.name);
                w.RegisterDependency(
                    this.raiser);
                w.RegisterDependency(
                    this.provider);
            }

            protected readonly MethodWeb web;
            protected readonly NextWeekKeyTappedHandler handler;
            protected readonly LogUiV3 ui;
            protected readonly string name;
            protected readonly Fixture fixture;
            protected readonly Do unsubscribe;
            protected readonly Do subscribe;
            protected readonly UiReaderWriter uiRW;
            protected readonly TimeProvider provider;
            protected readonly LogStatisticsUi statsUi;
            protected readonly EventRaiser raiser;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Reads_ui_StartDate()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                A
                    .CallTo(() => this.ui.StartDate)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_ui_EndDate()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                A
                    .CallTo(() => this.ui.EndDate)
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_statsUi_StartDate_to_new_start_date()
            {
                this.ui.StartDate = this.fixture.Create<DateTime>();
                var newStartDate = this.ui.StartDate.AddDays(7);

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    newStartDate,
                    this.ui.StartDate);

            }

            [Fact]
            public void Sets_statsUi_EndDate_to_new_end_date()
            {
                this.ui.EndDate = this.fixture.Create<DateTime>();
                var newEndDate = this.ui.EndDate.AddDays(7);

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    newEndDate,
                    this.ui.EndDate);
            }

            [Fact]
            public void Calls_subscribe()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                A
                    .CallTo(() => this.subscribe.Invoke())
                    .MustHaveHappened();
            }

            [Fact]
            public void Raises_DateRangeChanged()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                A
                    .CallTo(() => this.raiser.Raise(
                        this.ui,
                        nameof(this.ui.DateRangeChanged)))
                    .MustHaveHappened();
            }
        }
    }
}
