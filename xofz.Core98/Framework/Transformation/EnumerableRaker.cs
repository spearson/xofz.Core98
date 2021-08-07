namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableRaker
    {
        public virtual IEnumerable<T> Rake<T>(
            IEnumerable<T> source,
            IEnumerable<long> rakePoints)
        {
            var e = source?.GetEnumerator();
            if (e == null)
            {
                yield break;
            }

            if (rakePoints == null)
            {
                while (e.MoveNext())
                {
                    yield return e.Current;
                }

                e.Dispose();
                yield break;
            }

            long counter = zero;
            foreach (var rakePoint in rakePoints)
            {
                while (counter < rakePoint)
                {
                    e.MoveNext();
                    ++counter;
                }

                yield return e.Current;
                counter = zero;
            }

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            e.Dispose();
        }

        public virtual IEnumerable<T> InverseRake<T>(
            IEnumerable<T> source,
            IEnumerable<int> passPoints)
        {
            var e = source?.GetEnumerator();
            if (e == null)
            {
                yield break;
            }

            if (passPoints == null)
            {
                while (e.MoveNext())
                {
                    yield return e.Current;
                }

                e.Dispose();
                yield break;
            }

            int counter = zero;
            foreach (var pp in passPoints)
            {
                while (counter < pp)
                {
                    e.MoveNext();
                    yield return e.Current;
                    ++counter;
                }

                counter = zero;
            }

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            e.Dispose();
        }

        protected const byte zero = 0;
    }
}
