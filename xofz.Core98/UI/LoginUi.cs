namespace xofz.UI
{
    using System;

    public interface LoginUi : Ui
    {
        event Action BackspaceKeyTapped;

        event Action LoginKeyTapped;

        event Action CancelKeyTapped;

        string CurrentPassword { get; set; }

        string TimeRemaining { get; set; }

        AccessLevel CurrentAccessLevel { get; set; }

        void Display();

        void Hide();
    }
}
