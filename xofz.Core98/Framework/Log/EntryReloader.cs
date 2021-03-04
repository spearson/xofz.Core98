namespace xofz.Framework.Log
{
    using System.Collections.Generic;
    using xofz.Framework.Logging;
    using xofz.UI;

    public class EntryReloader
    {
        public EntryReloader(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Reload(
            LogUi ui,
            string name)
        {
            var r = this.runner;
            r?.Run<Log, UiReaderWriter, EntryConverter>(
                (log, uiRW, converter) =>
                {
                    var start = uiRW.Read(ui, () => ui?.StartDate);
                    var end = uiRW.Read(ui, () => ui?.EndDate);
                    var filterContent = uiRW.Read(
                        ui,
                        () => ui?.FilterContent);
                    var filterType = uiRW.Read(
                        ui,
                        () => ui?.FilterType);
                    const string emptyString = @"";

                    // first, begin reading all entries
                    var matchingEntries = log.ReadEntries();

                    // second, get all the entries in the date range
                    matchingEntries = EnumerableHelpers.Where(
                        matchingEntries,
                        e => e.Timestamp >= start
                             && e.Timestamp < end?.AddDays(1));

                    // third, match on content
                    if (!StringHelpers.NullOrWhiteSpace(filterContent))
                    {
                        matchingEntries = EnumerableHelpers.Where(
                            matchingEntries,
                            e => EnumerableHelpers.Any(
                                e.Content,
                                s => s
                                         ?.ToLowerInvariant()
                                         ?.Contains(
                                             filterContent?.ToLowerInvariant()
                                             ?? emptyString) ??
                                     false));
                    }

                    // fourth, match on type
                    if (!StringHelpers.NullOrWhiteSpace(filterType))
                    {
                        matchingEntries = EnumerableHelpers.Where(
                            matchingEntries,
                            e => e
                                     ?.Type
                                     ?.ToLowerInvariant()
                                     ?.Contains(
                                         filterType?.ToLowerInvariant()
                                         ?? emptyString) ??
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
                                entry => converter.Convert(
                                    entry,
                                    name)));

                    uiRW.WriteSync(
                        ui,
                        () =>
                        {
                            if (ui == null)
                            {
                                return;
                            }

                            ui.Entries = uiEntries;
                        });
                    r.Run<ICollection<LogEntry>>(
                        refreshEntries =>
                        {
                            refreshEntries.Clear();
                        },
                        name);
                },
                name);
        }

        protected readonly MethodRunner runner;
    }
}
