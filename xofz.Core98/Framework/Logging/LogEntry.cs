namespace xofz.Framework.Logging
{
    using System;

    public class LogEntry
    {
        public LogEntry(
            string type, 
            MaterializedEnumerable<string> content)
            : this(DateTime.Now, type, content)
        {
        }

        public LogEntry(
            DateTime timestamp, 
            string type, 
            MaterializedEnumerable<string> content)
        {
            this.Timestamp = timestamp;
            this.Type = type;
            this.Content = content;
        }

        public virtual DateTime Timestamp { get; }

        public virtual string Type { get; }

        public virtual MaterializedEnumerable<string> Content { get; }
    }
}
