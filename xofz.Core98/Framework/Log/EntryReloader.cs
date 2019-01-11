namespace xofz.Framework.Log
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class EntryReloader
    {
        public EntryReloader(MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Reload(
            LogUi ui,
            string name)
        {
            var w = this.web;
            w.Run<Log, UiReaderWriter>((log, uiRW) =>
                {
                    var start = uiRW.Read(ui, () => ui.StartDate);
                    var end = uiRW.Read(ui, () => ui.EndDate);
                    var filterContent = uiRW.Read(
                        ui,
                        () => ui.FilterContent);
                    var filterType = uiRW.Read(
                        ui,
                        () => ui.FilterType);

                    // first, begin reading all entries
                    var matchingEntries = log.ReadEntries();

                    // second, get all the entries in the date range
                    matchingEntries = EnumerableHelpers.Where(
                        matchingEntries,
                        e => e.Timestamp >= start
                             && e.Timestamp < end.AddDays(1));

                    // third, match on content
                    if (!StringHelpers.NullOrWhiteSpace(filterContent))
                    {
                        matchingEntries = EnumerableHelpers.Where(
                            matchingEntries,
                            e => EnumerableHelpers.Any(
                                e.Content,
                                s => s
                                         ?.ToLowerInvariant()
                                         .Contains(
                                             filterContent
                                                 .ToLowerInvariant()) ??
                                     false));
                    }

                    // fourth, match on type
                    if (!StringHelpers.NullOrWhiteSpace(filterType))
                    {
                        matchingEntries = EnumerableHelpers.Where(
                            matchingEntries,
                            e => e.Type?.ToLowerInvariant()
                                     .Contains(filterType.ToLowerInvariant()) ??
                                 false);
                    }

                    // finally, order them by newest first
                    matchingEntries = EnumerableHelpers.OrderByDescending(
                        matchingEntries,
                        e => e.Timestamp);

                    ICollection<XTuple<string, string, string>> uiEntries
                        = new LinkedList<XTuple<string, string, string>>(
                            EnumerableHelpers.Select(
                                matchingEntries,
                                this.createTuple));

                    uiRW.WriteSync(
                        ui,
                        () => ui.Entries = uiEntries);
                    w.Run<ICollection<LogEntry>>(
                        refreshEntries =>
                        {
                            refreshEntries.Clear();
                        },
                        name);
                },
                name);
        }

        protected virtual XTuple<string, string, string> createTuple(LogEntry e)
        {
            return XTuple.Create(
                e.Timestamp.ToString(
                    "yyyy/MM/dd HH:mm.ss.fffffff",
                    CultureInfo.CurrentCulture),
                e.Type,
                string.Join(
                    Environment.NewLine,
                    EnumerableHelpers.ToArray(e.Content)));
        }

        protected readonly MethodWeb web;
    }
}
