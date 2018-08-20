﻿namespace xofz.Framework.Logging.Logs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

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

        public event Action<LogEntry> EntryWritten;

        public event Action Cleared;

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
                    new LinkedList<string>(
                        new[]
                        {
                            entry.Message
                        }));
            }
        }

        private static string getEntryType(EventLogEntry entry)
        {
            switch (entry.EntryType)
            {
                case EventLogEntryType.Information:
                    return "Information";
                case EventLogEntryType.Error:
                    return "Error";
                case EventLogEntryType.Warning:
                    return "Warning";
                case EventLogEntryType.SuccessAudit:
                    return "Successful Audit";
                case EventLogEntryType.FailureAudit:
                    return "Audit Failure";
            }

            return "Information";
        }

        ICollection<LogEntry> Log.ReadEntries(
            DateTime oldestTimestamp)
        {
            Log log = this;
            ICollection<LogEntry> collection = new LinkedList<LogEntry>();
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
                new LinkedList<string>(content));
            LogEditor editor = this;
            editor.AddEntry(entry);
        }

        void LogEditor.AddEntry(LogEntry entry)
        {
            this.eventLog.WriteEntry(
                string.Join(
                    Environment.NewLine,
                    EnumerableHelpers.ToArray(entry.Content)),
                getEventLogEntryType(entry.Type));
            this.EntryWritten?.Invoke(entry);
        }

        private static EventLogEntryType getEventLogEntryType(string type)
        {
            switch (type)
            {
                case "Warning":
                    return EventLogEntryType.Warning;
                case "Error":
                    return EventLogEntryType.Error;
                case "Audit Failure":
                    return EventLogEntryType.FailureAudit;
                case "Successful Audit":
                    return EventLogEntryType.SuccessAudit;
            }

            return EventLogEntryType.Information;
        }


        void LogEditor.Clear()
        {
            this.eventLog.Clear();
            this.Cleared?.Invoke();
        }

        void LogEditor.Clear(string backupLocation)
        {
            LogEditor newLog = new TextFileLog(backupLocation);
            var oldLog = this.eventLog;

            foreach (EventLogEntry entry in oldLog.Entries)
            {
                newLog.AddEntry(
                    new LogEntry(
                        entry.TimeWritten,
                        getEntryType(entry),
                        new LinkedList<string>(
                            new[]
                            {
                                entry.Message
                            })
                    ));
            }

            oldLog.Clear();
            this.Cleared?.Invoke();
        }

        private readonly EventLog eventLog;
    }
}
