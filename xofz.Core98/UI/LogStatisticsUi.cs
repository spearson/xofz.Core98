namespace xofz.UI
{
    using System;

    public interface LogStatisticsUi : PopupUi
    {
        event Action OverallKeyTapped;

        event Action RangeKeyTapped;

        event Action HideKeyTapped;

        event Action ResetContentKeyTapped;

        event Action ResetTypeKeyTapped;

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        string FilterContent { get; set; }

        string FilterType { get; set; }

        string Header { get; set; }

        string TotalEntryCount { get; set; }

        string AvgEntriesPerDay { get; set; }

        string OldestTimestamp { get; set; }

        string NewestTimestamp { get; set; }

        string EarliestTimestamp { get; set; }

        string LatestTimestamp { get; set; }
    }
}
