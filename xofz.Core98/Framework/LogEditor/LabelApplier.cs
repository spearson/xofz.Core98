namespace xofz.Framework.LogEditor
{
    using xofz.UI;
    using xofz.UI.LogEditor;

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
            r?.Run<Labels>(labels =>
            {
                this.Apply(
                    ui,
                    labels);
            });
        }

        public virtual void Apply(
            LogEditorUiV2 ui,
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