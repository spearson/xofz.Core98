namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;

    public class LogHelpers
    {
        public static void AddEntry(
            LogEditor logEditor,
            UnhandledExceptionEventArgs e)
        {
            if (e == null)
            {
                logEditor?.AddEntry(
                    DefaultEntryTypes.Error,
                    new[]
                    {
                        @"An unhandled exception occurred, "
                        + @"but the event args object was null."
                    });
                return;
            }

            var eo = e.ExceptionObject;
            if (eo == null)
            {
                logEditor?.AddEntry(
                    DefaultEntryTypes.Error,
                    new[]
                    {
                        @"An unhandled exception occurred, "
                        + @"but the exception object was null."
                    });
                return;
            }

            var ex = eo as Exception;
            if (ex == null)
            {
                logEditor?.AddEntry(
                    DefaultEntryTypes.Error,
                    new[]
                    {
                        @"An unhandled exception occurred, but the exception "
                        + @"did not derive from System.Exception.",
                        @"Here is the exception's type: "
                        + eo.GetType()
                    });
                return;
            }

            AddEntry(logEditor, ex);
        }

        public static void AddEntry(
            LogEditor logEditor,
            Exception e)
        {
            var content = new List<string>
            {
                e.GetType().ToString(),
                e.Message,
                string.Empty,
                StackTraceHeader,
            };
            content.AddRange(ExceptionHelpers
                .TrimmedStackTraceFor(e));

            var ie = e.InnerException;
            if (ie != null)
            {
                content.Add(string.Empty);
                content.Add(string.Empty);
                content.Add(@"Inner exception: " + ie.GetType());
                content.Add(ie.Message);
                content.Add(string.Empty);
                content.Add(StackTraceHeader);
                content.AddRange(ExceptionHelpers
                    .TrimmedStackTraceFor(ie));
            }

            logEditor?.AddEntry(
                DefaultEntryTypes.Error,
                content.ToArray());
        }

        protected const string StackTraceHeader = @"Stack trace:";
    }
}
