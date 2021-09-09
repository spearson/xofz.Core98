namespace xofz.Framework.Log
{
    using System.Threading;
    using xofz.UI;

    public class DateAndFilterResetter
    {
        public DateAndFilterResetter(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Reset(
            LogUi ui,
            Do unsubscribe,
            Do subscribe,
            string name)
        {
            const byte zero = 0;
            const byte one = 1;
            const byte six = 6;

            var r = this.runner;
            r?.Run<FieldHolder, Delayer, UiReaderWriter>(
                (fields, delayer, uiRW) =>
                {
                    while (Interlocked.Exchange(
                               ref fields.resettingIf1,
                               one) == one)
                    {
                        delayer.Delay(zero);
                    }

                    var today = System.DateTime.Today;
                    r.Run<TimeProvider>(provider =>
                    {
                        today = provider.Now().Date;
                    });

                    var lastWeek = today.Subtract(System.TimeSpan.FromDays(six));
                    var needsReload = true;
                    var started = Interlocked.Read(ref fields.startedIf1) == one;
                    var emptyString = string.Empty;
                    if (uiRW.Read(ui, () => ui?.StartDate) == lastWeek
                        && uiRW.Read(ui, () => ui?.EndDate) == today
                        && uiRW.Read(ui, () => ui?.FilterContent) == emptyString
                        && uiRW.Read(ui, () => ui?.FilterType) == emptyString)
                    {
                        if (started && Interlocked.Read(
                                ref fields.startedFirstTimeIf1) == one)
                        {
                            needsReload = false;
                        }
                    }

                    Interlocked.Exchange(
                        ref fields.refreshOnStartIf1, 
                        zero);
                    if (started && needsReload)
                    {
                        unsubscribe?.Invoke();
                        uiRW.WriteSync(ui, () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.StartDate = lastWeek;
                            ui.EndDate = today;
                            ui.FilterType = emptyString;
                            ui.FilterContent = emptyString;
                        });

                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        subscribe?.Invoke();
                    }

                    Interlocked.Exchange(
                        ref fields.resettingIf1,
                        zero);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
