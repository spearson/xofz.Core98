namespace xofz.Tests.Framework.Log
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.UI;
    using Xunit;

    public class FilterCheckerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.checker = new FilterChecker(
                    this.web);
                this.fixture = new Fixture();
                this.startDate = this.fixture.Create<DateTime>();
                this.endDate = this.startDate.AddDays(7);
                this.ui = A.Fake<LogUi>();
                this.entry = new LogEntry(
                    this.startDate.AddDays(1),
                    DefaultEntryTypes.Error,
                    new []
                    {
                        this.fixture.Create<string>(),
                        this.fixture.Create<string>()
                    });
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                A
                    .CallTo(() => this.ui.StartDate)
                    .Returns(this.startDate);
                A
                    .CallTo(() => this.ui.EndDate)
                    .Returns(this.endDate);
            }

            protected readonly MethodWeb web;
            protected readonly FilterChecker checker;
            protected readonly LogUi ui;
            protected readonly LogEntry entry;
            protected readonly UiReaderWriter uiRW;
            protected readonly DateTime startDate, endDate;
            protected readonly Fixture fixture;
        }

        public class When_PassesFilters_is_called : Context
        {
            [Fact]
            public void Reads_StartDate()
            {
                this.checker.PassesFilters(
                    this.ui,
                    this.entry);

                A
                    .CallTo(() => this.ui.StartDate)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_EndDate()
            {
                this.checker.PassesFilters(
                    this.ui,
                    this.entry);

                A
                    .CallTo(() => this.ui.EndDate)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_FilterType()
            {
                this.checker.PassesFilters(
                    this.ui,
                    this.entry);

                A
                    .CallTo(() => this.ui.FilterType)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_FilterContent()
            {
                this.ui.FilterType = DefaultEntryTypes.Error;
                this.checker.PassesFilters(
                    this.ui,
                    this.entry);

                A
                    .CallTo(() => this.ui.FilterContent)
                    .MustHaveHappened();
            }

            [Fact]
            public void Returns_false_for_invalid_filter_type()
            {
                this.ui.FilterType = this.fixture.Create<string>();

                Assert.False(this.checker.PassesFilters(
                    this.ui,
                    this.entry));
            }

            [Fact]
            public void Returns_false_for_invalid_filter_content()
            {
                this.ui.FilterType = DefaultEntryTypes.Error;
                this.ui.FilterContent = this.fixture.Create<string>();

                Assert.False(this.checker.PassesFilters(
                    this.ui,
                    this.entry));
            }
        }
    }
}
