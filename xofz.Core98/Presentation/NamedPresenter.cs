namespace xofz.Presentation
{
    using UI;

    public class NamedPresenter
        : Presenter, Nameable
    {
        public NamedPresenter(
            Ui ui,
            ShellUi shell)
            : base(ui, shell)
        {
        }

        public virtual string Name { get; set; }
    }
}