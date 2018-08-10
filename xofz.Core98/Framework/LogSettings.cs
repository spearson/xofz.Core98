namespace xofz.Framework
{
    public class LogSettings
    {
        public virtual AccessLevel EditLevel { get; set; }

        public virtual AccessLevel ClearLevel { get; set; }

        public virtual Func<string> ComputeBackupLocation { get; set; }

        public virtual bool ResetOnStart { get; set; }

        public virtual bool StatisticsEnabled { get; set; }
    }
}
