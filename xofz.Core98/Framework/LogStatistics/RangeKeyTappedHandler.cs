namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class RangeKeyTappedHandler
    {
        public RangeKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            string name)
        {
            var r = this.runner;
            r.Run<UiReaderWriter>(uiRW =>
            {
                var start = uiRW.Read(
                    ui,
                    () => ui.StartDate);
                var end = uiRW.Read(
                    ui,
                    () => ui.EndDate);
                r.Run<FilterSetter>(fs =>
                {
                    fs.Set(ui, name);
                });
                r.Run<
                    LogStatistics,
                    SettingsHolder,
                    Labels>(
                    (stats, settings, labels) =>
                    {
                        var df = settings.DateFormat;
                        stats.ComputeRange(
                            start, end);
                        var typeInfo = labels.Range(
                            start,
                            end,
                            df);
                        uiRW.Write(
                            ui,
                            () =>
                            {
                                ui.Title = typeInfo;
                            });
                        r.Run<StatsDisplayer>(sd =>
                        {
                            sd.Display(ui, stats, false);
                        });
                    },
                    name,
                    name);
            });
        }

        protected readonly MethodRunner runner;
    }
}
