namespace xofz.Framework.Login
{
    using xofz.UI;

    public class StartHandler
    {
        public StartHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        /// <summary>
        /// Requires a UiReaderWriter and a SettingsHolder
        /// Registered with the MethodWeb
        /// </summary>
        /// <param name="ui"></param>
        public virtual void Handle(
            LoginUi ui)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(rw =>
            {
                w.Run<SettingsHolder>(
                    settings =>
                    {
                        settings.CurrentPassword = rw.Read(
                            ui, () => ui.CurrentPassword);
                    });

                rw.WriteSync(ui, () =>
                {
                    try
                    {
                        ui.Display();
                    }
                    catch
                    {
                        // already visible, swallow
                    }
                    
                });
            });
        }

        protected readonly MethodWeb web;
    }
}
