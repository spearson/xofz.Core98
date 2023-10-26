namespace xofz.Framework.LogStatistics
{
    using xofz.UI;
    using xofz.UI.Log;
    using xofz.UI.LogStatistics;

    public class StartHandler
    {
        public StartHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            Gen<LogUi> readLogUi,
            string name)
        {
            var r = this.runner;
            r?.Run<Log.SettingsHolder>(settings =>
                {
                    if (settings.ResetOnStart)
                    {
                        r.Run<DateResetter>(dr =>
                        {
                            dr.Reset(ui, name);
                        });
                    }
                },
                name);

            r?.Run<UiReaderWriter>(uiRW =>
            {
                var contentFilter = uiRW.Read(
                    ui, 
                    () => ui?.FilterContent);
                if (string.IsNullOrEmpty(contentFilter))
                {
                    var logUi = readLogUi?.Invoke();
                    if (logUi == null)
                    {
                        return;
                    }

                    contentFilter = uiRW.Read(
                        logUi,
                        () => logUi.FilterContent);
                    uiRW.WriteSync(
                        ui,
                        () =>
                        {
                            ui.FilterContent = contentFilter;
                        });
                }
            });
        }

        protected readonly MethodRunner runner;
    }
}
