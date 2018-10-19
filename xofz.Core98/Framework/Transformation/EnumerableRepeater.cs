namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableRepeater
    {
        public virtual Lot<T> Repeat<T>(
            IEnumerable<T> source,
            int times)
        {
            var collection = new LinkedListLot<T>();
            if (source == null)
            {
                return collection;
            }

            if (times < 1)
            {
                return collection;
            }

            foreach (var item in source)
            {
                collection.AddLast(item);
            }

            var result = new ListLot<T>();
            for (var i = 0; i < times; ++i)
            {
                result.AddRange(collection);
            }

            return result;
        }
    }
}
