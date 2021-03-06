namespace xofz.Tests.Presentation
{
    using FakeItEasy;
    using xofz.Presentation;
    using xofz.UI;
    using Xunit;

    public class PopupPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<PopupUi>();
                this.presenter = new PopupPresenter(this.ui);
            }

            protected readonly PopupUi ui;
            protected readonly PopupPresenter presenter;
        }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Displays_the_popup_ui()
            {
                this.presenter.Start();

                A.CallTo(() => this.ui.Display())
                    .MustHaveHappened();
            }
        }

        public class When_Stop_is_called : Context
        {
            [Fact]
            public void Hides_the_popup_ui()
            {
                this.presenter.Stop();

                A.CallTo(() => this.ui.Hide())
                    .MustHaveHappened();
            }
        }
    }
}
