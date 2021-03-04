namespace xofz.Tests.Framework.Log
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using Xunit;

    public class PreviousWeekKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new PreviousWeekKeyTappedHandler(
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
                    this.uiRW);
                w.RegisterDependency(
                    this.statsUi,
                    this.name);
                w.RegisterDependency(
                    this.raiser);
                w.RegisterDependency(
                    this.provider);

                this.ui.StartDate = this.fixture.Create<DateTime>();
                this.ui.EndDate = this.fixture.Create<DateTime>();
            }

            protected readonly MethodWeb web;
            protected readonly PreviousWeekKeyTappedHandler handler;
            protected readonly LogUiV3 ui;
            protected readonly Fixture fixture;
            protected readonly string name;
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
            public void Reads_StartDate()
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
            public void Reads_EndDate()
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
            public void Sets_ui_StartDate_to_new_start_date()
            {
                var newStartDate = this.fixture.Create<DateTime>();
                this.ui.StartDate = newStartDate;

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    newStartDate.AddDays(-7),
                    this.ui.StartDate);
            }

            [Fact]
            public void Sets_ui_EndDate_to_new_end_date()
            {
                var newEndDate = this.fixture.Create<DateTime>();
                this.ui.EndDate = newEndDate;

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    newEndDate.AddDays(-7),
                    this.ui.EndDate);
            }

            [Fact]
            public void Sets_statsUi_StartDate_to_new_start_date()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    this.ui.StartDate,
                    this.statsUi.StartDate);
            }

            [Fact]
            public void Sets_statsUi_EndDate_to_new_end_date()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.unsubscribe,
                    this.subscribe);

                Assert.Equal(
                    this.ui.EndDate,
                    this.statsUi.EndDate);
            }

            [Fact]
            public void Invokes_unsubscribe()
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
            public void Invokes_subscribe()
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
            public void Raises_ui_DateRangeChanged()
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
