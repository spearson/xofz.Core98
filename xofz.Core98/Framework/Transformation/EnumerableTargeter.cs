namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableTargeter
    {
        public virtual T Target<T>(
            IEnumerable<T> source,
            T target)
        {
            if (source == null)
            {
                return default;
            }

            if (target == null)
            {
                return default;
            }

            foreach (var item in source)
            {
                if (item?.Equals(target) ?? false)
                {
                    return item;
                }
            }

            return default;
        }

        public virtual Lot<T> Target<T>(
            IEnumerable<T> source, 
            T target, 
            int radius)
        {
            var ll = new LinkedListLot<T>();
            if (source == null)
            {
                return ll;
            }

            Lot<T> lot = ll;
            var e = source.GetEnumerator();
            if (e == null)
            {
                return lot;
            }

            var nullTarget = target == null;
            while (e.MoveNext())
            {
                var item = e.Current;
                ll.AddLast(item);
                if (item?.Equals(target) ?? nullTarget)
                {
                    for (long i = 0; i < radius; ++i)
                    {
                        if (e.MoveNext())
                        {
                            ll.AddLast(e.Current);
                        }
                    }

                    e.Dispose();
                    return ll;
                }

                while (lot.Count > radius)
                {
                    ll.RemoveFirst();
                }
            }

            e.Dispose();

            // if target not found, return the last radius number of items
            return lot;
        }
    }
}
