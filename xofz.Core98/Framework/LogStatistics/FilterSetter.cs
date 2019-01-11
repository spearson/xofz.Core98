namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class FilterSetter
    {
        public FilterSetter(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Set(
            LogStatisticsUi ui,
            LogStatistics stats)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(uiRW =>
            {
                stats.FilterContent = uiRW.Read(
                    ui,
                    () => ui.FilterContent);
                stats.FilterType = uiRW.Read(
                    ui,
                    () => ui.FilterType);
            });
        }

        protected readonly MethodWeb web;
    }
}
