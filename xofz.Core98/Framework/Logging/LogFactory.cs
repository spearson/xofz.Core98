namespace xofz.Framework.Logging
{
    using xofz.Framework.Logging.Logs;

    public class LogFactory
    {
        public virtual XTuple<Log, LogEditor> CreateTextFileLog(
            string filePath)
        {
            var log = new TextFileLog(filePath);
            return XTuple.Create<Log, LogEditor>(log, log);
        }

        public virtual XTuple<Log, LogEditor> CreateEventLogLog(
            string logName,
            string sourceName)
        {
            var log = new EventLogLog(logName, sourceName);
            return XTuple.Create<Log, LogEditor>(log, log);
        }
    }
}