namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableSplicer
    {
        public virtual Lot<T> Splice<T>(
            ICollection<T>[] collections)
        {
            if (collections == null)
            {
                return Lot.Empty<T>();
            }

            var sources = new IEnumerable<T>[collections.Length];
            var indexCounter = 0;
            foreach (var collection in collections)
            {
                sources[indexCounter] = collection;
                ++indexCounter;
            }

            return this.Splice(sources);
        }

        public virtual Lot<T> Splice<T>(
            Lot<T>[] lots)
        {
            if (lots == null)
            {
                return Lot.Empty<T>();
            }

            var sources = new IEnumerable<T>[lots.Length];
            var indexCounter = 0;
            foreach (var lot in lots)
            {
                sources[indexCounter] = lot;
                ++indexCounter;
            }

            return this.Splice(sources);
        }

        public virtual Lot<T> Splice<T>(
            IEnumerable<T>[] sources)
        {
            var result = new ListLot<T>();
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
            result = new ListLot<T>(
                EnumerableHelpers.Sum(
                    lists,
                    l => l.Count));
            var smallestCount = EnumerableHelpers.Min(
                EnumerableHelpers.Select(
                lists,
                l => l.Count));

            for (var i = 0; i < smallestCount; ++i)
            {
                var currentIndex = i;
                result.AddRange(
                    EnumerableHelpers.Select(
                        lists,
                        l => l[currentIndex]));
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

            if (remainingLists.Count < 1)
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
