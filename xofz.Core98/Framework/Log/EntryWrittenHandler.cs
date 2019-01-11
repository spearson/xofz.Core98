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
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogUi ui,
            string name,
            LogEntry entry)
        {
            var w = this.web;
            w.Run<FieldHolder, UiReaderWriter, FilterChecker>(
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
                            w.Run<ICollection<LogEntry>>(
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
                        w.Run<EntryConverter>(converter =>
                        {
                            var xt = converter.Convert(entry);
                            uiRW.Write(
                                ui,
                                () => ui.AddToTop(xt));
                        });
                    }
                },
                name);
        }

        protected readonly MethodWeb web;
    }
}
