namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public class LogStatistics
    {
        public LogStatistics(MethodWeb web)
        {
            this.web = web;
        }

        public virtual string LogName { get; set; }

        public virtual long TotalEntryCount { get; set; }

        public virtual double AvgEntriesPerDay { get; protected set; }

        public virtual string FilterContent { get; set; }

        public virtual string FilterType { get; set; }

        public virtual DateTime OldestTimestamp { get; protected set; }

        public virtual DateTime NewestTimestamp { get; protected set; }

        public virtual DateTime EarliestTimestamp { get; protected set; }

        public virtual DateTime LatestTimestamp { get; protected set; }

        public virtual void ComputeOverall()
        {
            var w = this.web;
            w.Run<Log>(l =>
                {
                    var ll = new LinkedList<LogEntry>();
                    foreach (var entry in l.ReadEntries())
                    {
                        if (this.passesFilters(entry))
                        {
                            ll.AddLast(entry);
                        }
                    }

                    MaterializedEnumerable<LogEntry> matches
                        = new LinkedListMaterializedEnumerable<LogEntry>(ll);

                    var start = DateTime.MaxValue;
                    var end = DateTime.MinValue;
                    foreach (var entry in ll)
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
                this.LogName);
        }

        public virtual void ComputeRange(
            DateTime startDate,
            DateTime endDate)
        {
            var w = this.web;
            w.Run<Log>(l =>
                {
                    var ll = new LinkedList<LogEntry>();
                    foreach (var entry in l.ReadEntries())
                    {
                        if (this.passesFilters(entry)
                            && entry.Timestamp >= startDate
                            && entry.Timestamp < endDate.AddDays(1))
                        {
                            ll.AddLast(entry);
                        }
                    }

                    MaterializedEnumerable<LogEntry> matches
                        = new LinkedListMaterializedEnumerable<LogEntry>(ll);

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
                this.LogName);
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

        private bool passesFilters(LogEntry e)
        {
            var fc = this.FilterContent;
            if (!string.IsNullOrEmpty(fc))
            {
                var found = false;
                foreach (var line in e.Content)
                {
                    if (line.ToLowerInvariant()
                        .Contains(
                            fc.ToLowerInvariant()))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }

            var ft = this.FilterType;
            if (!string.IsNullOrEmpty(ft))
            {
                if (!e.Type.ToLowerInvariant()
                    .Contains(ft.ToLowerInvariant()))
                {
                    return false;
                }
            }

            return true;
        }

        private void computeTotal(MaterializedEnumerable<LogEntry> entries)
        {
            this.TotalEntryCount = entries.Count;
        }

        private void computeAvgPerDay(
            long entryCount,
            DateTime start,
            DateTime end)
        {
            if (entryCount == 0)
            {
                this.AvgEntriesPerDay = default(double);
                return;
            }

            var totalDays = (end - start).TotalDays + 1;
            this.AvgEntriesPerDay = entryCount / totalDays;
        }

        private void computeOldestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.OldestTimestamp = default(DateTime);
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

        private void computeNewestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.NewestTimestamp = default(DateTime);
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

        private void computeEarliestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.EarliestTimestamp = default(DateTime);
                return;
            }

            var earliest = new DateTime(1, 1, 1, 23, 59, 59);
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts.TimeOfDay < earliest.TimeOfDay)
                {
                    earliest = ts;
                }
            }

            this.EarliestTimestamp = earliest;
        }

        private void computeLatestTimestamp(MaterializedEnumerable<LogEntry> entries)
        {
            if (entries.Count == 0)
            {
                this.LatestTimestamp = default(DateTime);
                return;
            }

            var latest = new DateTime(1, 1, 1, 0, 0, 0);
            foreach (var entry in entries)
            {
                var ts = entry.Timestamp;
                if (ts.TimeOfDay > latest.TimeOfDay)
                {
                    latest = ts;
                }
            }

            this.LatestTimestamp = latest;
        }

        private readonly MethodWeb web;
    }
}
