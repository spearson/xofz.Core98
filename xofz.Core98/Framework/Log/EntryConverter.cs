namespace xofz.Framework.Log
{
    using System;
    using System.Globalization;
    using xofz.Framework.Logging;

    public class EntryConverter
    {
        public EntryConverter(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual XTuple<string, string, string> Convert(LogEntry entry)
        {
            if (entry == null)
            {
                return XTuple.Create<string, string, string>(
                    null, null, null);
            }

            var w = this.web;
            string format = null;
            w.Run<SettingsHolder>(settings =>
            {
                format = settings.TimestampFormat;
            });

            return XTuple.Create(
                entry.Timestamp.ToString(
                    format,
                    CultureInfo.CurrentCulture),
                entry.Type,
                string.Join(
                    Environment.NewLine,
                    EnumerableHelpers.ToArray(entry.Content)));
        }

        protected readonly MethodWeb web;
    }
}
