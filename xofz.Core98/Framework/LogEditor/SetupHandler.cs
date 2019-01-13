namespace xofz.Framework.LogEditor
{
    using System.Collections.Generic;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        /// <remarks>
        /// Requires a UiReaderWriter registered with the MethodWeb
        /// </remarks>>
        public virtual void Handle(
            LogEditorUi ui)
        {
            ICollection<string> types = new LinkedList<string>(
                new[]
                {
                    DefaultEntryTypes.Information,
                    DefaultEntryTypes.Warning,
                    DefaultEntryTypes.Error,
                    DefaultEntryTypes.Custom
                });

            var w = this.web;
            w.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.Types = types;
                        ui.SelectedType = DefaultEntryTypes.Information;
                        ui.CustomTypeVisible = false;
                    });
            });
        }

        protected readonly MethodWeb web;
    }
}
