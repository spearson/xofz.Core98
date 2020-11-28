namespace xofz.Framework.LogStatistics
{
    using System;
    using System.Globalization;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class StatsDisplayer
    {
        public StatsDisplayer(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Display(
            LogStatisticsUi ui,
            string name,
            bool reset)
        {
            var r = this.runner;
            r?.Run<LogStatistics>(stats =>
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
            var r = this.runner;
            r?.Run<SettingsHolder, Labels>(
                (settings, labels) =>
                {
                    var defaultInfo = reset
                        ? string.Empty
                        : labels.NoEntriesInRange;

                    var tf = settings.TimestampFormat;
                    var total = reset
                        ? defaultInfo
                        : stats.TotalEntryCount.ToString();
                    var oldest = stats.OldestTimestamp;
                    var newest = stats.NewestTimestamp;
                    var earliest = stats.EarliestTimestamp;
                    var latest = stats.LatestTimestamp;
                    var oldestString = oldest == default
                        ? defaultInfo
                        : oldest.ToString(tf);
                    var newestString = newest == default
                        ? defaultInfo
                        : newest.ToString(tf);
                    var earliestString = earliest == default
                        ? defaultInfo
                        : earliest.ToString(tf);
                    var latestString = latest == default
                        ? defaultInfo
                        : latest.ToString(tf);
                    var avgPerDay = reset
                        ? string.Empty
                        : Math.Round(stats.AvgEntriesPerDay, 4)
                            .ToString(CultureInfo.CurrentUICulture);
                    r.Run<UiReaderWriter>(uiRW =>
                    {
                        uiRW.WriteSync(
                            ui,
                            () =>
                            {
                                ui.TotalEntryCount = total;
                                ui.OldestTimestamp = oldestString;
                                ui.NewestTimestamp = newestString;
                                ui.EarliestTimestamp = earliestString;
                                ui.LatestTimestamp = latestString;
                                ui.AvgEntriesPerDay = avgPerDay;
                            });
                    });
                },
                stats.LogDependencyName);
        }

        protected readonly MethodRunner runner;
    }
}
