﻿namespace xofz.Framework.Transformation
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
                return default(T);
            }

            var nullTarget = target == null;
            foreach (var item in source)
            {
                if (item == null && nullTarget)
                {
                    return item;
                }

                if (item?.Equals(target) ?? false)
                {
                    return item;
                }
            }

            return default(T);
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
            var nullTarget = target == null;
            var e = source.GetEnumerator();
            while (e?.MoveNext() ?? false)
            {
                var item = e.Current;
                ll.AddLast(item);
                if (item?.Equals(target) ?? nullTarget ? true : false)
                {
                    for (var i = 0; i < radius; ++i)
                    {
                        if (e.MoveNext())
                        {
                            ll.AddLast(e.Current);
                        }
                    }

                    return ll;
                }

                while (lot.Count > radius)
                {
                    ll.RemoveFirst();
                }
            }

            e?.Dispose();

            // if target not found, return the last radius number of items
            return lot;
        }
    }
}
