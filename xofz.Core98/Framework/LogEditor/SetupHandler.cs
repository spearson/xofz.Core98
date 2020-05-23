﻿namespace xofz.Framework.LogEditor
{
    using System.Collections.Generic;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <remarks>
        /// Requires a UiReaderWriter registered with the MethodWeb
        /// </remarks>>
        public virtual void Handle(
            LogEditorUi ui)
        {
            var defaultType = DefaultEntryTypes.Information;
            ICollection<string> types = new LinkedList<string>(
                new[]
                {
                    defaultType,
                    DefaultEntryTypes.Warning,
                    DefaultEntryTypes.Error,
                    DefaultEntryTypes.Custom
                });

            var r = this.runner;
            r.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(
                    ui,
                    () =>
                    {
                        ui.Types = types;
                        ui.SelectedType = defaultType;
                        ui.CustomTypeVisible = false;
                    });
            });

            if (ui is LogEditorUiV2 v2)
            {
                r.Run<LabelApplier>(applier =>
                {
                    applier.Apply(v2);
                });
            }
        }

        protected readonly MethodRunner runner;
    }
}
