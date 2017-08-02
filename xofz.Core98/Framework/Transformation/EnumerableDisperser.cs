namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableDisperser
    {
        public virtual IEnumerable<T> Disperse<T>(
            IEnumerable<T> source,
            IEnumerable<T> dispersion,
            MaterializedEnumerable<int> dispersionPoints)
        {
            var e = dispersion.GetEnumerator();
            var counter = 0;
            foreach (var item in source)
            {
                yield return item;
                ++counter;
                e.MoveNext();
                foreach (var dp in dispersionPoints)
                {
                    if (dp == counter)
                    {
                        yield return e.Current;
                    }
                }
            }

            e.Dispose();
        }
    }
}
