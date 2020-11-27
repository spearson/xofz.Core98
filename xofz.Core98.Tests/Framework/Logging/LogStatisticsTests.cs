namespace xofz.Tests.Framework.Logging
{
    using System.Collections.Generic;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.Framework.Lots;
    using Xunit;

    public class LogStatisticsTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.stats = new LogStatistics(
                    this.web);
                this.log = A.Fake<Log>();
                this.lotter = A.Fake<Lotter>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.stats.LogDependencyName = this.name;

                var w = this.web;
                w.RegisterDependency(
                    this.log,
                    this.name);
                w.RegisterDependency(
                    this.lotter,
                    xofz.Framework.Log.DependencyNames.Lotter);
            }

            protected readonly MethodWeb web;
            protected readonly LogStatistics stats;
            protected readonly Log log;
            protected readonly Lotter lotter;
            protected readonly Fixture fixture;
            protected readonly string name;
        }

        public class When_ComputeOverall_is_called : Context
        {
            [Fact]
            public void Calls_log_ReadEntries()
            {
                this.stats.ComputeOverall();

                A
                    .CallTo(() => this.log.ReadEntries())
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_lotter_Materialize()
            {
                this.stats.ComputeOverall();

                A
                    .CallTo(() => this.lotter.Materialize(
                        A<IEnumerable<LogEntry>>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Materializes_matches()
            {
                var sharedContent = this.fixture.Create<string>();
                var e1 = new LogEntry(
                    DefaultEntryTypes.Information,
                    new []
                    {
                        this.fixture.Create<string>(),
                        sharedContent
                    });
                var e2 = new LogEntry(
                    DefaultEntryTypes.Information,
                    new []
                    {
                        this.fixture.Create<string>(),
                        this.fixture.Create<string>()
                    });
                var entries = new[]
                {
                    e1,
                    e2
                };
                A
                    .CallTo(() => this.log.ReadEntries())
                    .Returns(entries);
                A
                    .CallTo(() => this.lotter.Materialize(
                        A<IEnumerable<LogEntry>>.That.Matches(es =>
                            EnumerableHelpers.Count(
                                es) == 2)))
                    .Returns(new LinkedListLot<LogEntry>(entries));
                this.stats.FilterContent = sharedContent;

                this.stats.ComputeOverall();

                A
                    .CallTo(() => this.lotter.Materialize(
                        A<IEnumerable<LogEntry>>.That.Contains(e1)))
                    .MustHaveHappened(
                        2, Times.Exactly);
                A
                    .CallTo(() => this.lotter.Materialize(
                        A<IEnumerable<LogEntry>>.That.Contains(e2)))
                    .MustHaveHappened(
                        1, Times.Exactly);
            }
        }
    }
}
