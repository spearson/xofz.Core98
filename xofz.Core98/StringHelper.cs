namespace xofz
{
    using System.Collections.Generic;
    using SH = StringHelpers;

    public class StringHelper
    {
        public virtual T ToEnum<T>(
            string s)
            where T : System.Enum
        {
            return SH.ToEnum<T>(
                s);
        }

        public virtual string RemoveEndChars(
            string s,
            int count)
        {
            return SH.RemoveEndChars(
                s,
                count);
        }

        public virtual bool NullOrEmpty(
            string s)
        {
            return SH.NullOrEmpty(
                s);
        }

        public virtual bool NullOrWhiteSpace(
            string s)
        {
            return SH.NullOrWhiteSpace(
                s);
        }

        public virtual IEnumerable<string> Chunks(
            string s,
            int chunkSize)
        {
            return SH.Chunks(
                s,
                chunkSize);
        }
    }
}