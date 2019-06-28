namespace xofz.Framework.LogEditor
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
            LogEditorUiV2 ui)
        {
            var r = this.runner;
            r.Run<Labels, UiReaderWriter>((labels, uiRW) =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.EntryTypeLabelLabel = labels.EntryTypeLabel;
                        ui.EntryContentLabelLabel = labels.EntryContentLabel;
                        ui.AddKeyLabel = labels.AddKey;
                        ui.TitleLabel = labels.Title;
                    });
            });
        }

        protected readonly MethodRunner runner;
    }
}
