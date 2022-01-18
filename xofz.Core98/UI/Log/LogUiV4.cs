namespace xofz.UI.Log
{
    public interface LogUiV4
        : LogUiV3
    {
        event Do NewestKeyTapped;

        event Do OldestKeyTapped;

        string NewestKeyLabel { get; set; }

        string OldestKeyLabel { get; set; }

        bool NewestKeyDisabled { get; set; }

        bool OldestKeyDisabled { get; set; }
    }
}
