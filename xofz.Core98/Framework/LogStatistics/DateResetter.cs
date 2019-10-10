namespace xofz.Framework.LogStatistics
{
    using System;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class DateResetter
    {
        public DateResetter(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Reset(
            LogStatisticsUi ui,
            string name)
        {
            var r = this.runner;
            r.Run<LogStatistics, UiReaderWriter>(
                (stats, uiRW) =>
                {
                    var today = DateTime.Today;
                    var lastWeek = today.Subtract(
                        TimeSpan.FromDays(6));
                    uiRW.WriteSync(ui, () =>
                    {
                        ui.StartDate = lastWeek;
                        ui.EndDate = today;
                    });

                    stats.Reset();

                    r.Run<StatsDisplayer>(sd =>
                    {
                        sd.Display(ui, stats, true);
                    });
                },
                name);

        }

        protected readonly MethodRunner runner;
    }
}
