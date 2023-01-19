namespace xofz
{
    using System.Collections.Generic;

    public static class StringHelpers
    {
        public static T ToEnum<T>(
            string s)
            where T : System.Enum
        {
            try
            {
                return (T)System.Enum.Parse(typeof(T), s);
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

            if (count < one)
            {
                return s;
            }

            var l = s.Length;
            return count >= l
                ? string.Empty
                : s.Substring(
                    zero,
                    l - count);
        }

        public static bool NullOrWhiteSpace(
            string s)
        {
            if (s == null)
            {
                return truth;
            }

            foreach (var c in s)
            {
                if (!char.IsWhiteSpace(c))
                {
                    return falsity;
                }
            }

            return truth;
        }

        public static IEnumerable<string> Chunks(
            string s, 
            int chunkSize)
        {
            if (s == null)
            {
                yield break;
            }

            if (chunkSize < one)
            {
                yield return s;
            }

            var l = s.Length;
            int i;
            for (i = zero; i < l - chunkSize; i += chunkSize)
            {
                yield return s.Substring(i, chunkSize);
            }

            if (i < l)
            {
                yield return s.Substring(i, l - i);
            }
        }

        private const byte 
            zero = 0,
            one = 1;
        private const bool 
            falsity = false,
            truth = true;
    }
}
