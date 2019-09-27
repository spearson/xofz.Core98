namespace xofz.Presentation
{
    using xofz.UI;

    public class PopupPresenter 
        : Presenter
    {
        public PopupPresenter(
            PopupUi ui) 
            : base(ui, null)
        {
            this.ui = ui;
        }

        public override void Start()
        {
            UiHelpers.WriteSync(
                this.ui,
                () =>
                {
                    try
                    {
                        this.ui.Display();
                    }
                    catch
                    {
                        // assume already visible, swallow
                    }
                });
        }

        public override void Stop()
        {
            UiHelpers.WriteSync(
                this.ui, 
                this.ui.Hide);
        }

        private readonly PopupUi ui;
    }
}
