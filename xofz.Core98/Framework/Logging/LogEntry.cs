namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class LogEntry
    {
        public LogEntry(
            string type,
            IEnumerable<string> finiteContent)
            : this(
                DateTime.Now,
                type,
                finiteContent)
        {
        }

        public LogEntry(
            string type, 
            Lot<string> content)
            : this(
                DateTime.Now, 
                type, 
                content)
        {
        }

        public LogEntry(
            DateTime timestamp,
            string type,
            IEnumerable<string> finiteContent)
            : this(
                timestamp,
                type,
                new XLinkedListLot<string>(
                    XLinkedList<string>.Create(
                        finiteContent)))
        {
        }

        public LogEntry(
            DateTime timestamp, 
            string type, 
            Lot<string> content)
        {
            this.Timestamp = timestamp;
            this.Type = type;
            this.Content = content;
        }

        public virtual DateTime Timestamp { get; }

        public virtual string Type { get; }

        public virtual Lot<string> Content { get; }
    }
}
