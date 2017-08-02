namespace xofz.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Materialization;

    public sealed class TextFileLog : Log, LogEditor
    {
        public TextFileLog(string filePath)
        {
            this.filePath = filePath;
        }
        
        public event Action<LogEntry> EntryWritten;

        public IEnumerable<LogEntry> ReadEntries()
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

        public MaterializedEnumerable<LogEntry> ReadEntries(DateTime oldestTimestamp)
        {
            var ll = new LinkedList<LogEntry>();
            foreach (var entry in this.ReadEntries())
            {
                if (entry.Timestamp >= oldestTimestamp)
                {
                    ll.AddLast(entry);
                }
            }

            return new LinkedListMaterializedEnumerable<LogEntry>(ll);
        }

        public void AddEntry(string type, IEnumerable<string> content)
        {
            this.AddEntry(
                new LogEntry(
                    type,
                    new LinkedListMaterializedEnumerable<string>(
                        content)));
        }

        public void AddEntry(LogEntry entry)
        {
            var lines = new LinkedList<string>();
            lines.AddLast(entry.Timestamp.ToString(this.timestampFormat));
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

            File.AppendAllText(this.filePath, sb.ToString());
            new Thread(() => this.EntryWritten?.Invoke(entry)).Start();
        }

        private readonly string filePath;
        private readonly string timestampFormat = "yyyy MMMM dd hh:mm.ss tt";
    }
}
