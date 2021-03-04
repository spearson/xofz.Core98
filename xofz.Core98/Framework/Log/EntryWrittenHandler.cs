namespace xofz.Framework.Log
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class EntryWrittenHandler
    {
        public EntryWrittenHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogUi ui,
            string name,
            LogEntry entry)
        {
            const byte one = 1;
            var r = this.runner;
            r?.Run<FieldHolder, UiReaderWriter, FilterChecker>(
                (holder, uiRW, checker) =>
                {
                    var today = DateTime.Today;
                    r.Run<TimeProvider>(provider =>
                    {
                        today = provider.Now().Date;
                    });

                    if (uiRW.Read(ui, () => ui?.EndDate) < today)
                    {
                        return;
                    }

                    if (Interlocked.Read(ref holder.startedIf1) != one)
                    {
                        if (Interlocked.Read(ref holder.startedFirstTimeIf1) ==
                            one && checker.PassesFilters(ui, entry))
                        {
                            r.Run<ICollection<LogEntry>>(
                                refreshEntries =>
                                {
                                    refreshEntries.Add(entry);
                                },
                                name);
                        }

                        return;
                    }

                    if (checker.PassesFilters(ui, entry))
                    {
                        r.Run<EntryConverter>(converter =>
                        {
                            var xt = converter.Convert(entry, name);
                            uiRW.Write(
                                ui,
                                () =>
                                {
                                    ui?.AddToTop(xt);
                                });
                        });
                    }
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
