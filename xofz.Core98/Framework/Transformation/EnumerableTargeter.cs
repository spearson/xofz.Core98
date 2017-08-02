namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public class EnumerableTargeter
    {
        public virtual T Target<T>(IEnumerable<T> source, T target)
        {
            foreach (var item in source)
            {
                if (item.Equals(target))
                {
                    return item;
                }
            }

            return default(T);
        }

        public virtual MaterializedEnumerable<T> Target<T>(
            IEnumerable<T> source, 
            T target, 
            int radius)
        {
            var ll = new LinkedList<T>();
            var e = source.GetEnumerator();
            while (e.MoveNext())
            {
                ll.AddLast(e.Current);
                if (e.Current.Equals(target))
                {
                    for (var i = 0; i < radius; ++i)
                    {
                        if (e.MoveNext())
                        {
                            ll.AddLast(e.Current);
                        }
                    }

                    return new LinkedListMaterializedEnumerable<T>(ll);
                }

                while (ll.Count > radius)
                {
                    ll.RemoveFirst();
                }
            }

            e.Dispose();

            // if target not found, return the last radius number of items
            return new LinkedListMaterializedEnumerable<T>(ll);
        }
    }
}
