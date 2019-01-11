namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableConnector
    {
        public virtual Lot<T> Connect<T>(
            params IEnumerable<T>[] sources)
        {
            var connection = new LinkedListLot<T>();
            if (sources == null)
            {
                return connection;
            }
           
            for (var i = 0; i < sources.Length; ++i)
            {
                var source = sources[i];
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
