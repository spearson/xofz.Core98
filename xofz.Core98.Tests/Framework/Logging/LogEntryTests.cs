namespace xofz.Tests.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using xofz.Framework.Logging;
    using xofz.Framework.Lots;
    using Xunit;

    public class LogEntryTests
    {
        public class Context
        {
            protected Context()
            {
                this.fixture = new Fixture();
                var relay = new TypeRelay(
                    typeof(Lot<string>),
                    typeof(LinkedListLot<string>));
                this.fixture.Customizations.Add(
                    relay);
                this.type = this.fixture.Create<string>();
                this.timestamp = this.fixture.Create<DateTime>();
            }

            protected readonly Fixture fixture;
            protected readonly string type;
            protected readonly DateTime timestamp;
            protected LogEntry entry;
        }

        public class When_Created1 : Context
        {
            [Fact]
            public void Type_is_type()
            {
                this.entry = new LogEntry(
                    this.type,
                    this.fixture.Create<IEnumerable<string>>());

                Assert.Equal(
                    this.type,
                    this.entry.Type);
            }

            [Fact]
            public void Content_is_content()
            {
                var line1 = this.fixture.Create<string>();
                var line2 = this.fixture.Create<string>();
                var content = new[]
                {
                    line1,
                    line2
                };

                this.entry = new LogEntry(
                    this.type,
                    content);

                Assert.Contains(
                    line1,
                    this.entry.Content);
                Assert.Contains(
                    line2,
                    this.entry.Content);
            }
        }

        public class When_Created2 : Context
        {
            [Fact]
            public void Type_is_type()
            {
                this.entry = new LogEntry(
                    this.type,
                    this.fixture.Create<Lot<string>>());

                Assert.Equal(
                    this.type,
                    this.entry.Type);
            }

            [Fact]
            public void Content_is_content()
            {
                var content = this.fixture.Create<Lot<string>>();
                this.entry = new LogEntry(
                    this.type,
                    content);

                Assert.Same(
                    content,
                    this.entry.Content);
            }
        }

        public class When_Created3 : Context
        {
            [Fact]
            public void Type_is_type()
            {
                this.entry = new LogEntry(
                    this.fixture.Create<DateTime>(),
                    this.type,
                    this.fixture.Create<IEnumerable<string>>());

                Assert.Equal(
                    this.type,
                    this.entry.Type);
            }

            [Fact]
            public void Content_is_content()
            {
                var line1 = this.fixture.Create<string>();
                this.entry = new LogEntry(
                    this.fixture.Create<DateTime>(),
                    this.type,
                    new[]
                    {
                        line1
                    });

                Assert.Contains(
                    line1,
                    this.entry.Content);
            }

            [Fact]
            public void Timestamp_is_timestamp()
            {
                this.entry = new LogEntry(
                    this.timestamp,
                    this.type,
                    this.fixture.Create<IEnumerable<string>>());

                Assert.Equal(
                    this.timestamp,
                    this.entry.Timestamp);
            }
        }

        public class When_Created4 : Context
        {
            [Fact]
            public void Type_is_type()
            {
                this.entry = new LogEntry(
                    this.timestamp,
                    this.type,
                    this.fixture.Create<Lot<string>>());

                Assert.Equal(
                    this.type,
                    this.entry.Type);
            }

            [Fact]
            public void Content_is_content()
            {
                var content = this.fixture.Create<Lot<string>>();
                this.entry = new LogEntry(
                    this.timestamp,
                    this.type,
                    content);

                Assert.Same(
                    content,
                    this.entry.Content);
            }

            [Fact]
            public void Timestamp_is_timestamp()
            {
                this.entry = new LogEntry(
                    this.timestamp,
                    this.type,
                    this.fixture.Create<Lot<string>>());

                Assert.Equal(
                    this.timestamp,
                    this.entry.Timestamp);
            }
        }
    }
}
