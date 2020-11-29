namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = xofz.EnumerableHelpers;

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
            long indexCounter = 0;
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
            long indexCounter = 0;
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

            var l = sources.Length;
            if (l < 1)
            {
                return result;
            }

            var lists = new List<T>[l];
            // first, enumerate all the items into separate lists
            for (long i = 0; i < l; ++i)
            {
                lists[i] = new List<T>(
                    sources[i]);
            }

            // then, splice the lists together
            result = new ListLot<T>(
                EH.Sum(
                    lists,
                    list => list.Count));
            var smallestCount = EH.Min(
                EH.Select(
                lists,
                list => list.Count));

            for (var i = 0; i < smallestCount; ++i)
            {
                var currentIndex = i;
                result.AddRange(
                    EH.Select(
                        lists,
                        list => list[currentIndex]));
            }

            ICollection<ICollection<T>> remainingLists = 
                new LinkedList<ICollection<T>>();
            foreach (var list in lists)
            {
                list.RemoveRange(
                    0, 
                    smallestCount);
                if (list.Count > 0)
                {
                    remainingLists.Add(list);
                }
            }

            if (remainingLists.Count < 1)
            {
                return result;
            }

            result.AddRange(
                this.Splice(
                    // ReSharper disable once CoVariantArrayConversion
                    EH.ToArray(
                        remainingLists)));

            return result;
        }
    }
}
