namespace xofz.Framework.Log
{
    using System;
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
            var r = this.runner;
            r?.Run<FieldHolder, UiReaderWriter>(
                (fields, uiRW) =>
                {
                    while (Interlocked.Exchange(
                               ref fields.resettingIf1,
                               1) == 1)
                    {
                        Thread.Sleep(0);
                    }

                    var today = DateTime.Today;
                    r.Run<TimeProvider>(provider =>
                    {
                        today = provider.Now().Date;
                    });

                    var lastWeek = today.Subtract(TimeSpan.FromDays(6));
                    var needsReload = true;
                    var started = Interlocked.Read(ref fields.startedIf1) == 1;
                    if (uiRW.Read(ui, () => ui.StartDate) == lastWeek
                        && uiRW.Read(ui, () => ui.EndDate) == today
                        && uiRW.Read(ui, () => ui.FilterContent) == string.Empty
                        && uiRW.Read(ui, () => ui.FilterType) == string.Empty)
                    {
                        if (started && Interlocked.Read(
                                ref fields.startedFirstTimeIf1) == 1)
                        {
                            needsReload = false;
                        }
                    }

                    Interlocked.Exchange(
                        ref fields.refreshOnStartIf1, 
                        0);
                    if (started && needsReload)
                    {
                        unsubscribe?.Invoke();
                        uiRW.WriteSync(ui, () =>
                        {
                            ui.StartDate = lastWeek;
                            ui.EndDate = today;
                            ui.FilterType = string.Empty;
                            ui.FilterContent = string.Empty;
                        });
                        r.Run<EntryReloader>(reloader =>
                        {
                            reloader.Reload(ui, name);
                        });
                        subscribe?.Invoke();
                    }

                    Interlocked.Exchange(
                        ref fields.resettingIf1,
                        0);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
