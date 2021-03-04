namespace xofz.Framework.Log
{
    using xofz.UI;

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
            r?.Run<UiReaderWriter, TimeProvider>(
                (uiRW, timeProvider) =>
                {
                    var currentDay = timeProvider.Now().Date;
                    const short daysToAdd = -6;
                    var newEndDate = currentDay;
                    var newStartDate = newEndDate
                        .AddDays(daysToAdd);
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
