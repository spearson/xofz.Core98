namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using Xunit;

    public class OverallKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new OverallKeyTappedHandler(
                    this.web);

                this.ui = A.Fake<LogStatisticsUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.stats = A.Fake<LogStatistics>();
                this.setter = A.Fake<FilterSetter>();
                this.uiRW = new UiReaderWriter();
                this.labels = new Labels();
                this.displayer = A.Fake<StatsDisplayer>();

                var w = this.web;
                w.RegisterDependency(
                    this.stats,
                    this.name);
                w.RegisterDependency(
                    this.setter);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.displayer);
            }

            protected readonly MethodWeb web;
            protected readonly OverallKeyTappedHandler handler;
            protected readonly LogStatisticsUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly LogStatistics stats;
            protected readonly FilterSetter setter;
            protected readonly UiReaderWriter uiRW;
            protected readonly Labels labels;
            protected readonly StatsDisplayer displayer;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Calls_setter_Set()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.setter.Set(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_stats_ComputeOverall()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.stats.ComputeOverall())
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_ui_Title_to_labels_Overall()
            {
                this.ui.Title = null;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.Equal(
                    this.labels.Overall,
                    this.ui.Title);
            }

            [Fact]
            public void Calls_displayer_Display()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.displayer.Display(
                        this.ui,
                        this.stats,
                        false))
                    .MustHaveHappened();
            }
        }
    }
}
