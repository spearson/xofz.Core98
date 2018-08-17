namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableConnector
    {
        public virtual ICollection<T> Connect<T>(
            params IEnumerable<T>[] sources)
        {
            ICollection<T> connection = new LinkedList<T>();

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
                        connection.Add(e.Current);
                    }
                }
            }

            return connection;
        }
    }
}
