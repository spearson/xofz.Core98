namespace xofz.Framework.Logging.Logs
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using xofz.Framework.Lots;

    // note: this class requires Windows 2000 or above
    public sealed class EventLogLog 
        : Log, LogEditor
    {
        public EventLogLog(
            string logName,
            string sourceName)
        {
            this.eventLog = new EventLog(logName)
            {
                Source = sourceName
            };
        }

        public event Do<LogEntry> EntryWritten;

        public event Do Cleared;

        IEnumerable<LogEntry> Log.ReadEntries()
        {
            var el = this.eventLog;
            foreach (EventLogEntry entry in el.Entries)
            {
                if (entry.Source != el.Source)
                {
                    continue;
                }

                yield return new LogEntry(
                    entry.TimeWritten,
                    getEntryType(entry),
                    new XLinkedListLot<string>(
                        XLinkedList<string>.Create(
                        new[]
                        {
                            entry.Message
                        })));
            }
        }

        private static string getEntryType(
            EventLogEntry entry)
        {
            switch (entry.EntryType)
            {
                case EventLogEntryType.Information:
                    return DefaultEntryTypes.Information;
                case EventLogEntryType.Error:
                    return DefaultEntryTypes.Error;
                case EventLogEntryType.Warning:
                    return DefaultEntryTypes.Warning;
                case EventLogEntryType.SuccessAudit:
                    return DefaultEntryTypes.SuccessAudit;
                case EventLogEntryType.FailureAudit:
                    return DefaultEntryTypes.FailureAudit;
            }

            return DefaultEntryTypes.Information;
        }

        ICollection<LogEntry> Log.ReadEntries(
            System.DateTime oldestTimestamp)
        {
            Log log = this;
            ICollection<LogEntry> collection = new XLinkedList<LogEntry>();
            foreach (var entry in EnumerableHelpers.OrderByDescending(
                log.ReadEntries(),
                e => e.Timestamp))
            {
                if (entry.Timestamp < oldestTimestamp)
                {
                    break;
                }

                collection.Add(entry);
            }

            return collection;
        }

        void LogEditor.AddEntry(
            string type, 
            IEnumerable<string> content)
        {
            var entry = new LogEntry(
                type,
                new XLinkedListLot<string>(
                    XLinkedList<string>.Create(
                    content)));
            LogEditor editor = this;
            editor.AddEntry(entry);
        }

        void LogEditor.AddEntry(
            LogEntry entry)
        {
            this.eventLog.WriteEntry(
                string.Join(
                    System.Environment.NewLine,
                    EnumerableHelpers.ToArray(entry.Content)),
                getEventLogEntryType(entry.Type));
            this.EntryWritten?.Invoke(entry);
        }

        private static EventLogEntryType getEventLogEntryType(
            string type)
        {
            switch (type)
            {
                case DefaultEntryTypes.Warning:
                    return EventLogEntryType.Warning;
                case DefaultEntryTypes.Error:
                    return EventLogEntryType.Error;
                case DefaultEntryTypes.FailureAudit:
                    return EventLogEntryType.FailureAudit;
                case DefaultEntryTypes.SuccessAudit:
                    return EventLogEntryType.SuccessAudit;
            }

            return EventLogEntryType.Information;
        }


        void LogEditor.Clear()
        {
            this.eventLog.Clear();
            this.Cleared?.Invoke();
        }

        void LogEditor.Clear(
            string backupLocation)
        {
            LogEditor newLog = new TextFileLog(backupLocation);
            var oldLog = this.eventLog;

            foreach (EventLogEntry entry in oldLog.Entries)
            {
                newLog.AddEntry(
                    new LogEntry(
                        entry.TimeWritten,
                        getEntryType(entry),
                        new XLinkedListLot<string>(
                            XLinkedList<string>.Create(
                                new[]
                                {
                                    entry.Message
                                }))));
            }

            oldLog.Clear();
            this.Cleared?.Invoke();
        }

        private readonly EventLog eventLog;
    }
}
