namespace xofz.UI
{
    public interface LoginUi : PopupUi
    {
        event Action BackspaceKeyTapped;

        event Action LoginKeyTapped;

        event Action CancelKeyTapped;

        event Action KeyboardKeyTapped;

        AccessLevel CurrentAccessLevel { get; set; }

        string CurrentPassword { get; set; }

        string TimeRemaining { get; set; }

        bool KeyboardKeyVisible { get; set; }

        void FocusPassword();
    }
}
