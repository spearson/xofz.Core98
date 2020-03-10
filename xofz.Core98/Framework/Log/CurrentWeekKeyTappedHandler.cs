namespace xofz.Framework.Log
{
    using System;
    using xofz.Framework.Logging;
    using xofz.UI;
    using EH = xofz.EnumerableHelpers;

    public class CurrentWeekKeyTappedHandler
    {
        public CurrentWeekKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUiV3 ui,
            string name,
            Do unsubscribe,
            Do subscribe)
        {
            var r = this.runner;
            r.Run<UiReaderWriter, TimeProvider>(
                (uiRW, timeProvider) =>
                {
                    var currentDay = timeProvider.Now().Date;
                    r.Run<Log>(log =>
                        {
                            try
                            {
                                var newestEntry =
                                    EH.FirstOrDefault(
                                        EH.OrderByDescending(
                                            log.ReadEntries(),
                                            entry => entry.Timestamp));
                                if (newestEntry != null)
                                {
                                    var ts = newestEntry.Timestamp;
                                    if (currentDay > ts)
                                    {
                                        currentDay = ts.Date;
                                    }
                                }
                            }
                            catch (OutOfMemoryException)
                            {
                                // swallow
                            }
                        },
                        name);

                    const short daysToAdd = -6;
                    var newEndDate = currentDay;
                    var newStartDate = newEndDate
                        .AddDays(daysToAdd);
                    unsubscribe?.Invoke();

                    uiRW.WriteSync(
                        ui,
                        () =>
                        {
                            ui.StartDate = newStartDate;
                            ui.EndDate = newEndDate;
                        });

                    r.Run<LogStatisticsUi>(statsUi =>
                        {
                            uiRW.Write(
                                statsUi,
                                () =>
                                {
                                    statsUi.StartDate = newStartDate;
                                    statsUi.EndDate = newEndDate;
                                });
                        },
                        name);

                    subscribe?.Invoke();

                    r.Run<EventRaiser>(raiser =>
                    {
                        raiser.Raise(
                            ui,
                            nameof(ui.DateRangeChanged));
                    });
                });
        }

        protected readonly MethodRunner runner;
    }
}
