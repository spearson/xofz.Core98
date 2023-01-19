namespace xofz.Framework.Logging
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class LogEntry
    {
        public LogEntry(
            string type,
            IEnumerable<string> finiteContent)
            : this(
                System.DateTime.Now,
                type,
                finiteContent)
        {
        }

        public LogEntry(
            string type, 
            Lot<string> content)
            : this(
                System.DateTime.Now, 
                type, 
                content)
        {
        }

        public LogEntry(
            System.DateTime timestamp,
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
            System.DateTime timestamp, 
            string type, 
            Lot<string> content)
        {
            this.Timestamp = timestamp;
            this.Type = type;
            this.Content = content;
        }

        public virtual System.DateTime Timestamp { get; }

        public virtual string Type { get; }

        public virtual Lot<string> Content { get; }
    }
}
