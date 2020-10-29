namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using Xunit;

    public class FilterSetterTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.setter = new FilterSetter(
                    this.web);
                this.ui = A.Fake<LogStatisticsUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.stats = A.Fake<LogStatistics>();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.stats,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly FilterSetter setter;
            protected readonly LogStatisticsUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly LogStatistics stats;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Set_is_called : Context
        {
            [Fact]
            public void Sets_FilterContent()
            {
                this.stats.FilterContent = null;
                this.ui.FilterContent = this.fixture.Create<string>();

                this.setter.Set(
                    this.ui,
                    this.name);

                Assert.Equal(
                    this.stats.FilterContent,
                    this.ui.FilterContent);
            }

            [Fact]
            public void Sets_FilterType()
            {
                this.stats.FilterType = null;
                this.ui.FilterType = this.fixture.Create<string>();

                this.setter.Set(
                    this.ui,
                    this.name);

                Assert.Equal(
                    this.stats.FilterType,
                    this.ui.FilterType);
            }
        }
    }
}
