namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

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

        public virtual ICollection<T> Target<T>(
            IEnumerable<T> source, 
            T target, 
            int radius)
        {
            var ll = new LinkedList<T>();
            if (source == null)
            {
                return ll;
            }

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

                while (ll.Count > radius)
                {
                    ll.RemoveFirst();
                }
            }

            e?.Dispose();

            // if target not found, return the last radius number of items
            return ll;
        }
    }
}
