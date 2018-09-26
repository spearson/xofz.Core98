namespace xofz.UI
{
    public interface LoginUi : PopupUi
    {
        event Do BackspaceKeyTapped;

        event Do LoginKeyTapped;

        event Do CancelKeyTapped;

        event Do KeyboardKeyTapped;

        AccessLevel CurrentAccessLevel { get; set; }

        string CurrentPassword { get; set; }

        string TimeRemaining { get; set; }

        bool KeyboardKeyVisible { get; set; }

        void FocusPassword();
    }
}
