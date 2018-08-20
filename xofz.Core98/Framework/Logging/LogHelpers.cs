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
            var ex = e.ExceptionObject as Exception;
            if (ex == null)
            {
                logEditor.AddEntry(
                    "Error",
                    new[]
                    {
                        "An unhandled exception occurred, but the exception "
                        + "did not derive from System.Exception.",
                        "Here is the exception's type: "
                        + e.ExceptionObject.GetType()
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

            if (e.InnerException != null)
            {
                content.Add(string.Empty);
                content.Add(string.Empty);
                content.Add("Inner exception: " + e.InnerException.GetType());
                content.Add(e.InnerException.Message);
                content.Add(string.Empty);
                content.Add("Stack trace:");
                content.AddRange(trimmedStackTraceFor(e.InnerException));
            }

            logEditor.AddEntry("Error", content.ToArray());
        }

        private static IEnumerable<string> trimmedStackTraceFor(Exception e)
        {
            var untrimmedLines = e.StackTrace.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var utl in untrimmedLines)
            {
                yield return utl.Trim();
            }
        }
    }
}
