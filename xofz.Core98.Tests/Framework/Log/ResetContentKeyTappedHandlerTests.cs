namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using xofz.UI.Log;
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
                this.ui = A.Fake<LogUiV3>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly ResetContentKeyTappedHandler handler;
            protected readonly LogUiV3 ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Calls_ui_ResetContentFilter()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.ResetContentFilter())
                    .MustHaveHappened();
            }
        }
    }
}
