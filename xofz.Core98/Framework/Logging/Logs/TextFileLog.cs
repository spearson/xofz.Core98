namespace xofz.Framework.Logging.Logs
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using xofz.Framework.Lots;

    public sealed class TextFileLog 
        : Log, LogEditor
    {
        public TextFileLog(
            string filePath)
        {
            this.filePath = filePath;
            this.locker = new object();
        }

        public event Do<LogEntry> EntryWritten;

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
                        ICollection<string> content = new XLinkedList<string>();
                        string contentLine;
                        readContent:
                        while ((contentLine = reader.ReadLine()) != string.Empty
                               && contentLine != null)
                        {
                            content.Add(contentLine);
                        }

                        // support up to two blank lines in entries
                        if (contentLine == string.Empty)
                        {
                            if (!string.IsNullOrEmpty(contentLine = reader.ReadLine()))
                            {
                                content.Add(string.Empty);
                                content.Add(contentLine);
                                goto readContent;
                            }

                            if (contentLine == string.Empty)
                            {
                                if (!string.IsNullOrEmpty(contentLine = reader.ReadLine()))
                                {
                                    content.Add(string.Empty);
                                    content.Add(string.Empty);
                                    content.Add(contentLine);
                                    goto readContent;
                                }
                            }
                        }

                        System.DateTime timestamp;
                        if (System.DateTime.TryParseExact(
                            timestampString,
                            timestampFormatV2,
                            CultureInfo.CurrentCulture,
                            DateTimeStyles.AllowWhiteSpaces,
                            out timestamp))
                        {
                            yield return new LogEntry(
                                timestamp,
                                type,
                                new LinkedListLot<string>(
                                    content));
                            continue;
                        }

                        if (System.DateTime.TryParseExact(
                            timestampString,
                            timestampFormat,
                            CultureInfo.CurrentCulture,
                            DateTimeStyles.AllowWhiteSpaces,
                            out timestamp))
                        {
                            yield return new LogEntry(
                                timestamp,
                                type,
                                new LinkedListLot<string>(content));
                        }
                    }
                }
            }
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

        public event Do Cleared;

        void LogEditor.AddEntry(
            string type, 
            IEnumerable<string> content)
        {
            LogEditor editor = this;
            editor.AddEntry(
                new LogEntry(
                    type,
                    new LinkedListLot<string>(content)));
        }

        void LogEditor.AddEntry(
            LogEntry entry)
        {
            var lines = new XLinkedList<string>();
            lines.AddTail(entry.Timestamp.ToString(timestampFormatV2));
            lines.AddTail(entry.Type);
            foreach (var line in entry.Content)
            {
                lines.AddTail(line);
            }

            lines.AddTail(string.Empty);
            lines.AddTail(string.Empty);
            lines.AddTail(string.Empty);

            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.Append(line);
                sb.Append(System.Environment.NewLine);
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

        void LogEditor.Clear(
            string backupLocation)
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
        private readonly object locker;
        private const string timestampFormat = "yyyy MMMM dd hh:mm.ss tt";
        private const string timestampFormatV2 = "yyyy MMMM dd hh:mm:ss.fffffff tt";
    }
}
