namespace xofz.Presentation
{
    using System;
    using System.Globalization;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.UI;

    public sealed class LogStatisticsPresenter : PopupNamedPresenter
    {
        public LogStatisticsPresenter(
            LogStatisticsUi ui,
            MethodWeb web)
            : base(ui)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            var subscriberRegistered = false;
            w.Run<EventSubscriber>(subscriber =>
            {
                subscriberRegistered = true;
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.RangeKeyTapped),
                    this.ui_RangeKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.OverallKeyTapped),
                    this.ui_OverallKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.HideKeyTapped),
                    this.Stop);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.ResetContentKeyTapped),
                    this.ui_ResetContentKeyTapped);
                subscriber.Subscribe(
                    this.ui,
                    nameof(this.ui.ResetTypeKeyTapped),
                    this.ui_ResetTypeKeyTapped);
            });

            if (!subscriberRegistered)
            {
                this.ui.RangeKeyTapped += this.ui_RangeKeyTapped;
                this.ui.OverallKeyTapped += this.ui_OverallKeyTapped;
                this.ui.HideKeyTapped += this.Stop;
                this.ui.ResetContentKeyTapped += this.ui_ResetContentKeyTapped;
                this.ui.ResetTypeKeyTapped += this.ui_ResetTypeKeyTapped;
            }
            
            this.resetDates();
            
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            if (Interlocked.Read(ref this.setupIf1) == 0)
            {
                return;
            }

            base.Start();
            var w = this.web;
            w.Run<LogSettings>(settings =>
                {
                    if (settings.ResetOnStart)
                    {
                        this.resetDates();
                    }
                },
                this.Name);
            
            var fc = UiHelpers.Read(this.ui, () => this.ui.FilterContent);
            if (string.IsNullOrEmpty(fc))
            {
                w.Run<Navigator>(n =>
                {
                    var logUi = n.GetUi<LogPresenter, LogUi>(
                        this.Name);
                    if (logUi == default(LogUi))
                    {
                        return;
                    }

                    fc = UiHelpers.Read(
                        logUi,
                        () => logUi.FilterContent);
                    UiHelpers.WriteSync(
                        this.ui,
                        () => this.ui.FilterContent = fc);
                });
            }
        }

        private void resetDates()
        {
            var w = this.web;
            var today = DateTime.Today;
            var lastWeek = today.Subtract(TimeSpan.FromDays(6));
            UiHelpers.WriteSync(this.ui, () =>
            {
                this.ui.StartDate = lastWeek;
                this.ui.EndDate = today;
            });

            w.Run<LogStatistics>(
                stats =>
                {
                    stats.Reset();
                    this.showStatistics(
                        stats, true);
                },
                this.Name);
        }

        private void ui_RangeKeyTapped()
        {
            var w = this.web;
            var startDate = UiHelpers.Read(
                this.ui,
                () => this.ui.StartDate);
            var endDate = UiHelpers.Read(
                this.ui,
                () => this.ui.EndDate);
            w.Run<LogStatistics>(
                stats =>
                {
                    this.setFilters(stats);
                    stats.ComputeRange(
                        startDate, endDate);
                    var typeInfo =
                        "Range: "
                        + this.formatDate(startDate)
                        + " to "
                        + this.formatDate(endDate);
                    UiHelpers.WriteSync(
                        this.ui,
                        () => this.ui.Header = typeInfo);
                    this.showStatistics(stats, false);
                },
                this.Name);
        }

        private void ui_OverallKeyTapped()
        {
            var w = this.web;
            w.Run<LogStatistics>(
                stats =>
                {
                    this.setFilters(stats);
                    stats.ComputeOverall();
                    UiHelpers.WriteSync(
                        this.ui,
                        () => this.ui.Header = "Overall");
                    this.showStatistics(stats, false);
                },
                this.Name);
        }

        private void setFilters(LogStatistics statistics)
        {
            statistics.FilterContent = UiHelpers.Read(
                this.ui,
                () => this.ui.FilterContent);
            statistics.FilterType = UiHelpers.Read(
                this.ui,
                () => this.ui.FilterType);
        }

        private void showStatistics(
            LogStatistics statistics,
            bool reset)
        {
            var defaultInfo = reset
                ? string.Empty
                : "No entries in range";

            var total = reset
                ? string.Empty
                : statistics.TotalEntryCount.ToString();
            var oldest = statistics.OldestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.OldestTimestamp);
            var newest = statistics.NewestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.NewestTimestamp);
            var earliest = statistics.EarliestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.EarliestTimestamp);
            var latest = statistics.LatestTimestamp == default(DateTime)
                ? defaultInfo
                : this.formatTimestamp(statistics.LatestTimestamp);
            var avgPerDay = reset
                ? string.Empty
                : Math.Round(statistics.AvgEntriesPerDay, 4)
                    .ToString(CultureInfo.CurrentUICulture);
            UiHelpers.WriteSync(
                this.ui,
                () =>
                {
                    this.ui.TotalEntryCount = total;
                    this.ui.OldestTimestamp = oldest;
                    this.ui.NewestTimestamp = newest;
                    this.ui.EarliestTimestamp = earliest;
                    this.ui.LatestTimestamp = latest;
                    this.ui.AvgEntriesPerDay = avgPerDay;
                });
        }

        private string formatDate(DateTime date)
        {
            return date.ToString(
                @"yyyy/MM/dd");
        }

        private string formatTimestamp(DateTime timestamp)
        {
            return timestamp.ToString(
                @"yyyy/MM/dd HH:mm:ss");
        }

        private void ui_ResetContentKeyTapped()
        {
            UiHelpers.Write(
                this.ui,
                () => this.ui.FilterContent = null);
        }

        private void ui_ResetTypeKeyTapped()
        {
            UiHelpers.Write(
                this.ui,
                () => this.ui.FilterType = null);
        }

        private long setupIf1;
        private readonly LogStatisticsUi ui;
        private readonly MethodWeb web;
    }
}
