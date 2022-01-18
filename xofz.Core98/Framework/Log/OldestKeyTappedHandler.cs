namespace xofz.Framework.Log
{
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.Log;
    using xofz.UI.LogStatistics;
    using EH = EnumerableHelpers;

    public class OldestKeyTappedHandler
    {
        public OldestKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUiV4 ui,
            string name,
            Do unsubscribe,
            Do subscribe)
        {
            var r = this.runner;
            r?.Run<Log>(log =>
                {
                    var oldestNullable = EH.FirstOrNull(
                            EH.OrderBy(
                                log.ReadEntries(),
                                entry => entry.Timestamp))?.
                        Timestamp;
                    if (oldestNullable == null)
                    {
                        return;
                    }

                    var oldest = oldestNullable.Value;
                    const short daysToAdd = 6;
                    var newStartDate = oldest.Date;
                    var newEndDate = newStartDate.AddDays(daysToAdd);

                    r.Run<UiReaderWriter>(uiRW =>
                    {
                        unsubscribe?.Invoke();
                        uiRW.WriteSync(
                            ui,
                            () =>
                            {
                                if (ui == null)
                                {
                                    return;
                                }

                                ui.StartDate = newStartDate;
                                ui.EndDate = newEndDate;
                            });

                        subscribe?.Invoke();
                        r.Run<LogStatisticsUi>(statsUi =>
                            {
                                uiRW.Write(
                                    statsUi,
                                    () =>
                                    {
                                        if (statsUi == null)
                                        {
                                            return;
                                        }

                                        statsUi.StartDate = newStartDate;
                                        statsUi.EndDate = newEndDate;
                                    });
                            },
                            name);

                        r.Run<EventRaiser>(raiser =>
                        {
                            raiser.Raise(
                                ui,
                                nameof(ui.DateRangeChanged));
                        });
                    });
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}