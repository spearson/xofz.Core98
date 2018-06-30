namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;

    public interface Log
    {
        event Action<LogEntry> EntryWritten;

        IEnumerable<LogEntry> ReadEntries();

        MaterializedEnumerable<LogEntry> ReadEntries(DateTime oldestTimestamp);
    }
}
