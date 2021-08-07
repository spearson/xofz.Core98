namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableSkipper
    {
        public virtual IEnumerable<T> Skip<T>(
            IEnumerable<T> source, 
            long skipInterval)
        {
            if (source == null)
            {
                yield break;
            }

            var e = source.GetEnumerator();
            long counter = zero;
            while (e?.MoveNext() ?? false)
            {
                if (counter == skipInterval)
                {
                    counter = zero;
                    yield return e.Current;
                }

                ++counter;
            }

            e?.Dispose();
        }

        public virtual ICollection<T> SkipThrough<T>(
            IEnumerable<T> finiteSource, 
            int skipPoint)
        {
            var ll = new LinkedList<T>();
            if (finiteSource == null)
            {
                return ll;
            }

            var result = new List<T>();
            for (int i = zero; i < skipPoint; ++i)
            {
                result.AddRange(this.Skip(ll, skipPoint));
                ll.RemoveFirst();
            }

            return result;
        }

        protected const byte zero = 0;
    }
}
