namespace xofz.Tests.Framework.Log
{
    using System.Collections.Generic;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.UI;
    using Xunit;

    public class EntryWrittenHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new EntryWrittenHandler(
                    this.web);
                this.ui = A.Fake<LogUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.entry = new LogEntry(
                    this.fixture.Create<string>(),
                    A.Fake<Lot<string>>());
                this.fields = new FieldHolder();
                this.uiRW = new UiReaderWriter();
                this.checker = A.Fake<FilterChecker>();
                this.provider = A.Fake<TimeProvider>();
                this.refreshEntries = A.Fake<ICollection<LogEntry>>();
                this.converter = A.Fake<EntryConverter>();

                var w = this.web;
                w.RegisterDependency(
                    this.fields,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.checker);
                w.RegisterDependency(
                    this.provider);
                w.RegisterDependency(
                    this.refreshEntries,
                    this.name);
                w.RegisterDependency(
                    this.converter);
            }

            protected readonly MethodWeb web;
            protected readonly EntryWrittenHandler handler;
            protected readonly LogUi ui;
            protected readonly string name;
            protected readonly Fixture fixture;
            protected readonly LogEntry entry;
            protected readonly FieldHolder fields;
            protected readonly UiReaderWriter uiRW;
            protected readonly FilterChecker checker;
            protected readonly TimeProvider provider;
            protected readonly ICollection<LogEntry> refreshEntries;
            protected readonly EntryConverter converter;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Calls_provider_Now()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.entry);

                A
                    .CallTo(() => this.provider.Now())
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_ui_EndDate()
            {
                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.entry);

                A
                    .CallTo(() => this.ui.EndDate)
                    .MustHaveHappened();
            }

            [Fact]
            public void
                If_not_started_but_started_once_and_filters_pass_adds_to_refresh_entries()
            {
                this.fields.startedIf1 = 0;
                this.fields.startedFirstTimeIf1 = 1;
                A
                    .CallTo(() => this.checker.PassesFilters(
                        this.ui,
                        this.entry))
                    .Returns(true);

                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.entry);

                A
                    .CallTo(() => this.refreshEntries.Add(entry))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_started_and_filters_pass_adds_entry_to_top()
            {
                this.fields.startedIf1 = 1;
                var xt = A.Fake<XTuple<string, string, string>>();
                A
                    .CallTo(() => this.converter.Convert(
                        this.entry,
                        this.name))
                    .Returns(xt);
                A
                    .CallTo(() => this.checker.PassesFilters(
                        this.ui,
                        this.entry))
                    .Returns(true);


                this.handler.Handle(
                    this.ui,
                    this.name,
                    this.entry);

                A
                    .CallTo(() => this.ui.AddToTop(
                        xt))
                    .MustHaveHappened();
            }
        }
    }
}
