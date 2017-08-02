namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableStriker
    {
        public virtual IEnumerable<T> Strike<T, K>(
            IEnumerable<T> tSource,
            IEnumerable<K> kSource,
            Func<T, K, T> strike)
        {
            var te = tSource.GetEnumerator();
            var ke = kSource.GetEnumerator();
            while (te.MoveNext() || ke.MoveNext())
            {
                yield return strike(te.Current, ke.Current);
            }

            te.Dispose();
            ke.Dispose();
        }

        public virtual IEnumerable<K> Strike<T, K>(
            IEnumerable<T> tSource,
            IEnumerable<K> kSource,
            Func<T, K, K> strike)
        {
            var te = tSource.GetEnumerator();
            var ke = kSource.GetEnumerator();
            while (te.MoveNext() || ke.MoveNext())
            {
                yield return strike(te.Current, ke.Current);
            }

            te.Dispose();
            ke.Dispose();
        }
    }
}
