namespace xofz.UI
{
    public interface MinimizableUi : Ui
    {
        event Action Minimized;

        event Action Restored;

        void Minimize();

        void Restore();
    }
}
