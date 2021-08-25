namespace xofz.Framework.Log
{
    using xofz.Framework.Logging;
    using xofz.UI;
    using EH = EnumerableHelpers;

    public class NewestKeyTappedHandler
    {
        public NewestKeyTappedHandler(
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
                    var newestNullable = EH.FirstOrNull(
                            EH.OrderByDescending(
                                log.ReadEntries(),
                                entry => entry.Timestamp))?.
                        Timestamp;
                    if (newestNullable == null)
                    {
                        return;
                    }

                    var newest = newestNullable.Value;
                    const short daysToAdd = -6;
                    var newEndDate = newest.Date;
                    var newStartDate = newEndDate.AddDays(daysToAdd);

                    r?.Run<UiReaderWriter>(uiRW =>
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