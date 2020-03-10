namespace xofz.UI
{
    public interface LogUiV3
        : LogUiV2
    {
        event Do PreviousWeekKeyTapped;

        event Do CurrentWeekKeyTapped;

        event Do NextWeekKeyTapped;

        event Do UpKeyTapped;

        event Do DownKeyTapped;

        event Do ResetContentKeyTapped;

        event Do ResetTypeKeyTapped;

        void FocusEntries();

        void ResetContentFilter();

        void ResetTypeFilter();
    }
}
