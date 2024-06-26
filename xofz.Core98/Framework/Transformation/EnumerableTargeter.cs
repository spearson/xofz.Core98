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
                return default;
            }

            if (target == null)
            {
                return default;
            }

            const bool falsity = false;
            foreach (var item in source)
            {
                if (item?.Equals(target) ?? falsity)
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
            var ll = new XLinkedListLot<T>();
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
            const byte zero = 0;
            while (e.MoveNext())
            {
                var o = e.Current;
                ll.AddTail(o);
                if (o?.Equals(target) ?? nullTarget)
                {
                    for (long i = zero; i < radius; ++i)
                    {
                        if (e.MoveNext())
                        {
                            ll.AddTail(e.Current);
                        }
                    }

                    e.Dispose();
                    return ll;
                }

                while (lot.Count > radius)
                {
                    ll.RemoveTail();
                }
            }

            e.Dispose();

            // if target not found, return the last radius number of items
            return lot;
        }
    }
}