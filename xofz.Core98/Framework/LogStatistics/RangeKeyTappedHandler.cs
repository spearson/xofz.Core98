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
            LogStatistics stats)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(uiRW =>
            {
                var start = UiHelpers.Read(
                    ui,
                    () => ui.StartDate);
                var end = UiHelpers.Read(
                    ui,
                    () => ui.EndDate);
                w.Run<FilterSetter>(fs =>
                {
                    fs.Set(ui, stats);
                });

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
                    () => ui.Header = typeInfo);
                w.Run<StatsDisplayer>(sd =>
                {
                    sd.Display(ui, stats, false);
                });
            });
        }

        protected readonly MethodWeb web;
    }
}
