namespace xofz
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public static class EnumerableHelpers
    {
        public static IEnumerable<T> Empty<T>()
        {
            yield break;
        }

        public static IEnumerable<TResult> Select<T, TResult>(
            IEnumerable<T> source,
            Gen<T, TResult> selector)
        {
            if (source == null)
            {
                yield break;
            }

            if (selector == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TResult> SelectMany<T, TResult>(
            IEnumerable<T> source,
            Gen<T, IEnumerable<TResult>> selector)
        {
            if (source == null)
            {
                yield break;
            }

            if (selector == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                var subItems = selector(item);
                if (subItems == null)
                {
                    continue;
                }

                foreach (var selectee in subItems)
                {
                    yield return selectee;
                }
            }
        }

        public static IEnumerable<T> Where<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                yield break;
            }

            if (predicate == null)
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

        public static IEnumerable<T> Where<T>(
            IEnumerable<T> source,
            Gen<T, int, bool> predicateWithIndex)
        {
            if (source == null)
            {
                yield break;
            }

            if (predicateWithIndex == null)
            {
                yield break;
            }

            var index = 0;
            foreach (var item in source)
            {
                if (predicateWithIndex(item, index))
                {
                    yield return item;
                }

                ++index;
            }
        }

        public static IEnumerable<T> LongWhere<T>(
            IEnumerable<T> source,
            Gen<T, long, bool> predicateWithIndex)
        {
            if (source == null)
            {
                yield break;
            }

            if (predicateWithIndex == null)
            {
                yield break;
            }

            long index = 0;
            foreach (var item in source)
            {
                if (predicateWithIndex(item, index))
                {
                    yield return item;
                }

                ++index;
            }
        }

        public static IEnumerable<T> Skip<T>(
            IEnumerable<T> source,
            int skipCount)
        {
            if (source == null)
            {
                yield break;
            }

            var currentIndex = 0;
            foreach (var item in source)
            {
                ++currentIndex;
                if (currentIndex > skipCount)
                {
                    yield return item;
                }
            }
        }

        public static T First<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new InvalidOperationException(
                    "The enumerable is null and therefore "
                    + "does not have a first item.  If this can happen, "
                    + "consider using FirstOrDefault<T>()");
            }

            foreach (var item in source)
            {
                return item;
            }

            throw new InvalidOperationException(
                "The enumerable is empty and therefore "
                + "does not have a first item.  If this can happen, "
                + "consider using FirstOrDefault<T>()");
        }

        public static T First<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                throw new InvalidOperationException(
                    "The enumerable is null and therefore "
                    + "does not have a first item.  If this can happen, "
                    + "consider using FirstOrDefault<T>()");
            }

            var empty = true;
            foreach (var item in source)
            {
                empty = false;
                if (predicate(item))
                {
                    return item;
                }
            }

            if (empty)
            {
                throw new InvalidOperationException(
                    "The enumerable is empty and therefore "
                    + "does not have a first item.  If this can happen, "
                    + "consider using FirstOrDefault<T>()");
            }

            throw new InvalidOperationException(
                "The non-empty enumerable did not have any elements "
                + "which matched the predicate.");
        }

        public static T FirstOrDefault<T>(
            IEnumerable<T> source)
        {
            if (source == null)
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
            Gen<T, bool> predicate)
        {
            if (source == null)
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

        public static T Last<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new InvalidOperationException(
                    "The enumerable is null and therefore does not " +
                    "have a last item.  If this can occur, consider using " +
                    "LastOrDefault<T>()");
            }

            T lastItem = default(T);
            bool lastChanged = false;
            foreach (var item in source)
            {
                lastChanged = true;
                lastItem = item;
            }

            if (!lastChanged)
            {
                throw new InvalidOperationException(
                    "The enumerable is empty and therefore does not " +
                    "have a last item.  If this can occur, consider using " +
                    "LastOrDefault<T>()");
            }

            return lastItem;
        }

        public static T Last<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                throw new InvalidOperationException(
                    "The enumerable is null and therefore does not " +
                    "have a last item.  If this can occur, consider using " +
                    "LastOrDefault<T>()");
            }

            T lastItem = default(T);
            bool empty = true;
            bool lastChanged = false;
            foreach (var item in source)
            {
                empty = false;
                if (predicate(item))
                {
                    lastChanged = true;
                    lastItem = item;
                }                
            }

            if (!lastChanged && !empty)
            {
                throw new InvalidOperationException(
                    "No elements in the source matched the predicate.");
            }

            if (empty)
            {
                throw new InvalidOperationException(
                    "The enumerable is empty and does not contain a last item. " +
                    "To avoid the exception, consider using LastOrDefault<T>()");
            }

            return lastItem;
        }

        public static T LastOrDefault<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                return default(T);
            }

            T lastItem = default(T);
            foreach (var item in source)
            {
                lastItem = item;
            }

            return lastItem;
        }

        public static T LastOrDefault<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                return default(T);
            }

            T lastItem = default(T);
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    lastItem = item;
                }                
            }

            return lastItem;
        }

        public static bool Any<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                return false;
            }

            foreach (var item in source)
            {
                return true;
            }

            return false;
        }

        public static bool Any<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                return false;
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool All<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                return true;
            }

            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Contains<T>(
            IEnumerable<T> source,
            T item)
        {
            if (source == null)
            {
                return false;
            }

            var itemIsNull = item == null;
            foreach (var itemInSource in source)
            {
                if (itemInSource == null && itemIsNull)
                {
                    return true;
                }

                if (item?.Equals(itemInSource) ?? false)
                {
                    return true;
                }
            }

            return false;
        }

        public static IEnumerable<T> Cast<T>(
            IEnumerable source)
        {
            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return (T)item;
            }
        }

        public static int Count<T>(
            IEnumerable<T> source)
        {
            var count = 0;
            if (source == null)
            {
                return count;
            }
            
            foreach (var item in source)
            {
                ++count;
            }

            return count;
        }

        public static int Count<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            var count = 0;
            if (source == null)
            {
                return count;
            }
            
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    ++count;
                }
            }

            return count;
        }

        public static long LongCount<T>(
            IEnumerable<T> source)
        {
            long count = 0;
            if (source == null)
            {
                return count;
            }
            
            foreach (var item in source)
            {
                ++count;
            }

            return count;
        }

        public static long LongCount<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            long count = 0;
            if (source == null)
            {
                return count;
            }
            
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    ++count;
                }
            }

            return count;
        }

        public static T[] ToArray<T>(IEnumerable<T> source)
        {
            if (source == null)
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

        public static List<T> ToList<T>(
            IEnumerable<T> source)
        {
            return new List<T>(source);
        }

        public static ICollection<T> OrderBy<T, TKey>(
            IEnumerable<T> source,
            Gen<T, TKey> keySelector)
        {
            return orderBy(
                source,
                keySelector,
                Comparer<TKey>.Default,
                false);
        }

        public static ICollection<T> OrderBy<T, TKey>(
            IEnumerable<T> source,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return orderBy(
                source,
                keySelector,
                comparer,
                false);
        }

        public static ICollection<T> OrderByDescending<T, TKey>(
            IEnumerable<T> source,
            Gen<T, TKey> keySelector)
        {
            return orderBy(
                source,
                keySelector,
                Comparer<TKey>.Default,
                true);
        }

        public static ICollection<T> OrderByDescending<T, TKey>(
            IEnumerable<T> source,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return orderBy(
                source,
                keySelector,
                comparer,
                true);
        }

        private static ICollection<T> orderBy<T, TKey>(
            IEnumerable<T> source,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            if (source == null)
            {
                return new T[0];
            }

            if (keySelector == null)
            {
                return new T[0];
            }

            var d = new Dictionary<TKey, ICollection<T>>();
            ICollection<T> itemsWithNullKeys = new LinkedList<T>();

            foreach (var item in source)
            {
                var key = keySelector(item);
                if (key == null)
                {
                    itemsWithNullKeys.Add(item);
                    continue;
                }

                if (!d.ContainsKey(key))
                {
                    d.Add(key, new LinkedList<T>());
                }

                d[key].Add(item);
            }

            var keyList = new List<TKey>(d.Keys);
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

            return finalList;
        }

        public static TEnd Aggregate<T, TEnd>(
            IEnumerable<T> source,
            TEnd seed,
            Gen<TEnd, T, TEnd> accumulator)
        {
            if (source == null)
            {
                return seed;
            }

            if (accumulator == null)
            {
                return seed;
            }

            var end = seed;
            foreach (var item in source)
            {
                end = accumulator(end, item);
            }

            return end;
        }

        public static IEnumerable<T> OfType<T>(IEnumerable source)
        {
            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                if (item is T t)
                {
                    yield return t;
                }
            }
        }

        public static IEnumerable<T> SafeForEach<T>(IEnumerable<T> source)
        {
            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Iterate<T>(params T[] items)
        {
            if (items == null)
            {
                yield break;
            }

            foreach (var item in items)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> PrivateFieldsOfType<T>(
            object o)
        {
            if (o == null)
            {
                yield break;
            }

            foreach (var fieldInfo in o.GetType().GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic))
            {
                var value = fieldInfo.GetValue(o);
                if (value is T t)
                {
                    yield return t;
                }
            }
        }

        public static int Sum<T>(
            IEnumerable<T> source,
            Gen<T, int> valueComputer)
        {
            if (source == null)
            {
                return 0;
            }

            var sum = 0;
            foreach (var item in source)
            {
                checked
                {
                    sum += valueComputer(item);
                }
            }

            return sum;
        }

        public static long Sum<T>(
            IEnumerable<T> source,
            Gen<T, long> valueComputer)
        {
            if (source == null)
            {
                return 0;
            }

            long sum = 0;
            foreach (var item in source)
            {
                checked
                {
                    sum += valueComputer(item);
                }
            }

            return sum;
        }

        public static int Min(
            IEnumerable<int> source)
        {
            if (source == null)
            {
                return 0;
            }

            var min = int.MaxValue;
            var minChanged = false;
            foreach (var item in source)
            {
                minChanged = true;
                if (item < min)
                {
                    min = item;
                }
            }

            if (!minChanged)
            {
                return 0;
            }

            return min;
        }

        public static long Min(
            IEnumerable<long> source)
        {
            if (source == null)
            {
                return 0;
            }

            var min = long.MaxValue;
            var minChanged = false;
            foreach (var item in source)
            {
                minChanged = true;
                if (item < min)
                {
                    min = item;
                }
            }

            if (!minChanged)
            {
                return 0;
            }

            return min;
        }

        public static int Max(
            IEnumerable<int> source)
        {
            if (source == null)
            {
                return 0;
            }

            var max = 0;
            foreach (var item in source)
            {
                if (item > max)
                {
                    max = item;
                }
            }

            return max;
        }

        public static long Max(
            IEnumerable<long> source)
        {
            if (source == null)
            {
                return 0;
            }

            long max = 0;
            foreach (var item in source)
            {
                if (item > max)
                {
                    max = item;
                }
            }

            return max;
        }

        public static ICollection<T> Reverse<T>(
            IEnumerable<T> source)
        {
            var ll = new LinkedList<T>();
            if (source == null)
            {
                return ll;
            }

            foreach (var item in source)
            {
                ll.AddFirst(item);
            }

            return ll;
        }

        public static TKey KeyLookup<TValue, TKey>(
            IEnumerable<KeyValuePair<TKey, TValue>> dictionary,
            TValue value)
        {
            foreach (var kvp in dictionary)
            {
                if (kvp.Value?.Equals(value) ?? value == null)
                {
                    return kvp.Key;
                }
            }

            return default(TKey);
        }
    }
}
