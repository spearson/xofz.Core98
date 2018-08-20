namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;

    public class LogEntry
    {
        public LogEntry(
            string type, 
            ICollection<string> content)
            : this(DateTime.Now, type, content)
        {
        }

        public LogEntry(
            DateTime timestamp, 
            string type, 
            ICollection<string> content)
        {
            this.Timestamp = timestamp;
            this.Type = type;
            this.Content = content;
        }

        public virtual DateTime Timestamp { get; }

        public virtual string Type { get; }

        public virtual ICollection<string> Content { get; }
    }
}
