namespace xofz.Framework.Log
{
    using System;
    using System.Globalization;
    using xofz.Framework.Logging;

    public class EntryConverter
    {
        public EntryConverter(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual XTuple<string, string, string> Convert(
            LogEntry entry)
        {
            return this.Convert(
                entry,
                null);
        }

        public virtual XTuple<string, string, string> Convert(
            LogEntry entry,
            string name)
        {
            if (entry == null)
            {
                return XTuple.Create<string, string, string>(
                    null, null, null);
            }

            var r = this.runner;
            string format = null;
            r.Run<SettingsHolder>(settings =>
                {
                    format = settings.TimestampFormat;
                },
                name);

            return XTuple.Create(
                entry.Timestamp.ToString(
                    format,
                    CultureInfo.CurrentCulture),
                entry.Type,
                string.Join(
                    Environment.NewLine,
                    EnumerableHelpers.ToArray(
                        entry.Content)));
        }

        protected readonly MethodRunner runner;
    }
}
