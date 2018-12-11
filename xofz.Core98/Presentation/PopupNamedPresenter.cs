namespace xofz.Presentation
{
    using xofz.UI;

    public class PopupNamedPresenter : NamedPresenter
    {
        public PopupNamedPresenter(
            PopupUi ui) 
            : base(ui, null)
        {
            this.ui = ui;
        }

        public override void Start()
        {
            try
            {
                UiHelpers.WriteSync(
                    this.ui,
                    this.ui.Display);
            }
            catch
            {
                // assume already visible, swallow
            }
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
