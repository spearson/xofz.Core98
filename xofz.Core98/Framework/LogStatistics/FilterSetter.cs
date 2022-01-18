namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.LogStatistics;

    public class FilterSetter
    {
        public FilterSetter(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Set(
            LogStatisticsUi ui,
            string name)
        {
            var r = this.runner;
            r?.Run<LogStatistics, UiReaderWriter>(
                (stats, uiRW) =>
                {
                    stats.FilterContent = uiRW.Read(
                        ui,
                        () => ui?.FilterContent);
                    stats.FilterType = uiRW.Read(
                        ui,
                        () => ui?.FilterType);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
