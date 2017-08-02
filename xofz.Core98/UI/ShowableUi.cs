namespace xofz.UI
{
    using System;

    public interface ShowableUi : Ui
    {
        event Action FirstShown;
    }
}
