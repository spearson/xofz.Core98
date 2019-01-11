namespace xofz.UI
{
    using System.Security;

    public interface LoginUi : PopupUi
    {
        event Do BackspaceKeyTapped;

        event Do LoginKeyTapped;

        event Do CancelKeyTapped;

        event Do KeyboardKeyTapped;

        AccessLevel CurrentAccessLevel { get; set; }

        SecureString CurrentPassword { get; set; }

        string TimeRemaining { get; set; }

        bool KeyboardKeyVisible { get; set; }

        void FocusPassword();
    }
}
