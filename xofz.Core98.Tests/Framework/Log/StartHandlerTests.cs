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

    public class StartHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new StartHandler(
                    this.web);
                this.fields = new FieldHolder();
                this.log = A.Fake<Log>();
                this.settings = new SettingsHolder();
                this.reloader = A.Fake<EntryReloader>();
                this.resetter = A.Fake<DateAndFilterResetter>();
                this.refreshEntries = new LinkedList<LogEntry>();
                this.uiRW = new UiReaderWriter();
                this.converter = A.Fake<EntryConverter>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.ui = A.Fake<LogUi>();
                this.unsubscribe = A.Fake<Do>();
                this.subscribe = A.Fake<Do>();
                this.fields.startedFirstTimeIf1 = 1;

                var w = this.web;
                w.RegisterDependency(
                    this.fields,
                    this.name);
                w.RegisterDependency(
                    this.log,
                    this.name);
                w.RegisterDependency(
                    this.settings,
                    this.name);
                w.RegisterDependency(
                    this.reloader);
                w.RegisterDependency(
                    this.resetter);
                w.RegisterDependency(
                    this.refreshEntries,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.converter);
            }

            protected readonly MethodWeb web;
            protected readonly StartHandler handler;
            protected readonly FieldHolder fields;
            protected readonly Log log;
            protected readonly SettingsHolder settings;
            protected readonly EntryReloader reloader;
            protected readonly DateAndFilterResetter resetter;
            protected readonly ICollection<LogEntry> refreshEntries;
            protected readonly UiReaderWriter uiRW;
            protected readonly EntryConverter converter;
            protected readonly string name;
            protected readonly Fixture fixture;
            protected readonly LogUi ui;
            protected readonly Do unsubscribe;
            protected readonly Do subscribe;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Sets_fields_startedIf1_to_1()
            {
                this.fields.startedIf1 = 0;
                
                this.handler.Handle(
                    this.ui,
                    this.unsubscribe,
                    this.subscribe,
                    this.name);

                Assert.Equal(
                    this.fields.startedIf1,
                    1);
            }

            [Fact]
            public void If_startedFirstTimeIf1_is_not_1_calls_EntryReloader_Reload()
            {
                this.fields.startedFirstTimeIf1 = 0;

                this.handler.Handle(
                    this.ui,
                    this.unsubscribe,
                    this.subscribe,
                    this.name);

                A
                    .CallTo(() => this.reloader.Reload(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_ResetOnStart_calls_resetter_Reset()
            {
                this.settings.ResetOnStart = true;

                this.handler.Handle(
                    this.ui,
                    this.unsubscribe,
                    this.subscribe,
                    this.name);

                A
                    .CallTo(() => this.resetter.Reset(
                        this.ui,
                        this.unsubscribe,
                        this.subscribe,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_converter_Convert_on_refresh_entries()
            {
                var entry = new LogEntry(
                    DefaultEntryTypes.FailureAudit,
                    new []
                    {
                        this.fixture.Create<string>()
                    });
                this.refreshEntries.Add(entry);

                this.handler.Handle(
                    this.ui,
                    this.unsubscribe,
                    this.subscribe,
                    this.name);

                A
                    .CallTo(() => this.converter.Convert(
                        entry,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_ui_AddToTop_on_converted_refresh_entries()
            {
                var entry = new LogEntry(
                    DefaultEntryTypes.FailureAudit,
                    new[]
                    {
                        this.fixture.Create<string>()
                    });
                this.refreshEntries.Add(entry);

                this.handler.Handle(
                    this.ui,
                    this.unsubscribe,
                    this.subscribe,
                    this.name);

                A
                    .CallTo(() => this.ui.AddToTop(
                        A<XTuple<string, string, string>>.Ignored))
                    .MustHaveHappened();
            }
        }
    }
}
