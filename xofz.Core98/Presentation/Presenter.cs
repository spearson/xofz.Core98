namespace xofz.Presentation
{
    using UI;

    public class Presenter
    {
        public Presenter(Ui ui, ShellUi shell)
        {
            this.ui = ui;
            this.shell = shell;
        }

        public virtual void Start()
        {
            UiHelpers.Write(this.shell, () => this.shell.SwitchUi(this.ui));
            this.shell.WriteFinished.WaitOne();
        }

        public virtual void Stop()
        {
        }

        private readonly Ui ui;
        private readonly ShellUi shell;
    }
}
