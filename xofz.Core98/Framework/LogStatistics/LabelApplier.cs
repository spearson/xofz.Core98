namespace xofz.Framework.LogStatistics
{
    using xofz.UI;
    using xofz.UI.LogStatistics;

    public class LabelApplier
    {
        public LabelApplier(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Apply(
            LogStatisticsUiV2 ui)
        {
            var r = this.runner;
            r?.Run<Labels>(labels =>
            {
                this.Apply(
                    ui,
                    labels);
            });
        }

        public virtual void Apply(
            LogStatisticsUiV2 ui,
            Labels labels)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        if (ui == null || labels == null)
                        {
                            return;
                        }

                        ui.Label = labels.Label;
                        ui.StartLabelLabel = labels.StartLabel;
                        ui.EndLabelLabel = labels.EndLabel;
                        ui.HideKeyLabel = labels.HideKey;
                        ui.OverallKeyLabel = labels.OverallKey;
                        ui.RangeKeyLabel = labels.RangeKey;
                        ui.FilterContentLabelLabel = labels.FilterContentLabel;
                        ui.FilterTypeLabelLabel = labels.FilterTypeLabel;
                        ui.ResetContentKeyLabel = labels.ResetContentKey;
                        ui.ResetTypeKeyLabel = labels.ResetTypeKey;
                        ui.StatsContainerLabel = labels.StatsContainer;
                        ui.TotalEntryCountLabelLabelLabel =
                            labels.TotalEntryCountLabelLabel;
                        ui.AvgEntriesPerDayLabelLabelLabel =
                            labels.AvgEntriesPerDayLabelLabel;
                        ui.OldestTimestampLabelLabelLabel
                            = labels.OldestTimestampLabelLabel;
                        ui.NewestTimestampLabelLabelLabel
                            = labels.NewestTimestampLabelLabel;
                        ui.EarliestTimestampLabelLabelLabel
                            = labels.EarliestTimestampLabelLabel;
                        ui.LatestTimestampLabelLabelLabel
                            = labels.LatestTimestampLabelLabel;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
