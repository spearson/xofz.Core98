namespace xofz
{
    using System;
    using System.Collections.Generic;

    public class ExceptionHelpers
    {
        public static IEnumerable<string> TrimmedStackTraceFor(
            Exception e)
        {
            var st = e?.StackTrace;
            if (st == null)
            {
                yield break;
            }

            var untrimmedLines = st.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in untrimmedLines)
            {
                yield return line.Trim();
            }
        }
    }
}
