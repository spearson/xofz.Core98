namespace xofz.Framework.LogStatistics
{
    using xofz.UI;

    public class StartHandler
    {
        public StartHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            Gen<LogUi> readLogUi,
            string name)
        {
            var w = this.web;
            w.Run<Log.SettingsHolder>(settings =>
            {
                if (settings.ResetOnStart)
                {
                    w.Run<DateResetter>(dr =>
                    {
                        dr.Reset(ui, name);
                    });
                }
            });

            w.Run<UiReaderWriter>(uiRW =>
            {
                var contentFilter = uiRW.Read(
                    ui, 
                    () => ui.FilterContent);
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
                        () => ui.FilterContent = contentFilter);
                }
            });
        }

        protected readonly MethodWeb web;
    }
}
