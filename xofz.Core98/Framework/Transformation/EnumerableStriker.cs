namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableStriker
    {
        public virtual IEnumerable<T> Strike<T, K>(
            IEnumerable<T> tSource,
            IEnumerable<K> kSource,
            Gen<T, K, T> strike)
        {
            var te = tSource?.GetEnumerator();
            var ke = kSource?.GetEnumerator();
            if (te == null || ke == null)
            {
                yield break;
            }

            var strikeIsNull = strike == null;
            loopThrough:
            if (te.MoveNext())
            {
                if (ke.MoveNext())
                {
                    if (strikeIsNull)
                    {
                        yield return te.Current;
                        goto loopThrough;
                    }

                    yield return strike(te.Current, ke.Current);
                    goto loopThrough;
                }
            }

            te.Dispose();
            ke.Dispose();
        }

        public virtual IEnumerable<K> Strike<T, K>(
            IEnumerable<T> tSource,
            IEnumerable<K> kSource,
            Gen<T, K, K> strike)
        {
            var te = tSource?.GetEnumerator();
            var ke = kSource?.GetEnumerator();
            if (te == null || ke == null)
            {
                yield break;
            }

            var strikeIsNull = strike == null;
            loopThrough:
            if (te.MoveNext())
            {
                if (ke.MoveNext())
                {
                    if (strikeIsNull)
                    {
                        yield return ke.Current;
                        goto loopThrough;
                    }

                    yield return strike(te.Current, ke.Current);
                    goto loopThrough;
                }
            }

            te.Dispose();
            ke.Dispose();
        }
    }
}