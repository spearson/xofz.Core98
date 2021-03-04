namespace xofz.Framework.LogEditor
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class AddKeyTappedHandler
    {
        public AddKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires a UiReaderWriter and a LogEditor
        /// registered with the MethodWeb.
        /// </summary> 
        /// <param name="ui">
        /// The LogEditorUi to handle the event for
        /// </param>
        /// <param name="logName">
        /// The name of the LogEditor dependency in the MethodWeb
        /// </param>
        public virtual void Handle(
            LogEditorUi ui,
            string logName)
        {
            var r = this.runner;
            const bool falsity = false;
            r?.Run<LogEditor, UiReaderWriter>(
                (le, uiRW) =>
                {
                    var customIsSelected =
                        uiRW
                            .Read(
                                ui, 
                                () => ui?.SelectedType)
                            ?.Contains(DefaultEntryTypes
                                .Custom)
                        ?? falsity;
                    var type = customIsSelected
                        ? uiRW.Read(ui, () => ui?.CustomType)
                        : uiRW.Read(ui, () => ui?.SelectedType);
                    le.AddEntry(
                        type,
                        uiRW.Read(
                            ui,
                            () => ui?.Content));

                    uiRW.Write(
                        ui, 
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.Content = null;
                            ui.SelectedType = DefaultEntryTypes.Information;
                            ui?.Hide();
                        });
                },
                logName);
        }

        protected readonly MethodRunner runner;
    }
}
