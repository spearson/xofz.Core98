namespace xofz.Framework.Logging
{
    using xofz.Framework.Logging.Logs;

    public class LogFactory
    {
        public virtual Tuple<Log, LogEditor> CreateTextFileLog(
            string filePath)
        {
            var log = new TextFileLog(filePath);
            return Tuple.Create<Log, LogEditor>(log, log);
        }

        public virtual Tuple<Log, LogEditor> CreateEventLogLog(
            string logName,
            string sourceName)
        {
            var log = new EventLogLog(logName, sourceName);
            return Tuple.Create<Log, LogEditor>(log, log);
        }
    }
}
