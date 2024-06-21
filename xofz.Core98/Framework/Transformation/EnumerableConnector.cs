namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableConnector
    {
        public virtual Lot<T> Connect<T>(
            params IEnumerable<T>[] finiteSources)
        {
            var connection = new XLinkedListLot<T>();
            foreach (var item in this.Connect(
                         (IEnumerable<IEnumerable<T>>)finiteSources))
            {
                connection.AddTail(item);
            }

            return connection;
        }

        public virtual IEnumerable<T> Connect<T>(
            IEnumerable<IEnumerable<T>> sources)
        {
            if (sources == null)
            {
                yield break;
            }

            foreach (var source in sources)
            {
                if (source == null)
                {
                    continue;
                }

                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }
    }
}