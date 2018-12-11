﻿namespace xofz.Framework.LogEditor
{
    using xofz.UI;

    public class TypeChangedHandler
    {
        public TypeChangedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        /// <summary>
        /// Requires a UiReaderWriter registered with the MethodWeb
        /// </summary>
        /// <param name="ui"></param>
        public virtual void Handle(
            LogEditorUi ui)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(rw =>
            {
                var customIsSelected = rw
                                           .Read(ui, () => ui.SelectedType)
                                           ?.ToLowerInvariant()
                                           .Contains("custom")
                                       ?? false;
                rw.Write(ui, () =>
                {
                    ui.CustomTypeVisible = customIsSelected;
                });
            });
        }

        protected readonly MethodWeb web;
    }
}
