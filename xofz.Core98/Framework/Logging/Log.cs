﻿namespace xofz.Framework.Logging
{
    using System.Collections.Generic;

    public interface Log
    {
        event Action<LogEntry> EntryWritten;

        IEnumerable<LogEntry> ReadEntries();

        ICollection<LogEntry> ReadEntries(System.DateTime oldestTimestamp);
    }
}
