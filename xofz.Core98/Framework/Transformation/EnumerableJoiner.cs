namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableJoiner
    {
        public virtual IEnumerable<Tuple<T, Y>> Join2<T, Y>(
            IEnumerable<T> source1, 
            IEnumerable<Y> source2)
        {
            var e1 = source1.GetEnumerator();
            var e2 = source2.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return Tuple.Create(e1.Current, e2.Current);
            }

            e1.Dispose();
            e2.Dispose();
        }

        public virtual IEnumerable<Tuple<T, Y, X>> Join3<T, Y, X>(
            IEnumerable<T> source1,
            IEnumerable<Y> source2,
            IEnumerable<X> source3)
        {
            var e1 = source1.GetEnumerator();
            var e2 = source2.GetEnumerator();
            var e3 = source3.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
            {
                yield return Tuple.Create(e1.Current, e2.Current, e3.Current);
            }

            e1.Dispose();
            e2.Dispose();
            e3.Dispose();
        }

        public virtual IEnumerable<Tuple<T, Y, X, Z>> Join4<T, Y, X, Z>(
            IEnumerable<T> source1,
            IEnumerable<Y> source2,
            IEnumerable<X> source3,
            IEnumerable<Z> source4)
        {
            var e1 = source1.GetEnumerator();
            var e2 = source2.GetEnumerator();
            var e3 = source3.GetEnumerator();
            var e4 = source4.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
            {
                yield return Tuple.Create(
                    e1.Current, 
                    e2.Current, 
                    e3.Current,
                    e4.Current);
            }

            e1.Dispose();
            e2.Dispose();
            e3.Dispose();
            e4.Dispose();
        }
    }
}
