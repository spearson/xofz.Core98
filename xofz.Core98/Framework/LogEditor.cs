namespace xofz.Framework
{
    using System.Collections.Generic;

    public interface LogEditor
    {
        void AddEntry(string type, IEnumerable<string> content);

        void AddEntry(LogEntry entry);
    }
}
