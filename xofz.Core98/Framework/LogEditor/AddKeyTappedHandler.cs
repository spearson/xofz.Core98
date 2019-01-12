namespace xofz.Framework.LogEditor
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class AddKeyTappedHandler
    {
        public AddKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
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
            var w = this.web;
            w.Run<LogEditor, UiReaderWriter>(
                (le, uiRW) =>
                {
                    var customIsSelected = uiRW
                                               .Read(ui, () => ui.SelectedType)
                                               ?.ToLowerInvariant()
                                               .Contains("custom")
                                           ?? false;
                    var type = customIsSelected
                        ? uiRW.Read(ui, () => ui.CustomType)
                        : uiRW.Read(ui, () => ui.SelectedType);
                    le.AddEntry(
                        type,
                        uiRW.Read(
                            ui,
                            () => ui.Content));

                    uiRW.Write(
                        ui, () =>
                        {
                            ui.Content = null;
                            ui.SelectedType = "Information";
                            ui.Hide();
                        });

                },
                logName);
        }

        protected readonly MethodWeb web;

    }
}
