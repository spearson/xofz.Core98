namespace xofz.Framework.Login
{
    using xofz.UI;
    using xofz.UI.Login;

    public class StartHandler
    {
        public StartHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        /// <summary>
        /// Requires a UiReaderWriter and a SettingsHolder
        /// Registered with the MethodWeb
        /// </summary>
        /// <param name="ui"></param>
        public virtual void Handle(
            LoginUi ui)
        {
            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                r.Run<SettingsHolder>(
                    settings =>
                    {
                        settings.CurrentPassword = uiRW.Read(
                            ui, () => ui?.CurrentPassword);
                    });

                uiRW.WriteSync(ui, () =>
                {
                    try
                    {
                        ui?.Display();
                    }
                    catch
                    {
                        // already visible, swallow
                    }
                });
            });
        }

        protected readonly MethodRunner runner;
    }
}