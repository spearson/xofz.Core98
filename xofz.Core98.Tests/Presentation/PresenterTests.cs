namespace xofz.Tests.Presentation
{
    using FakeItEasy;
    using xofz.Presentation;
    using xofz.UI;
    using Xunit;

    public class PresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<Ui>();
                this.shell = A.Fake<ShellUi>();
                this.presenter = new Presenter(
                    this.ui,
                    this.shell);
            }

            protected readonly Ui ui;
            protected readonly ShellUi shell;
            protected readonly Presenter presenter;
        }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Switches_the_ui_into_the_shell()
            {
                this.presenter.Start();

                A.CallTo(() => this.shell.SwitchUi(this.ui))
                    .MustHaveHappened();
            }
        }
    }
}
