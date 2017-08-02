namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableRaker
    {
        public virtual IEnumerable<T> Rake<T>(
            IEnumerable<T> source,
            IEnumerable<int> rakePoints)
        {
            var e = source.GetEnumerator();
            var counter = 0;
            foreach (var rp in rakePoints)
            {
                while (counter < rp)
                {
                    e.MoveNext();
                    ++counter;
                }

                yield return e.Current;
                counter = 0;
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
            var e = source.GetEnumerator();
            var counter = 0;
            foreach (var pp in passPoints)
            {
                while (counter < pp)
                {
                    e.MoveNext();
                    yield return e.Current;
                    ++counter;
                }

                counter = 0;
            }

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            e.Dispose();
        }
    }
}
