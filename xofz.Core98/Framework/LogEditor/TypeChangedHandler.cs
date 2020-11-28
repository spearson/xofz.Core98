namespace xofz.Framework.LogEditor
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class TypeChangedHandler
    {
        public TypeChangedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires a UiReaderWriter registered with the MethodWeb
        /// </summary>
        /// <param name="ui"></param>
        public virtual void Handle(
            LogEditorUi ui)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                var customIsSelected
                    = uiRW
                          .Read(ui, () => ui.SelectedType)
                          ?.Contains(DefaultEntryTypes.Custom)
                      ?? false;
                uiRW.Write(ui, () =>
                {
                    ui.CustomTypeVisible = customIsSelected;
                });
            });
        }

        public virtual void Handle(
            LogEditorUi ui,
            string name)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                var customIsSelected
                    = uiRW
                          .Read(ui, () => ui.SelectedType)
                          ?.Contains(DefaultEntryTypes.Custom)
                      ?? false;
                uiRW.Write(ui, () =>
                {
                    ui.CustomTypeVisible = customIsSelected;
                });
            });
        }

        protected readonly MethodRunner runner;
    }
}
