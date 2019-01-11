namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class RangeKeyTappedHandler
    {
        public RangeKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            string name)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(uiRW =>
            {
                var start = uiRW.Read(
                    ui,
                    () => ui.StartDate);
                var end = uiRW.Read(
                    ui,
                    () => ui.EndDate);
                w.Run<FilterSetter>(fs =>
                {
                    fs.Set(ui, name);
                });
                w.Run<LogStatistics>(stats =>
                    {
                        var df = SettingsHolder.DateFormat;
                        stats.ComputeRange(
                            start, end);
                        var typeInfo =
                            "Range: "
                            + start.ToString(df)
                            + " to "
                            + end.ToString(df);
                        uiRW.WriteSync(
                            ui,
                            () =>
                            {
                                ui.Title = typeInfo;
                            });
                        w.Run<StatsDisplayer>(sd =>
                        {
                            sd.Display(ui, stats, false);
                        });
                    },
                    name);
            });
        }

        protected readonly MethodWeb web;
    }
}
