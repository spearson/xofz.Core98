namespace xofz.UI
{
    using System;

    public interface MainUi : Ui
    {
        event Action ShutdownRequested;
    }
}
