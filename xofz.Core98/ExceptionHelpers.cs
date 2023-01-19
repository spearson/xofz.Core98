namespace xofz
{
    using System.Collections.Generic;

    public class ExceptionHelpers
    {
        public static IEnumerable<string> TrimmedStackTraceFor(
            System.Exception e)
        {
            var st = e?.StackTrace;
            if (st == null)
            {
                yield break;
            }

            var untrimmedLines = st.Split(
                new[] { System.Environment.NewLine },
                System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in untrimmedLines)
            {
                yield return line?.Trim();
            }
        }
    }
}
