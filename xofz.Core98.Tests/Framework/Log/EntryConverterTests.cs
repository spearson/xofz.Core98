namespace xofz.Tests.Framework.Log
{
    using System;
    using System.Globalization;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using Xunit;

    public class EntryConverterTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.converter = new EntryConverter(
                    this.web);
                this.fixture = new Fixture();
                this.entry = new LogEntry(
                    this.fixture.Create<DateTime>(),
                    this.fixture.Create<string>(),
                    new[]
                    {
                        this.fixture.Create<string>(),
                        this.fixture.Create<string>()
                    });
                this.settings = new SettingsHolder();
                this.name = this.fixture.Create<string>();

                var w = this.web;
                w.RegisterDependency(
                    this.settings,
                    this.name);
            }

            protected readonly MethodWeb web;
            protected readonly EntryConverter converter;
            protected readonly Fixture fixture;
            protected readonly LogEntry entry;
            protected readonly SettingsHolder settings;
            protected readonly string name;
        }

        public class When_Convert_is_called : Context
        {
            [Fact]
            public void Timestamp_is_first_item()
            {
                var xt = this.converter.Convert(
                    this.entry,
                    this.name);

                Assert.Equal(
                    this.entry.Timestamp.ToString(
                        this.settings.TimestampFormat,
                        CultureInfo.CurrentCulture),
                    xt.Item1);
            }

            [Fact]
            public void Type_is_second_item()
            {
                var xt = this.converter.Convert(
                    this.entry,
                    this.name);

                Assert.Equal(
                    this.entry.Type,
                    xt.Item2);
            }

            [Fact]
            public void Third_item_contains_Content()
            {
                var xt = this.converter.Convert(
                    this.entry,
                    this.name);

                foreach (var line in this.entry.Content)
                {
                    Assert.Contains(
                        line,
                        xt.Item3);
                }
            }
        }
    }
}
