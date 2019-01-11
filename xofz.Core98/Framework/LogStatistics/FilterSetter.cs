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
            string name)
        {
            var w = this.web;
            w.Run<LogStatistics, UiReaderWriter>((stats, uiRW) =>
                {
                    stats.FilterContent = uiRW.Read(
                        ui,
                        () => ui.FilterContent);
                    stats.FilterType = uiRW.Read(
                        ui,
                        () => ui.FilterType);
                },
                name);
        }

        protected readonly MethodWeb web;
    }
}
