namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class OverallKeyTappedHandler
    {
        public OverallKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            LogStatistics stats)
        {
            var w = this.web;
            w.Run<FilterSetter>(
                setter => setter.Set(
                    ui, stats));
            stats.ComputeOverall();
            w.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.Write(ui, () => ui.Header = @"Overall");
            });
            w.Run<StatsDisplayer>(sd =>
            {
                sd.Display(ui, stats, false);
            });
        }

        protected readonly MethodWeb web;
    }
}
