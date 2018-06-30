﻿namespace xofz
{
    using System;

    public static class StringHelpers
    {
        public static T ToEnum<T>(string s)
            where T : struct
        {
            if (s == null)
            {
                return default(T);
            }

            try
            {
                return (T)Enum.Parse(typeof(T), s);
            }
            catch
            {
                return default(T);
            }
            
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
                    break;
                }
            }

            return allWhiteSpace;
        }
    }
}
