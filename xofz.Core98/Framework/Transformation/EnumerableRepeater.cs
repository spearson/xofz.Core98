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
            const byte
                zero = 0,
                one = 1;
            var collection = new LinkedListLot<T>();
            if (source == null)
            {
                return collection;
            }

            if (times < one)
            {
                return collection;
            }

            foreach (var item in source)
            {
                collection.AddLast(item);
            }

            var result = new ListLot<T>();
            for (int i = zero; i < times; ++i)
            {
                result.AddRange(collection);
            }

            return result;
        }
    }
}
