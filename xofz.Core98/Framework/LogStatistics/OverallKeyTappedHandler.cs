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
            string name)
        {
            var w = this.web;
            w.Run<LogStatistics>(stats =>
                {
                    w.Run<FilterSetter>(
                        setter => setter.Set(
                            ui, name));
                    stats.ComputeOverall();
                    w.Run<UiReaderWriter>(uiRW =>
                    {
                        uiRW.Write(ui, () =>
                        {
                            ui.Title = @"Overall";
                        });
                    });
                    w.Run<StatsDisplayer>(sd =>
                    {
                        sd.Display(ui, stats, false);
                    });
                },
                name);
        }

        protected readonly MethodWeb web;
    }
}
