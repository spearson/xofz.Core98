namespace xofz
{
    using System;
    using System.Collections.Generic;

    public static class StringHelpers
    {
        public static T ToEnum<T>(
            string s)
            where T : struct
        {
            if (s == null)
            {
                return default;
            }

            try
            {
                return (T)Enum.Parse(typeof(T), s);
            }
            catch
            {
                return default;
            }
        }

        public static string RemoveEndChars(
            string s,
            int count)
        {
            if (s == null)
            {
                return null;
            }

            if (count < 1)
            {
                return s;
            }

            var l = s.Length;
            return count >= l
                ? string.Empty
                : s.Substring(
                    0,
                    l - count);
        }

        public static bool NullOrWhiteSpace(
            string s)
        {
            if (s == null)
            {
                return true;
            }

            foreach (var c in s)
            {
                if (!char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<string> Chunks(
            string s, 
            int chunkSize)
        {
            if (s == null)
            {
                yield break;
            }

            if (chunkSize < 1)
            {
                yield return s;
            }

            var l = s.Length;
            int i;
            for (i = 0; i < l - chunkSize; i += chunkSize)
            {
                yield return s.Substring(i, chunkSize);
            }

            if (i < l)
            {
                yield return s.Substring(i, l - i);
            }
        }
    }
}
