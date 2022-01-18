namespace xofz.UI.Login
{
    public interface LoginUiV2
        : LoginUi
    {
        string PasswordLabel { get; set; }

        string TimeRemainingLabel { get; set; }

        string BackspaceKeyLabel { get; set; }

        string ClearKeyLabel { get; set; }

        string LogInKeyLabel { get; set; }

        string CancelKeyLabel { get; set; }

        string KeyboardKeyLabel { get; set; }

        string Title { get; set; }
    }
}
