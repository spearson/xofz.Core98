namespace xofz.Presentation
{
    using UI;

    public class Presenter
    {
        public const string DefaultUiFieldName = nameof(ui);

        public Presenter(
            Ui ui, 
            ShellUi shell)
        {
            this.ui = ui;
            this.shell = shell;
        }

        public virtual void Start()
        {
            var s = this.shell;
            UiHelpers.WriteSync(
                s,
                () =>
                {
                    s.SwitchUi(this.ui);
                });
        }

        public virtual void Stop()
        {
        }

        private readonly Ui ui;
        private readonly ShellUi shell;
    }
}
