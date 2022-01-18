namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using xofz.UI.Log;
    using Xunit;

    public class UpKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new UpKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LogUiV3>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.uiRW = new UiReaderWriter();
                this.presser = A.Fake<KeyPresser>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.presser);
            }

            protected readonly MethodWeb web;
            protected readonly UpKeyTappedHandler handler;
            protected readonly LogUiV3 ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly UiReaderWriter uiRW;
            protected readonly KeyPresser presser;
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
            public void Calls_KeyPresser_Press()
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
