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
                r.Run<LogStatistics, SettingsHolder>(
                    (stats, settings) =>
                    {
                        var df = settings.DateFormat;
                        stats.ComputeRange(
                            start, end);
                        var typeInfo =
                            @"Range: "
                            + start.ToString(df)
                            + @" to "
                            + end.ToString(df);
                        uiRW.WriteSync(
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
                    name);
            });
        }

        protected readonly MethodRunner runner;
    }
}
