namespace xofz.Framework.LogStatistics
{
    using System;
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.LogStatistics;

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
            r?.Run<LogStatistics, UiReaderWriter>(
                (stats, uiRW) =>
                {
                    var today = DateTime.Today;
                    r.Run<Framework.Log.TimeProvider>(provider =>
                    {
                        today = provider.Now().Date;
                    });

                    const byte six = 6;
                    var lastWeek = today.Subtract(
                        TimeSpan.FromDays(six));
                    uiRW.WriteSync(
                        ui,
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.StartDate = lastWeek;
                            ui.EndDate = today;
                        });

                    stats.Reset();

                    r.Run<StatsDisplayer>(displayer =>
                    {
                        const bool truth = true;
                        displayer.Display(
                            ui, 
                            stats, 
                            truth);
                    });
                },
                name);

        }

        protected readonly MethodRunner runner;
    }
}
