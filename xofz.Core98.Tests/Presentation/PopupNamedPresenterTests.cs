namespace xofz.Tests.Presentation
{
    using FakeItEasy;
    using xofz.Presentation;
    using xofz.UI;
    using Xunit;

    public class PopupNamedPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.popup = A.Fake<PopupUi>();
                this.presenter = new PopupNamedPresenter(
                    this.popup);
            }

            protected readonly PopupUi popup;
            protected readonly PopupNamedPresenter presenter;
        }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Calls_ui_Display()
            {
                this.presenter.Start();

                A
                    .CallTo(() => this.popup.Display())
                    .MustHaveHappened();
            }
        }

        public class When_Stop_is_called : Context
        {
            [Fact]
            public void Calls_ui_Hide()
            {
                this.presenter.Stop();

                A
                    .CallTo(() => this.popup.Hide())
                    .MustHaveHappened();
            }
        }
    }
}
