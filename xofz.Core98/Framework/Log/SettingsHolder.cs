namespace xofz.Framework.Log
{
    public class SettingsHolder
    {
        public virtual string LogLocation { get; set; }

        public virtual string SecondaryLogLocation { get; set; }

        public virtual string LogDependencyName { get; set; }

        public virtual AccessLevel EditLevel { get; set; }

        public virtual AccessLevel ClearLevel { get; set; }

        public virtual bool ResetOnStart { get; set; }

        public virtual bool StatisticsEnabled { get; set; }

        public virtual Gen<string> ComputeBackupLocation { get; set; }

        public virtual string TimestampFormat { get; set; }
            = @"yyyy/MM/dd HH:mm.ss.fffffff";
    }
}