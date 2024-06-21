namespace xofz.UI.LogStatistics
{
    public interface LogStatisticsUiV2
        : LogStatisticsUi, LabeledUi
    {
        string StartLabelLabel { get; set; }

        string EndLabelLabel { get; set; }

        string HideKeyLabel { get; set; }

        string OverallKeyLabel { get; set; }

        string RangeKeyLabel { get; set; }

        string FilterContentLabelLabel { get; set; }

        string FilterTypeLabelLabel { get; set; }

        string ResetContentKeyLabel { get; set; }

        string ResetTypeKeyLabel { get; set; }

        string StatsContainerLabel { get; set; }

        string TotalEntryCountLabelLabelLabel { get; set; }

        string AvgEntriesPerDayLabelLabelLabel { get; set; }

        string OldestTimestampLabelLabelLabel { get; set; }

        string NewestTimestampLabelLabelLabel { get; set; }

        string EarliestTimestampLabelLabelLabel { get; set; }

        string LatestTimestampLabelLabelLabel { get; set; }
    }
}