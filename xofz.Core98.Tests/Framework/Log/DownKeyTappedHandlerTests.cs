namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using xofz.UI.Log;
    using Xunit;

    public class DownKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new DownKeyTappedHandler(
                    this.web);
                this.uiRW = new UiReaderWriter();
                this.presser = A.Fake<KeyPresser>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.ui = A.Fake<LogUiV3>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.presser);
            }

            protected readonly MethodWeb web;
            protected readonly DownKeyTappedHandler handler;
            protected readonly UiReaderWriter uiRW;
            protected readonly KeyPresser presser;
            protected readonly LogUiV3 ui;
            protected readonly Fixture fixture;
            protected readonly string name;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Calls_ui_FocusEntries()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.FocusEntries())
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_presser_Press()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.presser.Press(
                        A<string>.Ignored))
                    .MustHaveHappened();
            }
        }
    }
}
