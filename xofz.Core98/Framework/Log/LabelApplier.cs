namespace xofz.Framework.Log
{
    using xofz.UI;

    public class LabelApplier
    {
        public LabelApplier(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Apply(
            LogUiV2 ui)
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
            LogUiV2 ui,
            Labels labels)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>((uiRW) =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        if (ui == null || labels == null)
                        {
                            return;
                        }

                        ui.StartLabelLabel = labels.StartLabel;
                        ui.EndLabelLabel = labels.EndLabel;
                        ui.ClearKeyLabel = labels.ClearKey;
                        ui.StatsKeyLabel = labels.StatsKey;
                        ui.AddKeyLabel = labels.AddKey;
                        ui.PreviousWeekKeyLabel = labels.PreviousWeekKey;
                        ui.NextWeekKeyLabel = labels.NextWeekKey;
                        ui.CurrentWeekKeyLabel = labels.CurrentWeekKey;
                        ui.FilterContentLabelLabel = labels.FilterContentLabel;
                        ui.FilterTypeLabelLabel = labels.FilterTypeLabel;
                        ui.ResetContentKeyLabel = labels.ResetContentKey;
                        ui.ResetTypeKeyLabel = labels.ResetTypeKey;
                        ui.TimestampColumnHeaderLabel =
                            labels.TimestampColumnHeader;
                        ui.TypeColumnHeaderLabel = labels.TypeColumnHeader;
                        ui.ContentColumnHeaderLabel =
                            labels.ContentColumnHeader;
                    });

                if (ui is LogUiV4 v4)
                {
                    uiRW.Write(
                        v4,
                        () =>
                        {
                            v4.OldestKeyLabel = labels.OldestKey;
                            v4.NewestKeyLabel = labels.NewestKey;
                        });
                }
            });
        }

        protected readonly MethodRunner runner;
    }
}
