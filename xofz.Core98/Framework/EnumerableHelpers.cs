namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public static class EnumerableHelpers
    {
        public static IEnumerable<TResult> Select<T, TResult>(
            IEnumerable<T> source,
            Func<T, TResult> selector)
        {
            if (source == default(IEnumerable<T>))
            {
                yield break;
            }

            if (selector == default(Func<T, TResult>))
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<T> Where<T>(
            IEnumerable<T> source, 
            Func<T, bool> predicate)
        {
            if (source == default(IEnumerable<T>))
            {
                yield break;
            }

            if (predicate == default(Func<T, bool>))
            {
                yield break;
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Skip<T>(
            IEnumerable<T> source, 
            int numberToSkip)
        {
            if (source == default(IEnumerable<T>))
            {
                yield break;
            }

            var currentIndex = 0;
            foreach (var item in source)
            {
                ++currentIndex;
                if (currentIndex > numberToSkip)
                {
                    yield return item;
                }
            }
        }

        public static T FirstOrDefault<T>(
            IEnumerable<T> source)
        {
            if (source == default(IEnumerable<T>))
            {
                return default(T);
            }

            foreach (var item in source)
            {
                return item;
            }

            return default(T);
        }

        public static T FirstOrDefault<T>(
            IEnumerable<T> source,
            Func<T, bool> predicate)
        {
            if (source == default(IEnumerable<T>))
            {
                return default(T);
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        public static int Count<T>(
            IEnumerable<T> source,
            Func<T, bool> predicate)
        {
            if (source == default(IEnumerable<T>))
            {
                return default(int);
            }

            var totalCount = 0;
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    ++totalCount;
                }
            }

            return totalCount;
        }

        public static T[] ToArray<T>(IEnumerable<T> source)
        {
            if (source == default(IEnumerable<T>))
            {
                return new T[0];
            }

            var ll = new LinkedList<T>();
            foreach (var item in source)
            {
                ll.AddLast(item);
            }

            var array = new T[ll.Count];
            ll.CopyTo(array, 0);

            return array;
        }

        public static OrderedMaterializedEnumerable<T> ToOrdered<T>(
            IEnumerable<T> source)
        {
            var l = new List<T>();
            foreach (var item in source)
            {
                l.Add(item);
            }

            return new OrderedMaterializedEnumerable<T>(l);
        }

        public static OrderedMaterializedEnumerable<T> OrderBy<T, TKey>(
            IEnumerable<T> source,
            Func<T, TKey> keySelector)
        {
            return orderBy(
                source,
                keySelector,
                false);
        }

        public static OrderedMaterializedEnumerable<T> OrderByDescending<T, TKey>(
            IEnumerable<T> source,
            Func<T, TKey> keySelector)
        {
            return orderBy(
                source,
                keySelector,
                true);
        }

        private static OrderedMaterializedEnumerable<T> orderBy<T, TKey>(
            IEnumerable<T> source,
            Func<T, TKey> keySelector,
            bool descending)
        {
            if (source == default(IEnumerable<T>))
            {
                return default(OrderedMaterializedEnumerable<T>);
            }

            if (keySelector == default(Func<T, TKey>))
            {
                return new OrderedMaterializedEnumerable<T>(
                    source);
            }

            var d = new Dictionary<TKey, IList<T>>();
            var itemsWithNullKeys = new LinkedList<T>();

            foreach (var item in source)
            {
                var key = keySelector(item);
                if (key == null)
                {
                    itemsWithNullKeys.AddLast(item);
                    continue;
                }

                if (!d.ContainsKey(key))
                {
                    d.Add(key, new List<T>());
                }

                d[key].Add(item);
            }

            var keyList = new List<TKey>(d.Keys);
            var comparer = Comparer<TKey>.Default;
            keyList.Sort(comparer);

            if (descending)
            {
                keyList.Reverse();
            }

            var finalList = new List<T>();
            foreach (var key in keyList)
            {
                finalList.AddRange(d[key]);
            }
            finalList.AddRange(itemsWithNullKeys);

            return new OrderedMaterializedEnumerable<T>(finalList);
        }

        public static TEnd Aggregate<T, TEnd>(
            IEnumerable<T> source,
            TEnd seed,
            Func<TEnd, T, TEnd> accumulator)
        {
            if (source == default(IEnumerable<T>))
            {
                return default(TEnd);
            }

            if (accumulator == default(Func<TEnd, T, TEnd>))
            {
                return default(TEnd);
            }

            var end = seed;
            foreach (var item in source)
            {
                end = accumulator(end, item);
            }

            return end;
        }
    }
}
