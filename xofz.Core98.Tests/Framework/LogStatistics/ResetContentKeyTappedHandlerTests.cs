namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using Xunit;

    public class ResetContentKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new ResetContentKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LogStatisticsUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly ResetContentKeyTappedHandler handler;
            protected readonly LogStatisticsUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Handle1_is_called : Context
        {
            [Fact]
            public void Sets_FilterContent_to_null()
            {
                this.ui.FilterContent = this.fixture.Create<string>();

                this.handler.Handle(
                    this.ui);

                Assert.Null(
                    this.ui.FilterContent);
            }
        }

        public class When_Handle2_is_called : Context
        {
            [Fact]
            public void Sets_FilterContent_to_null()
            {
                this.ui.FilterContent = this.fixture.Create<string>();

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.Null(
                    this.ui.FilterContent);
            }
        }
    }
}
