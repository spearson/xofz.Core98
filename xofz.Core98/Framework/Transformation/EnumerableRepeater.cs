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
            var collection = new XLinkedListLot<T>();
            if (source == null)
            {
                return collection;
            }

            if (times < zero)
            {
                return collection;
            }

            foreach (var item in source)
            {
                collection.AddTail(item);
            }

            if (times < one)
            {
                return collection;
            }

            var result = new XLinkedListLot<T>();
            for (int i = zero; i < times; ++i)
            {
                foreach (var o in collection)
                {
                    result.AddTail(o);
                }
            }

            return result;
        }
    }
}
