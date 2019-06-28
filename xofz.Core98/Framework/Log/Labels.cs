namespace xofz.Framework.Log
{
    public class Labels
    {
        public virtual string ClearNoBackupQuestion { get; set; }
            = @"Really clear the log? "
              + @"A backup will NOT be created.";

        public virtual string ClearWithBackupQuestion { get; set; }
            = @"Clear log? "
              + @"A backup will be created.";

        public virtual string ClearedNoBackup { get; set; }
            = @"The log was cleared.";

        public virtual string ClearedWithBackup(
            string backupLocation)
        {
            return @"The log was cleared. A backup was created at "
                   + backupLocation + '.';
        }

        public virtual string StartLabel { get; set; }
            = @"Start:";

        public virtual string EndLabel { get; set; }
            = @"End:";

        public virtual string ClearKey { get; set; }
            = @"Clear Log";

        public virtual string StatsKey { get; set; }
            = @"Statistics";

        public virtual string AddKey { get; set; }
            = @"Add Entry";

        public virtual string PreviousWeekKey { get; set; }
            = @"<< Previous Week";

        public virtual string NextWeekKey { get; set; }
            = @"Next Week >>";

        public virtual string CurrentWeekKey { get; set; }
            = @"Current";

        public virtual string FilterContentLabel { get; set; }
            = @"Filter on Content:";

        public virtual string FilterTypeLabel { get; set; }
            = @"Filter on Type:";

        public virtual string ResetContentKey { get; set; }
            = @"Reset";

        public virtual string ResetTypeKey { get; set; }
            = @"Reset";

        public virtual string TimestampColumnHeader { get; set; }
            = @"Timestamp";

        public virtual string TypeColumnHeader { get; set; }
            = @"Type";

        public virtual string ContentColumnHeader { get; set; }
            = @"Content";
    }
}
