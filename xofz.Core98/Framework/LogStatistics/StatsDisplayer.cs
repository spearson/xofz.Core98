namespace xofz.Framework.LogStatistics
{
    using System;
    using System.Globalization;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class StatsDisplayer
    {
        public StatsDisplayer(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Display(
            LogStatisticsUi ui,
            string name,
            bool reset)
        {
            var w = this.web;
            w.Run<LogStatistics>(stats =>
                {
                    this.Display(ui, stats, reset);
                },
                name);
        }

        public virtual void Display(
            LogStatisticsUi ui,
            LogStatistics stats,
            bool reset)
        {
            var w = this.web;
            var defaultInfo = reset
                ? string.Empty
                : @"No entries in range";
            var tf = SettingsHolder.TimestampFormat;

            var total = reset
                ? defaultInfo
                : stats.TotalEntryCount.ToString();
            var oldest = stats.OldestTimestamp == default(DateTime)
                ? defaultInfo
                : stats.OldestTimestamp.ToString(tf);
            var newest = stats.NewestTimestamp == default(DateTime)
                ? defaultInfo
                : stats.NewestTimestamp.ToString(tf);
            var earliest = stats.EarliestTimestamp == default(DateTime)
                ? defaultInfo
                : stats.EarliestTimestamp.ToString(tf);
            var latest = stats.LatestTimestamp == default(DateTime)
                ? defaultInfo
                : stats.LatestTimestamp.ToString(tf);
            var avgPerDay = reset
                ? string.Empty
                : Math.Round(stats.AvgEntriesPerDay, 4)
                    .ToString(CultureInfo.CurrentUICulture);
            w.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.WriteSync(
                    ui,
                    () =>
                    {
                        ui.TotalEntryCount = total;
                        ui.OldestTimestamp = oldest;
                        ui.NewestTimestamp = newest;
                        ui.EarliestTimestamp = earliest;
                        ui.LatestTimestamp = latest;
                        ui.AvgEntriesPerDay = avgPerDay;
                    });
            });
        }

        protected readonly MethodWeb web;
    }
}
