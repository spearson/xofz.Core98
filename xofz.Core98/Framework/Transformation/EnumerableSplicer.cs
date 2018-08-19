namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableSplicer
    {
        public virtual ICollection<T> Splice<T>(
            ICollection<T>[] collections)
        {
            var sources = new IEnumerable<T>[collections.Length];
            var indexCounter = 0;
            foreach (var collection in collections)
            {
                sources[indexCounter] = collection;
                ++indexCounter;
            }

            return this.Splice(sources);
        }

        public virtual ICollection<T> Splice<T>(
            IEnumerable<T>[] sources)
        {
            var result = new List<T>();
            if (sources == null)
            {
                return result;
            }

            if (sources.Length < 1)
            {
                return result;
            }

            var lists = new List<T>[sources.Length];
            // first, enumerate all the items into separate lists
            for (var i = 0; i < sources.Length; ++i)
            {
                lists[i] = new List<T>(sources[i]);
            }

            // then, splice the lists together
            result = new List<T>(
                EnumerableHelpers.Sum(
                    lists,
                    l => l.Count));
            var smallestCount = EnumerableHelpers.Min(
                EnumerableHelpers.Select(
                lists,
                l => l.Count));

            for (var i = 0; i < smallestCount; ++i)
            {
                result.AddRange(
                    EnumerableHelpers.Select(
                        lists,
                        l => l[i]));
            }

            var remainingLists = new LinkedList<List<T>>();
            foreach (var l in lists)
            {
                l.RemoveRange(0, smallestCount);
                if (l.Count > 0)
                {
                    remainingLists.AddLast(l);
                }
            }

            if (remainingLists.Count == 0)
            {
                return result;
            }

            result.AddRange(
                this.Splice(
                    // ReSharper disable once CoVariantArrayConversion
                    EnumerableHelpers.ToArray(
                        remainingLists)));

            return result;
        }
    }
}
