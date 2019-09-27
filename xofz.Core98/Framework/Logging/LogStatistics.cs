namespace xofz.Framework.Logging
{
    using System;

    public class LogStatistics
    {
        public LogStatistics(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual string LogDependencyName { get; set; }

        public virtual long TotalEntryCount { get; protected set; }

        public virtual double AvgEntriesPerDay { get; protected set; }

        public virtual string FilterContent { get; set; }

        public virtual string FilterType { get; set; }

        public virtual DateTime OldestTimestamp { get; protected set; }

        public virtual DateTime NewestTimestamp { get; protected set; }

        public virtual DateTime EarliestTimestamp { get; protected set; }

        public virtual DateTime LatestTimestamp { get; protected set; }

        public virtual void ComputeOverall()
        {
            var r = this.runner;
            r.Run<Log, Lotter>((log, lotter) =>
                {
                    var allEntries = lotter.Materialize(
                        log.ReadEntries());
                    var start = DateTime.MaxValue;
                    var end = DateTime.MinValue;
                    foreach (var entry in allEntries)
                    {
                        if (entry.Timestamp < start)
                        {
                            start = entry.Timestamp;
                        }

                        if (entry.Timestamp > end)
                        {
                            end = entry.Timestamp;
                        }
                    }

                    var matches = lotter.Materialize(
                            EnumerableHelpers.Where(
                                allEntries,
                                this.passesFilters));

                    this.computeTotal(matches);
                    this.computeAvgPerDay(
                        matches.Count,
                        start,
                        end);
                    this.computeOldestTimestamp(matches);
                    this.computeNewestTimestamp(matches);
                    this.computeEarliestTimestamp(matches);
                    this.computeLatestTimestamp(matches);
                },
                this.LogDependencyName,
                Framework.Log.DependencyNames.Lotter);
        }

        public virtual void ComputeRange(
            DateTime startDate,
            DateTime endDate)
        {
            var r = this.runner;
            r.Run<Log, Lotter>((l, lotter) =>
                {
                    var matches = lotter.Materialize(
                        EnumerableHelpers.Where(
                            EnumerableHelpers.Where(
                                l.ReadEntries(),
                                e =>
                                {
                                    var ts = e.Timestamp;
                                    return ts >= startDate
                                           && ts < endDate.AddDays(1);
                                }),
                            this.passesFilters));

                    this.computeTotal(matches);
                    this.computeAvgPerDay(
                        matches.Count,
                        startDate,
                        endDate);
                    this.computeOldestTimestamp(matches);
                    this.computeNewestTimestamp(matches);
                    this.computeEarliestTimestamp(matches);
                    this.computeLatestTimestamp(matches);
                },
                this.LogDependencyName,
                Framework.Log.DependencyNames.Lotter);
        }

        public virtual void Reset()
        {
            var ts = default(DateTime);
            this.OldestTimestamp = ts;
            this.NewestTimestamp = ts;
            this.EarliestTimestamp = ts;
            this.LatestTimestamp = ts;
            this.AvgEntriesPerDay = default(double);
            this.TotalEntryCount = 0;
        }

        protected virtual bool passesFilters(
            LogEntry e)
        {
            var fc = this.FilterContent;
            if (!string.IsNullOrEmpty(fc))
            {
                bool foundContent;
                var ec = e.Content;
                if (ec == null)
                {
                    foundContent = false;
                    goto finishCheckingContent;
                }

                foreach (var line in ec)
                {
                    if (line?.ToLowerInvariant()
                            .Contains(fc.ToLowerInvariant())
                        ?? false)
                    {
                        foundContent = true;
                        goto finishCheckingContent;
                    }
                }

                foundContent = false;

                finishCheckingContent:
                if (!foundContent)
                {
                    return false;
                }
            }

            var ft = this.FilterType;
            if (!string.IsNullOrEmpty(ft))
            {
                var et = e.Type;
                if (!et?.ToLowerInvariant()
                        .Contains(ft.ToLowerInvariant())
                    ?? true)
                {
                    return false;
                }
            }

            return true;
        }

        protected virtual void computeTotal(
            Lot<LogEntry> entries)
        {
            this.TotalEntryCount = entries.Count;
        }

        protected virtual void computeAvgPerDay(
            long entryCount,
            DateTime start,
            DateTime end)
        {
            if (entryCount < 1)
            {
                this.AvgEntriesPerDay = default;
                return;
            }

            double totalDays = (long)(end.Date - start.Date).TotalDays + 1;
            this.AvgEntriesPerDay = entryCount / totalDays;
        }

        protected virtual void computeOldestTimestamp(
            Lot<LogEntry> entries)
        {
            if (entries.Count < 1)
            {
                this.OldestTimestamp = default;
                return;
            }

            var oldest = DateTime.MaxValue;
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts < oldest)
                {
                    oldest = ts;
                }
            }

            this.OldestTimestamp = oldest;
        }

        protected virtual void computeNewestTimestamp(
            Lot<LogEntry> entries)
        {
            if (entries.Count < 1)
            {
                this.NewestTimestamp = default;
                return;
            }

            var newest = DateTime.MinValue;
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts > newest)
                {
                    newest = ts;
                }
            }

            this.NewestTimestamp = newest;
        }

        protected virtual void computeEarliestTimestamp(
            Lot<LogEntry> entries)
        {
            if (entries.Count < 1)
            {
                this.EarliestTimestamp = default;
                return;
            }

            var earliest = new DateTime(
                1, 
                1, 
                1, 
                23, 
                59, 
                59, 
                999);
            var earliestChanged = false;
            foreach (var entry in entries)
            {
                if (entry == null)
                {
                    continue;
                }

                var ts = entry.Timestamp;
                if (ts.TimeOfDay <= earliest.TimeOfDay)
                {
                    earliest = ts;
                    earliestChanged = true;
                }
            }

            if (!earliestChanged)
            {
                earliest = default;
            }

            this.EarliestTimestamp = earliest;
        }

        protected virtual void computeLatestTimestamp(
            Lot<LogEntry> entries)
        {
            if (entries.Count < 1)
            {
                this.LatestTimestamp = default;
                return;
            }

            var latest = new DateTime(
                1, 
                1, 
                1, 
                0, 
                0, 
                0);
            var latestChanged = false;
            foreach (var entry in entries)
            {
                if (entry == null)
                {
                    continue;
                }

                var ts = entry.Timestamp;
                if (ts.TimeOfDay >= latest.TimeOfDay)
                {
                    latest = ts;
                    latestChanged = true;
                }
            }

            if (!latestChanged)
            {
                latest = default;
            }

            this.LatestTimestamp = latest;
        }

        protected readonly MethodRunner runner;
    }
}
