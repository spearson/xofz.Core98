namespace xofz.Framework.Log
{
    using xofz.UI;

    public class NextWeekKeyTappedHandler
    {
        public NextWeekKeyTappedHandler(
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
            r?.Run<UiReaderWriter>(uiRW =>
            {
                const short daysToAdd = 7;
                var newStartDate = uiRW.Read(
                        ui,
                        () => ui.StartDate)
                    .AddDays(daysToAdd);
                var newEndDate = uiRW.Read(
                        ui,
                        () => ui.EndDate)
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
