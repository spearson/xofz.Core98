namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableSkipper
    {
        public virtual IEnumerable<T> Skip<T>(
            IEnumerable<T> source, 
            long skipInterval)
        {
            if (skipInterval < one)
            {
                yield break;
            }

            var e = source?.GetEnumerator();
            if (e == null)
            {
                yield break;
            }

            long counter = zero;
            while (e.MoveNext())
            {
                ++counter;

                if (counter == skipInterval)
                {
                    counter = zero;
                    yield return e.Current;
                }
            }

            e.Dispose();
        }

        public virtual ICollection<T> SkipThrough<T>(
            IEnumerable<T> finiteSource, 
            int skipPoint)
        {
            if (finiteSource == null)
            {
                return new LinkedList<T>();
            }

            var ll = new LinkedList<T>(finiteSource);
            var result = new List<T>();
            for (int i = one; i <= skipPoint; ++i)
            {
                result.AddRange(this.Skip(ll, i));
            }

            return result;
        }

        protected const byte 
            zero = 0,
            one = 1;
    }
}
