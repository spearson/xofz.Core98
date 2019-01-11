namespace xofz.Framework.LogStatistics
{
    using System;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class DateResetter
    {
        public DateResetter(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Reset(
            LogStatisticsUi ui,
            LogStatistics stats)
        {
            var w = this.web;
            w.Run<UiReaderWriter>(uiRW =>
            {
                var today = DateTime.Today;
                var lastWeek = today.Subtract(TimeSpan.FromDays(6));
                uiRW.WriteSync(ui, () =>
                {
                    ui.StartDate = lastWeek;
                    ui.EndDate = today;
                });

                stats.Reset();

                w.Run<StatsDisplayer>(sd =>
                {
                    sd.Display(ui, stats, true);
                });
            });
            
        }

        protected readonly MethodWeb web;
    }
}
