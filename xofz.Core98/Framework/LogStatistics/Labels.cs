namespace xofz.Framework.LogStatistics
{
    public class Labels
    {
        public virtual string Overall { get; set; }
            = @"Overall";

        public virtual string Range(
            System.DateTime start,
            System.DateTime end,
            string dateFormat)
        {
            return @"Range: "
                   + start.ToString(dateFormat)
                   + @" to "
                   + end.ToString(dateFormat);
        }

        public virtual string NoEntriesInRange { get; set; }
            = @"No entries in range";

        public virtual string StartLabel { get; set; }
            = @"Start:";

        public virtual string EndLabel { get; set; }
            = @"End:";

        public virtual string HideKey { get; set; }
            = @"Hide";

        public virtual string OverallKey { get; set; }
            = @"Compute Overall";

        public virtual string RangeKey { get; set; }
            = @"Compute Range";

        public virtual string FilterContentLabel { get; set; }
            = @"Filter on Content:";

        public virtual string FilterTypeLabel { get; set; }
            = @"Filter on Type:";

        public virtual string ResetContentKey { get; set; }
            = @"Reset";

        public virtual string ResetTypeKey { get; set; }
            = @"Reset";

        public virtual string StatsContainer { get; set; }
            = @"Statistics";

        public virtual string TotalEntryCountLabelLabel { get; set; }
            = @"Total entry count:";

        public virtual string AvgEntriesPerDayLabelLabel { get; set; }
            = @"Avg. # of entries per day:";

        public virtual string OldestTimestampLabelLabel { get; set; }
            = @"Oldest entry timestamp:";

        public virtual string NewestTimestampLabelLabel { get; set; }
            = @"Newest entry timestamp:";

        public virtual string EarliestTimestampLabelLabel { get; set; }
            = @"Earliest timestamp:";

        public virtual string LatestTimestampLabelLabel { get; set; }
            = @"Latest timestamp:";

        public virtual string Label { get; set; }
            = @"Log Statistics";
    }
}