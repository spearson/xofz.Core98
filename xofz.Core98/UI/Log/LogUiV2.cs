namespace xofz.UI.Log
{
    public interface LogUiV2
        : LogUi
    {
        string StartLabelLabel { get; set; }

        string EndLabelLabel { get; set; }

        string ClearKeyLabel { get; set; }

        string StatsKeyLabel { get; set; }

        string AddKeyLabel { get; set; }

        string PreviousWeekKeyLabel { get; set; }

        string NextWeekKeyLabel { get; set; }

        string CurrentWeekKeyLabel { get; set; }

        string FilterContentLabelLabel { get; set; }

        string FilterTypeLabelLabel { get; set; }

        string ResetContentKeyLabel { get; set; }

        string ResetTypeKeyLabel { get; set; }

        string TimestampColumnHeaderLabel { get; set; }

        string TypeColumnHeaderLabel { get; set; }

        string ContentColumnHeaderLabel { get; set; }
    }
}
