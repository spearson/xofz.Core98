namespace xofz.Framework.Log
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class FilterChecker
    {
        public FilterChecker(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual bool PassesFilters(
            LogUi ui,
            LogEntry entry)
        {
            if (entry == null)
            {
                return false;
            }

            var r = this.runner;
            var passed = false;
            r.Run<UiReaderWriter>(uiRW =>
            {
                var start = uiRW.Read(
                    ui,
                    () => ui.StartDate);
                var end = uiRW.Read(
                    ui,
                    () => ui.EndDate);
                if (entry.Timestamp < start)
                {
                    return;
                }

                if (entry.Timestamp > end.AddDays(1))
                {
                    return;
                }

                var filterType = uiRW.Read(
                    ui,
                    () => ui.FilterType);
                filterType = filterType?.ToLowerInvariant();
                var entryType = entry.Type;
                if (filterType != null)
                {
                    if (!entryType
                        ?.ToLowerInvariant()
                        .Contains(filterType) ?? true)
                    {
                        return;
                    }
                }

                var filterContent = uiRW.Read(
                    ui,
                    () => ui.FilterContent);
                filterContent = filterContent?.ToLowerInvariant();
                var entryContent = entry.Content;
                if (filterContent != null)
                {
                    if (EnumerableHelpers.All(
                        entryContent,
                        s => !s
                                 ?.ToLowerInvariant()
                                 .Contains(filterContent.ToLowerInvariant()) ??
                             true))
                    {
                        return;
                    }
                }

                passed = true;
            });

            return passed;
        }

        protected readonly MethodRunner runner;
    }
}
