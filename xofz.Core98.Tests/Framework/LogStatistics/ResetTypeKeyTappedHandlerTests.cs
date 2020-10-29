namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
    using Xunit;

    public class ResetTypeKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new ResetTypeKeyTappedHandler(
                    this.web);
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.ui = A.Fake<LogStatisticsUi>();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly ResetTypeKeyTappedHandler handler;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly LogStatisticsUi ui;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Handle1_is_called : Context
        {
            [Fact]
            public void Sets_FilterType_to_null()
            {
                this.ui.FilterType = this.fixture.Create<string>();

                this.handler.Handle(
                    this.ui);

                Assert.Null(
                    this.ui.FilterType);
            }
        }

        public class When_Handle2_is_called : Context
        {
            [Fact]
            public void Sets_FilterType_to_null()
            {
                this.ui.FilterType = this.fixture.Create<string>();

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.Null(
                    this.ui.FilterType);
            }
        }
    }
}
