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
            long indexCounter = zero;
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
            long indexCounter = zero;
            foreach (var lot in lots)
            {
                sources[indexCounter] = lot;
                ++indexCounter;
            }

            return this.Splice(sources);
        }

        public virtual Lot<T> Splice<T>(
            IEnumerable<T>[] finiteSources)
        {
            var result = new ListLot<T>();
            if (finiteSources == null)
            {
                return result;
            }

            var l = finiteSources.Length;
            if (l < one)
            {
                return result;
            }

            var lists = new List<T>[l];
            // first, enumerate all the items into separate lists
            for (long i = zero; i < l; ++i)
            {
                lists[i] = new List<T>(
                    finiteSources[i]);
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

            for (var i = zero; i < smallestCount; ++i)
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
                    zero, 
                    smallestCount);
                if (list.Count > zero)
                {
                    remainingLists.Add(list);
                }
            }

            if (remainingLists.Count < one)
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

        public virtual IEnumerable<T> SpliceV2<T>(
            IEnumerable<IEnumerable<T>> sources)
        {
            if (sources == null)
            {
                yield break;
            }

            ICollection<IEnumerator<T>> enumerators
                = new LinkedList<IEnumerator<T>>();
            foreach (var source in sources)
            {
                var enumerator = source?.GetEnumerator();
                if (enumerator == null)
                {
                    continue;
                }

                enumerators.Add(enumerator);
            }

            bool reachedOne;

            splice:
            reachedOne = false;
            foreach (var enumerator in enumerators)
            {
                if (!enumerator.MoveNext())
                {
                    continue;
                }

                reachedOne = true;
                yield return enumerator.Current;
            }

            if (reachedOne)
            {
                goto splice;
            }
        }

        protected const byte
            zero = 0,
            one = 1;
    }
}
