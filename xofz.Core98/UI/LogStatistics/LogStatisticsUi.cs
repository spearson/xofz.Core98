namespace xofz.UI.LogStatistics
{
    using System;

    public interface LogStatisticsUi 
        : PopupUi
    {
        event Do OverallKeyTapped;

        event Do RangeKeyTapped;

        event Do HideKeyTapped;

        event Do ResetContentKeyTapped;

        event Do ResetTypeKeyTapped;

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        string FilterContent { get; set; }

        string FilterType { get; set; }

        string Title { get; set; }

        string TotalEntryCount { get; set; }

        string AvgEntriesPerDay { get; set; }

        string OldestTimestamp { get; set; }

        string NewestTimestamp { get; set; }

        string EarliestTimestamp { get; set; }

        string LatestTimestamp { get; set; }
    }
}
