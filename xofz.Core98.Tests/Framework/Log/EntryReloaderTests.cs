namespace xofz.Tests.Framework.Log
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.UI;
    using Xunit;

    public class EntryReloaderTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWebV2();
                this.reloader = new EntryReloader(
                    this.web);
                this.log = A.Fake<Log>();
                this.uiRW = new UiReaderWriter();
                this.converter = A.Fake<EntryConverter>();
                this.ui = A.Fake<LogUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.refreshEntries = A.Fake<ICollection<LogEntry>>();

                var w = this.web;
                w.RegisterDependency(
                    this.log,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.converter);
                w.RegisterDependency(
                    this.refreshEntries,
                    this.name);

                var startDate = this.fixture.Create<DateTime>();
                var endDate = startDate.AddDays(4);
                this.ui.StartDate = startDate;
                this.ui.EndDate = endDate;

                allEntries = new LinkedList<LogEntry>();
                this.allEntries.Add(
                    new LogEntry(
                        this.ui.StartDate.AddDays(1),
                        DefaultEntryTypes.FailureAudit,
                        new[]
                        {
                            this.fixture.Create<string>(),
                            this.fixture.Create<string>()
                        }));
                this.allEntries.Add(
                    new LogEntry(
                        this.ui.StartDate.AddDays(2),
                        DefaultEntryTypes.SuccessAudit,
                        new[]
                        {
                            this.fixture.Create<string>(),
                            this.fixture.Create<string>()
                        }));
                A
                    .CallTo(() => this.log.ReadEntries())
                    .Returns(this.allEntries);
            }

            protected readonly MethodWebV2 web;
            protected readonly EntryReloader reloader;
            protected readonly Log log;
            protected readonly UiReaderWriter uiRW;
            protected EntryConverter converter;
            protected readonly LogUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly ICollection<LogEntry> refreshEntries;
            protected readonly ICollection<LogEntry> allEntries;
        }

        public class When_Reload_is_called : Context
        {
            [Fact]
            public void Accesses_filter_content()
            {
                this.reloader.Reload(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.FilterContent)
                    .MustHaveHappened();
            }

            [Fact]
            public void Accesses_filter_type()
            {
                this.reloader.Reload(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.FilterType)
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_log_ReadEntries()
            {
                this.reloader.Reload(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.log.ReadEntries())
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_converter_Convert()
            {
                this.reloader.Reload(
                    this.ui,
                    this.name);

                foreach (var entry in this.allEntries)
                {
                    A
                        .CallTo(() => this.converter.Convert(
                            entry,
                            this.name))
                        .MustHaveHappened();
                }
            }

            [Fact]
            public void Sets_ui_Entries()
            {
                this.converter = new EntryConverter(
                    this.web);
                var w = this.web;
                w.Unregister<EntryConverter>();
                w.RegisterDependency(
                    this.converter);
                var settings = new SettingsHolder();
                w.RegisterDependency(
                    settings,
                    this.name);
                this.ui.Entries = null;

                this.reloader.Reload(
                    this.ui,
                    this.name);

                Assert.NotNull(
                    this.ui.Entries);
                var e = this.ui.Entries.GetEnumerator();
                foreach (var entry in EnumerableHelpers.OrderByDescending(
                    this.allEntries,
                    en => en.Timestamp))
                {
                    e.MoveNext();
                    var xt = e.Current;
                    if (xt == null)
                    {
                        Assert.True(false);
                        return;
                    }

                    Assert.Equal(
                        xt.Item1,
                        entry.Timestamp.ToString(
                            settings.TimestampFormat,
                            CultureInfo.CurrentCulture));
                    Assert.Equal(
                        xt.Item2,
                        entry.Type);
                    foreach (var line in entry.Content)
                    {
                        Assert.Contains(
                            line,
                            xt.Item3);
                    }
                }

                e.Dispose();
            }

            [Fact]
            public void Clears_RefreshEntries()
            {
                this.reloader.Reload(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.refreshEntries.Clear())
                    .MustHaveHappened();
            }
        }
    }
}
