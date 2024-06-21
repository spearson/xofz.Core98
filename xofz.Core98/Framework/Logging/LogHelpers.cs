namespace xofz.Framework.Logging
{
    public class LogHelpers
    {
        public static void AddEntry(
            LogEditor logEditor,
            System.UnhandledExceptionEventArgs e)
        {
            if (e == null)
            {
                logEditor?.AddEntry(
                    ErrorType,
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
                    ErrorType,
                    new[]
                    {
                        @"An unhandled exception occurred, "
                        + @"but the exception object was null."
                    });
                return;
            }

            var ex = eo as System.Exception;
            if (ex == null)
            {
                logEditor?.AddEntry(
                    ErrorType,
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
            System.Exception e)
        {
            var content = XLinkedList<string>
                .Create(
                    new[]
                    {
                        e
                            .GetType()
                            .ToString(),
                        e.Message,
                        string.Empty,
                        StackTraceHeader
                    }
                );
            content.Append(
                ExceptionHelpers
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
                content.Append(ExceptionHelpers
                    .TrimmedStackTraceFor(ie));
            }

            logEditor?.AddEntry(
                ErrorType,
                content);
        }

        protected const string StackTraceHeader = @"Stack trace:";
        protected const string ErrorType = DefaultEntryTypes.Error;
    }
}