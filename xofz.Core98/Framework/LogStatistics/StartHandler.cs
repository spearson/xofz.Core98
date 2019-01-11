namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
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
            LogStatistics stats,
            Framework.Log.SettingsHolder settings,
            Gen<LogUi> readLogUi)
        {
            var w = this.web;
            if (settings.ResetOnStart)
            {
                w.Run<DateResetter>(dr => dr.Reset(
                    ui, stats));
            }

            w.Run<UiReaderWriter>(uiRW =>
            {
                var contentFilter = uiRW.Read(
                    ui, 
                    () => ui.FilterContent);
                if (string.IsNullOrEmpty(contentFilter))
                {
                    var logUi = readLogUi();
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
