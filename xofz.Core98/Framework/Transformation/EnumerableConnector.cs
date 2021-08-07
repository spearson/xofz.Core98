namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableConnector
    {
        public virtual Lot<T> Connect<T>(
            params IEnumerable<T>[] finiteSources)
        {
            var connection = new LinkedListLot<T>();
            if (finiteSources == null)
            {
                return connection;
            }

            const byte zero = 0;
            var l = finiteSources.Length;
            for (int sourceIndex = zero;
                sourceIndex < l;
                ++sourceIndex)
            {
                var source = finiteSources[sourceIndex];
                if (source == null)
                {
                    continue;
                }

                using (var e = source.GetEnumerator())
                {
                    while (e?.MoveNext() ?? false)
                    {
                        connection.AddLast(e.Current);
                    }
                }
            }

            return connection;
        }
    }
}
