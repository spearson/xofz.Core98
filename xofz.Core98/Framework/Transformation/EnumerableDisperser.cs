namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableDisperser
    {
        public virtual IEnumerable<T> Disperse<T>(
            IEnumerable<T> source,
            IEnumerable<T> dispersion,
            Lot<long> dispersionPoints)
        {
            if (source == null)
            {
                yield break;
            }

            if (dispersion == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            if (dispersionPoints == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long counter = 0;
            using (var e = dispersion.GetEnumerator())
            {
                if (e == null)
                {
                    foreach (var item in source)
                    {
                        yield return item;
                    }

                    yield break;
                }

                foreach (var item in source)
                {
                    foreach (var dp in dispersionPoints)
                    {
                        if (dp == counter)
                        {
                            e.MoveNext();
                            yield return e.Current;
                        }
                    }

                    ++counter;
                    yield return item;
                }
            }
        }
    }
}
