namespace xofz.Tests.Framework.Logging
{
    using Ploeh.AutoFixture;
    using xofz.Framework.Logging;
    using Xunit;

    public class LogFactoryTests
    {
        public class Context
        {
            protected Context()
            {
                this.factory = new LogFactory();
                this.fixture = new Fixture();
            }

            protected readonly LogFactory factory;
            protected readonly Fixture fixture;
        }

        public class When_CreateTextFileLog_is_called : Context
        {
            [Fact]
            public void Creates_a_log()
            {
                Assert.NotNull(
                    this.factory.CreateTextFileLog(
                            this.fixture.Create<string>())
                        .Item1);
            }

            [Fact]
            public void Creates_a_logEditor()
            {
                Assert.NotNull(
                    this.factory.CreateTextFileLog(
                            this.fixture.Create<string>())
                        .Item2);
            }
        }

        public class When_CreateEventLogLog_is_called : Context
        {
            [Fact]
            public void Creates_a_log()
            {
                Assert.NotNull(
                    this.factory.CreateEventLogLog(
                        this.fixture.Create<string>(),
                        this.fixture.Create<string>())
                        .Item1);
            }

            [Fact]
            public void Creates_a_logEditor()
            {
                Assert.NotNull(
                    this.factory.CreateEventLogLog(
                            this.fixture.Create<string>(),
                            this.fixture.Create<string>())
                        .Item2);
            }
        }
    }
}
