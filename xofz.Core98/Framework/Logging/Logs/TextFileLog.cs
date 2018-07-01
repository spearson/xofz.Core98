namespace xofz.Framework.Logging.Logs
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using xofz.Framework.Materialization;

    public sealed class TextFileLog 
        : Log, LogEditor
    {
        public TextFileLog(string filePath)
        {
            this.filePath = filePath;
        }

        public event Action<LogEntry> EntryWritten;

        IEnumerable<LogEntry> Log.ReadEntries()
        {
            lock (this.locker)
            {
                if (!File.Exists(this.filePath))
                {
                    yield break;
                }

                using (var reader = File.OpenText(this.filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string timestampString;
                        while ((timestampString = reader.ReadLine()) == string.Empty)
                        {
                        }

                        var type = reader.ReadLine();
                        var content = new LinkedList<string>();
                        string contentLine;
                        readContent:
                        while ((contentLine = reader.ReadLine()) != string.Empty
                               && contentLine != null)
                        {
                            content.AddLast(contentLine);
                        }

                        if (contentLine == string.Empty)
                        {
                            if (!string.IsNullOrEmpty(contentLine = reader.ReadLine()))
                            {
                                content.AddLast(string.Empty);
                                content.AddLast(contentLine);
                                goto readContent;
                            }
                        }

                        DateTime timestamp;
                        if (DateTime.TryParseExact(
                            timestampString,
                            this.newTimestampFormat,
                            CultureInfo.CurrentCulture,
                            DateTimeStyles.AllowWhiteSpaces,
                            out timestamp))
                        {
                            yield return new LogEntry(
                                timestamp,
                                type,
                                new LinkedListMaterializedEnumerable<string>(content));
                            continue;
                        }

                        if (DateTime.TryParseExact(
                            timestampString,
                            this.timestampFormat,
                            CultureInfo.CurrentCulture,
                            DateTimeStyles.AllowWhiteSpaces,
                            out timestamp))
                        {
                            yield return new LogEntry(
                                timestamp,
                                type,
                                new LinkedListMaterializedEnumerable<string>(content));
                        }
                    }
                }
            }
        }

        MaterializedEnumerable<LogEntry> Log.ReadEntries(
            DateTime oldestTimestamp)
        {
            Log log = this;
            var ll = new LinkedList<LogEntry>();
            foreach (var entry in EnumerableHelpers.OrderByDescending(
                log.ReadEntries(),
                e => e.Timestamp))
            {
                if (entry.Timestamp < oldestTimestamp)
                {
                    break;
                }

                ll.AddLast(entry);
            }

            return new LinkedListMaterializedEnumerable<LogEntry>(ll);
        }

        public event Action Cleared;

        void LogEditor.AddEntry(string type, IEnumerable<string> content)
        {
            LogEditor editor = this;
            editor.AddEntry(
                new LogEntry(
                    type,
                    new LinkedListMaterializedEnumerable<string>(
                        content)));
        }

        void LogEditor.AddEntry(LogEntry entry)
        {
            var lines = new LinkedList<string>();
            lines.AddLast(entry.Timestamp.ToString(this.newTimestampFormat));
            lines.AddLast(entry.Type);
            foreach (var line in entry.Content)
            {
                lines.AddLast(line);
            }

            lines.AddLast(string.Empty);
            lines.AddLast(string.Empty);
            lines.AddLast(string.Empty);

            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.Append(line);
                sb.Append(Environment.NewLine);
            }

            lock (this.locker)
            {
                File.AppendAllText(this.filePath, sb.ToString());
            }

            this.EntryWritten?.Invoke(entry);
        }

        void LogEditor.Clear()
        {
            lock (this.locker)
            {
                if (File.Exists(this.filePath))
                {
                    File.Delete(this.filePath);
                }
            }

            this.Cleared?.Invoke();
        }

        void LogEditor.Clear(string backupLocation)
        {
            lock (this.locker)
            {
                var path = this.filePath;
                if (!File.Exists(path))
                {
                    return;
                }

                File.Copy(path, backupLocation);
                File.Delete(path);
            }

            this.Cleared?.Invoke();
        }

        private readonly string filePath;
        private readonly string timestampFormat = "yyyy MMMM dd hh:mm.ss tt";
        private readonly string newTimestampFormat = "yyyy MMMM dd hh:mm:ss.fffffff tt";
        private readonly object locker = new object();
    }
}
