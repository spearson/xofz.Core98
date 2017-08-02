namespace xofz
{
    using System;

    public static class StringHelpers
    {
        public static T ToEnum<T>(string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

        public static string RemoveEndChars(string s, int count)
        {
            if (s == null)
            {
                return null;
            }

            return count >= s.Length
                ? string.Empty
                : s.Substring(
                    0, 
                    s.Length - count);
        }

        public static bool NullOrWhiteSpace(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            var allWhiteSpace = true;
            foreach (var c in s)
            {
                if (!char.IsWhiteSpace(c))
                {
                    allWhiteSpace = false;
                }
            }

            return allWhiteSpace;
        }
    }
}
