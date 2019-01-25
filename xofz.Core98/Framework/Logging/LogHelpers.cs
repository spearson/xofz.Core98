namespace xofz.Framework.Logging
{
    using System;
    using System.Collections.Generic;

    public static class LogHelpers
    {
        public static void AddEntry(
            LogEditor logEditor, 
            UnhandledExceptionEventArgs e)
        {
            if (e == null)
            {
                logEditor?.AddEntry(
                    "Error",
                    new[]
                    {
                        "An unhandled exception occurred, "
                        + "but the event args object was null."
                    });
                return;
            }

            var eo = e.ExceptionObject;
            if (eo == null)
            {
                logEditor?.AddEntry(
                    "Error",
                    new[]
                    {
                        "An unhandled exception occurred, "
                        + "but the exception object was null."
                    });
                return;
            }
            
            var ex = eo as Exception;
            if (ex == null)
            {
                logEditor?.AddEntry(
                    "Error",
                    new[]
                    {
                        "An unhandled exception occurred, but the exception "
                        + "did not derive from System.Exception.",
                        "Here is the exception's type: "
                        + eo.GetType()
                    });
                return;
            }

            AddEntry(logEditor, ex);
        }

        public static void AddEntry(LogEditor logEditor, Exception e)
        {
            var content = new List<string>
                              {
                                  e.GetType().ToString(),
                                  e.Message,
                                  string.Empty,
                                  "Stack trace:",
                              };
            content.AddRange(trimmedStackTraceFor(e));

            var ie = e.InnerException;

            if (ie != null)
            {
                content.Add(string.Empty);
                content.Add(string.Empty);
                content.Add("Inner exception: " + ie.GetType());
                content.Add(ie.Message);
                content.Add(string.Empty);
                content.Add("Stack trace:");
                content.AddRange(trimmedStackTraceFor(ie));
            }

            logEditor?.AddEntry("Error", content.ToArray());
        }

        private static IEnumerable<string> trimmedStackTraceFor(Exception e)
        {
            var st = e?.StackTrace;
            if (st == null)
            {
                yield break;
            }

            var untrimmedLines = st.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var utl in untrimmedLines)
            {
                yield return utl.Trim();
            }
        }
    }
}
