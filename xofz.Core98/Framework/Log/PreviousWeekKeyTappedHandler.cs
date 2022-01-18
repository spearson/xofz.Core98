namespace xofz.Framework.Log
{
    using xofz.UI;
    using xofz.UI.Log;
    using xofz.UI.LogStatistics;

    public class PreviousWeekKeyTappedHandler
    {
        public PreviousWeekKeyTappedHandler(
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
                const short minusSeven = -7;
                var newStartDate = uiRW.Read(
                        ui,
                        () => ui?.StartDate)
                    ?.AddDays(minusSeven);
                var newEndDate = uiRW.Read(
                        ui,
                        () => ui?.EndDate)
                    ?.AddDays(minusSeven);

                unsubscribe?.Invoke();

                r.Run<TimeProvider>(provider =>
                {
                    var now = provider.Now();
                    var past = now.AddDays(minusSeven);

                    uiRW.WriteSync(
                        ui,
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.StartDate = newStartDate ?? past;
                            ui.EndDate = newEndDate ?? now;
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

                                    statsUi.StartDate = newStartDate ?? past;
                                    statsUi.EndDate = newEndDate ?? now;
                                });
                        },
                        name);
                });

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
