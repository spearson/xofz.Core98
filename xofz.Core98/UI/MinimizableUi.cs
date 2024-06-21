namespace xofz.UI
{
    public interface MinimizableUi
        : Ui
    {
        event Do Minimized;

        event Do Restored;

        void Minimize();

        void Restore();
    }
}