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
            var r = this.runner;
            r.Run<FieldHolder, UiReaderWriter, FilterChecker>(
                (holder, uiRW, checker) =>
                {
                    if (uiRW.Read(ui, () => ui.EndDate)
                        < DateTime.Today)
                    {
                        return;
                    }

                    if (Interlocked.Read(ref holder.startedIf1) != 1)
                    {
                        if (Interlocked.Read(ref holder.startedFirstTimeIf1) ==
                            1 && checker.PassesFilters(ui, entry))
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
                            var xt = converter.Convert(entry);
                            uiRW.Write(
                                ui,
                                () =>
                                {
                                    ui.AddToTop(xt);
                                });
                        });
                    }
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
