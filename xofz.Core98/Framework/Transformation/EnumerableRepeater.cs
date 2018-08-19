namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableRepeater
    {
        public virtual ICollection<T> Repeat<T>(
            IEnumerable<T> source,
            int times)
        {
            ICollection<T> collection = new LinkedList<T>();
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
                collection.Add(item);
            }

            var result = new List<T>();
            for (var i = 0; i < times; ++i)
            {
                result.AddRange(collection);
            }

            return result;
        }
    }
}
