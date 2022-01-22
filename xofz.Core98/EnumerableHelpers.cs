namespace xofz
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using EH = xofz.EnumerableHelpers;

    public class EnumerableHelpers
    {
        public static IEnumerable<T> Empty<T>()
        {
            yield break;
        }

        public static IEnumerable<T> Null<T>()
        {
            return null;
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

            var index = zero;
            foreach (var item in source)
            {
                if (predicateWithIndex(item, index))
                {
                    yield return item;
                }

                checked
                {
                    ++index;
                }
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

            long index = zero;
            foreach (var item in source)
            {
                if (predicateWithIndex(item, index))
                {
                    yield return item;
                }

                checked
                {
                    ++index;
                }
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

            uint skipIndex = one;
            foreach (var item in source)
            {               
                if (skipIndex > skipCount)
                {
                    yield return item;
                }

                ++skipIndex;
            }
        }

        public static IEnumerable<T> Take<T>(
            IEnumerable<T> source,
            int takeCount)
        {
            if (source == null)
            {
                yield break;
            }

            if (takeCount < one)
            {
                yield break;
            }

            uint takeIndex = one;
            foreach (var item in source)
            {                
                if (takeIndex > takeCount)
                {
                    yield break;
                }

                yield return item;

                ++takeIndex;
            }
        }

        public static IEnumerable<T> TakeWhile<T>(
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
                if (!predicate(item))
                {
                    yield break;
                }

                yield return item;
            }
        }

        public static IEnumerable<T> TakeWhile<T>(
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

            long index = zero;
            foreach (var item in source)
            {
                if (!predicateWithIndex(item, index))
                {
                    yield break;
                }

                yield return item;

                checked
                {
                    ++index;
                }
            }
        }

        public static IEnumerable<T> TakeLast<T>(
            IEnumerable<T> finiteSource,
            int takeCount)
        {
            if (finiteSource == null)
            {
                yield break;
            }

            if (takeCount < one)
            {
                yield break;
            }

            foreach (var item in
                EH.Reverse(
                    EH.Take(
                        EH.Reverse(
                            finiteSource),
                        takeCount)))
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Concat<T>(
            IEnumerable<T> firstSource,
            IEnumerable<T> secondSource)
        {
            if (firstSource == null)
            {
                goto checkSecond;
            }

            foreach (var item in firstSource)
            {
                yield return item;
            }

            checkSecond:
            if (secondSource == null)
            {
                yield break;
            }

            foreach (var item in secondSource)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Append<T>(
            IEnumerable<T> source,
            T appendee)
        {
            if (source == null)
            {
                yield return appendee;
                yield break;
            }

            foreach (var item in source)
            {
                yield return item;
            }

            yield return appendee;
        }

        public static IEnumerable<T> Prepend<T>(
            IEnumerable<T> source,
            T prependee)
        {
            yield return prependee;

            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Insert<T>(
            IEnumerable<T> source,
            T itemToInsert,
            long insertionIndex)
        {
            if (source == null)
            {
                yield return itemToInsert;
                yield break;
            }

            if (insertionIndex < one)
            {
                yield return itemToInsert;
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long indexer = one;
            foreach (var item in source)
            {
                yield return item;

                if (indexer == insertionIndex)
                {
                    yield return itemToInsert;
                }

                checked
                {
                    ++indexer;
                }
            }

            if (indexer == insertionIndex)
            {
                yield return itemToInsert;
            }
        }

        public static T First<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new InvalidOperationException(
                    @"The enumerable is null and therefore "
                    + @"does not have a first item.  If this can happen, "
                    + @"consider using FirstOrDefault<T>()");
            }

            foreach (var item in source)
            {
                return item;
            }

            throw new InvalidOperationException(
                @"The enumerable is empty and therefore "
                + @"does not have a first item.  If this can happen, "
                + @"consider using FirstOrDefault<T>()");
        }

        public static T First<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                throw new InvalidOperationException(
                    @"The enumerable is null and therefore "
                    + @"does not have a first item.  If this can happen, "
                    + @"consider using FirstOrDefault<T>()");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var empty = truth;
            foreach (var item in source)
            {
                empty = falsity;
                if (predicate(item))
                {
                    return item;
                }
            }

            if (empty)
            {
                throw new InvalidOperationException(
                    @"The enumerable is empty and therefore "
                    + @"does not have a first item.  If this can happen, "
                    + @"consider using FirstOrDefault<T>()");
            }

            throw new InvalidOperationException(
                @"The non-empty enumerable did not have any elements "
                + @"which matched the predicate.");
        }

        public static T FirstOrDefault<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                return default;
            }

            foreach (var item in source)
            {
                return item;
            }

            return default;
        }

        public static T FirstOrDefault<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                return default;
            }

            if (predicate == null)
            {
                return default;
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default;
        }

        public static T FirstOrNull<T>(
            IEnumerable<T> finiteSource)
            where T : class
        {
            if (finiteSource == null)
            {
                return null;
            }

            foreach (var item in finiteSource)
            {
                return item ?? null;
            }

            return null;
        }

        public static T FirstOrNull<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
            where T : class
        {
            T nullItem = null;
            if (finiteSource == nullItem)
            {
                return nullItem;
            }

            if (predicate == null)
            {
                return nullItem;
            }

            foreach (var item in finiteSource)
            {
                if (!predicate?.Invoke(item) ?? truth)
                {
                    continue;
                }

                return item;
            }

            return nullItem;
        }

        public static T Last<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                throw new InvalidOperationException(
                    @"The enumerable is null and therefore does not " +
                    @"have a last item.  If this can occur, consider using " +
                    @"LastOrDefault<T>()");
            }

            T lastItem = default;
            bool lastChanged = falsity;
            foreach (var item in finiteSource)
            {
                lastChanged = truth;
                lastItem = item;
            }

            if (!lastChanged)
            {
                throw new InvalidOperationException(
                    @"The enumerable is empty and therefore does not " +
                    @"have a last item.  If this can occur, consider using " +
                    @"LastOrDefault<T>()");
            }

            return lastItem;
        }

        public static T Last<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            if (finiteSource == null)
            {
                throw new InvalidOperationException(
                    @"The enumerable is null and therefore does not " +
                    @"have a last item.  If this can occur, consider using " +
                    @"LastOrDefault<T>()");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            T lastItem = default;
            bool empty = truth;
            bool lastChanged = falsity;
            foreach (var item in finiteSource)
            {
                empty = falsity;
                if (predicate(item))
                {
                    lastChanged = truth;
                    lastItem = item;
                }                
            }

            if (!lastChanged && !empty)
            {
                throw new InvalidOperationException(
                    @"No elements in the source matched the predicate.");
            }

            if (empty)
            {
                throw new InvalidOperationException(
                    @"The enumerable is empty and does not contain a last item. " +
                    @"To avoid the exception, consider using LastOrDefault<T>()");
            }

            return lastItem;
        }

        public static T LastOrDefault<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return default;
            }

            T lastItem = default;
            foreach (var item in finiteSource)
            {
                lastItem = item;
            }

            return lastItem;
        }

        public static T LastOrDefault<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            if (finiteSource == null)
            {
                return default;
            }

            if (predicate == null)
            {
                return default;
            }

            T lastItem = default;
            foreach (var item in finiteSource)
            {
                if (predicate(item))
                {
                    lastItem = item;
                }                
            }

            return lastItem;
        }

        public static T LastOrNull<T>(
            IEnumerable<T> finiteSource)
            where T : class
        {
            if (finiteSource == null)
            {
                return null;
            }

            T lastItem = null;
            foreach (var item in finiteSource)
            {
                lastItem = item;
            }

            return lastItem;
        }

        public static T LastOrNull<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
            where T : class
        {
            if (finiteSource == null)
            {
                return null;
            }

            if (predicate == null)
            {
                return null;
            }

            T lastItem = null;
            foreach (var item in finiteSource)
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
                return falsity;
            }

            foreach (var item in source)
            {
                return truth;
            }

            return falsity;
        }

        public static bool Any<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                return falsity;
            }

            if (predicate == null)
            {
                return falsity;
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return truth;
                }
            }

            return falsity;
        }

        public static bool All<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                return truth;
            }

            if (predicate == null)
            {
                return truth;
            }

            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    return falsity;
                }
            }

            return truth;
        }

        public static bool Contains<T>(
            IEnumerable<T> source,
            T item)
        {
            if (source == null)
            {
                return falsity;
            }

            var itemIsNull = item == null;
            foreach (var itemInSource in source)
            {
                if (itemInSource?.Equals(item) ?? itemIsNull)
                {
                    return truth;
                }
            }

            return falsity;
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

        public static IEnumerable<T> SafeCast<T>(
            IEnumerable source)
        {
            if (source == null)
            {
                yield break;
            }

            T t;
            foreach (var item in source)
            {
                try
                {
                    t = (T)item;
                }
                catch
                {
                    continue;
                }

                yield return t;
            }
        }

        public static int Count<T>(
            IEnumerable<T> finiteSource)
        {
            int count = zero;
            if (finiteSource == null)
            {
                return count;
            }
            
            foreach (var item in finiteSource)
            {
                checked
                {
                    ++count;
                }                
            }

            return count;
        }

        public static int Count<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            int count = zero;
            if (finiteSource == null)
            {
                return count;
            }

            if (predicate == null)
            {
                return count;
            }
            
            foreach (var item in finiteSource)
            {
                if (predicate(item))
                {
                    checked
                    {
                        ++count;
                    }                    
                }
            }

            return count;
        }

        public static long LongCount<T>(
            IEnumerable<T> finiteSource)
        {
            long count = zero;
            if (finiteSource == null)
            {
                return count;
            }
            
            foreach (var item in finiteSource)
            {
                checked
                {
                    ++count;
                }                
            }

            return count;
        }

        public static long LongCount<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            long count = zero;
            if (finiteSource == null)
            {
                return count;
            }

            if (predicate == null)
            {
                return count;
            }
            
            foreach (var item in finiteSource)
            {
                if (predicate(item))
                {
                    checked
                    {
                        ++count;
                    }                    
                }
            }

            return count;
        }

        public static T[] ToArray<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new T[zero];
            }

            if (finiteSource is T[] a)
            {
                return a;
            }

            ICollection<T> c;
            if (finiteSource is ICollection<T> collection)
            {
                c = collection;
                goto createArray;
            }

            c = XLinkedList<T>.Create(finiteSource);

            createArray:
            var array = new T[c.Count];
            int indexer = zero;
            foreach (var item in c)
            {
                array[indexer] = item;
                ++indexer;
            }

            return array;
        }

        public static T[] ToArray<T>(
            Lot<T> lot)
        {
            if (lot == null)
            {
                return new T[zero];
            }

            var array = new T[lot.Count];
            long indexer = zero;
            foreach (var item in lot)
            {
                array[indexer] = item;
                ++indexer;
            }

            return array;
        }

        public static List<T> ToList<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new List<T>();
            }

            if (finiteSource is List<T> l)
            {
                return l;
            }

            return new List<T>(finiteSource);
        }

        public static ICollection<T> OrderBy<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector)
        {
            return orderBy(
                finiteSource,
                keySelector,
                Comparer<TKey>.Default,
                falsity);
        }

        public static ICollection<T> OrderBy<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return orderBy(
                finiteSource,
                keySelector,
                comparer,
                falsity);
        }

        public static ICollection<T> OrderByDescending<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector)
        {
            return orderBy(
                finiteSource,
                keySelector,
                Comparer<TKey>.Default,
                truth);
        }

        public static ICollection<T> OrderByDescending<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return orderBy(
                finiteSource,
                keySelector,
                comparer,
                truth);
        }

        private static ICollection<T> orderBy<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            if (finiteSource == null || keySelector == null || comparer == null)
            {
                return new XLinkedList<T>();
            }

            var d = new Dictionary<TKey, ICollection<T>>();
            ICollection<T> itemsWithNullKeys = new XLinkedList<T>();

            foreach (var item in finiteSource)
            {
                var key = keySelector(item);
                if (key == null)
                {
                    itemsWithNullKeys.Add(item);
                    continue;
                }

                if (!d.ContainsKey(key))
                {
                    d.Add(key, new XLinkedList<T>());
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
            IEnumerable<T> finiteSource,
            TEnd seed,
            Gen<TEnd, T, TEnd> accumulator)
        {
            if (finiteSource == null)
            {
                return seed;
            }

            if (accumulator == null)
            {
                return seed;
            }

            var end = seed;
            foreach (var item in finiteSource)
            {
                end = accumulator(end, item);
            }

            return end;
        }

        public static IEnumerable<T> OfType<T>(
            IEnumerable source)
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

        public static IEnumerable<T> SafeForEach<T>(
            IEnumerable<T> source)
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

        public static IEnumerable<T> Iterate<T>(
            params T[] items)
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

            foreach (var fieldInfo in o
                .GetType()
                .GetFields(
                    BindingFlags.Instance |
                    BindingFlags.NonPublic))
            {
                var value = fieldInfo.GetValue(o);
                if (value is T t)
                {
                    yield return t;
                }
            }
        }

        public static int Sum<T>(
            IEnumerable<T> finiteSource,
            Gen<T, int> valueComputer)
        {
            if (finiteSource == null)
            {
                return zero;
            }

            if (valueComputer == null)
            {
                return zero;
            }

            int sum = zero;
            foreach (var item in finiteSource)
            {
                checked
                {
                    sum += valueComputer(item);
                }
            }

            return sum;
        }

        public static long Sum<T>(
            IEnumerable<T> finiteSource,
            Gen<T, long> valueComputer)
        {
            if (finiteSource == null)
            {
                return zero;
            }

            if (valueComputer == null)
            {
                return zero;
            }

            long sum = zero;
            foreach (var item in finiteSource)
            {
                checked
                {
                    sum += valueComputer(item);
                }
            }

            return sum;
        }

        public static int Min(
            IEnumerable<int> finiteSource)
        {
            if (finiteSource == null)
            {
                return zero;
            }

            var min = int.MaxValue;
            var minChanged = falsity;
            foreach (var item in finiteSource)
            {
                minChanged = truth;
                if (item < min)
                {
                    min = item;
                }
            }

            if (!minChanged)
            {
                return zero;
            }

            return min;
        }

        public static long Min(
            IEnumerable<long> finiteSource)
        {
            if (finiteSource == null)
            {
                return zero;
            }

            var min = long.MaxValue;
            var minChanged = falsity;
            foreach (var item in finiteSource)
            {
                minChanged = truth;
                if (item < min)
                {
                    min = item;
                }
            }

            if (!minChanged)
            {
                return zero;
            }

            return min;
        }

        public static int Max(
            IEnumerable<int> finiteSource)
        {
            if (finiteSource == null)
            {
                return zero;
            }

            int max = zero;
            foreach (var item in finiteSource)
            {
                if (item > max)
                {
                    max = item;
                }
            }

            return max;
        }

        public static long Max(
            IEnumerable<long> finiteSource)
        {
            if (finiteSource == null)
            {
                return zero;
            }

            long max = zero;
            foreach (var item in finiteSource)
            {
                if (item > max)
                {
                    max = item;
                }
            }

            return max;
        }

        public static ICollection<T> Reverse<T>(
            IEnumerable<T> finiteSource)
        {
            var ll = new XLinkedList<T>();
            if (finiteSource == null)
            {
                return ll;
            }

            foreach (var item in finiteSource)
            {
                ll.AddHead(item);
            }

            return ll;
        }

        public static TKey KeyLookup<TValue, TKey>(
            IEnumerable<KeyValuePair<TKey, TValue>> dictionary,
            TValue value)
        {
            if (dictionary == null)
            {
                return default;
            }

            var valueIsNull = value == null;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value?.Equals(value) ?? valueIsNull)
                {
                    return kvp.Key;
                }
            }

            return default;
        }

        public static IEnumerable<TKey> KeysLookup<TValue, TKey>(
            IEnumerable<KeyValuePair<TKey, TValue>> dictionary,
            TValue value)
        {
            if (dictionary == null)
            {
                yield break;
            }

            var valueIsNull = value == null;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value?.Equals(value) ?? valueIsNull)
                {
                    yield return kvp.Key;
                }
            }
        }

        public static T Single<T>(
            IEnumerable<T> singleSource)
        {
            if (singleSource == null)
            {
                throw new ArgumentNullException(
                    nameof(singleSource));
            }

            T currentItem = default;
            bool currentItemSet = falsity, isEmpty = truth;
            foreach (var item in singleSource)
            {
                if (currentItemSet)
                {
                    throw new InvalidOperationException(
                        @"The source contains more than 1 item.");
                }

                currentItem = item;
                currentItemSet = truth;
                isEmpty = falsity;
            }

            if (isEmpty)
            {
                throw new InvalidOperationException(
                    @"The source was empty.");
            }

            return currentItem;
        }

        public static T Single<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            T matchingItem = default;
            bool matchingItemSet = falsity, noMatches = truth;
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    continue;
                }

                if (matchingItemSet)
                {
                    throw new InvalidOperationException(
                        @"The source contains more than 1 match.");
                }

                matchingItem = item;
                matchingItemSet = truth;
                noMatches = falsity;
            }

            if (noMatches)
            {
                throw new InvalidOperationException(
                    @"The source did not contain any elements which matched the predicate.");
            }

            return matchingItem;
        }

        public static T SingleOrDefault<T>(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                return default;
            }

            T currentItem = default;
            bool currentItemSet = falsity;
            foreach (var item in source)
            {
                if (currentItemSet)
                {
                    throw new InvalidOperationException(
                        @"The source contains more than 1 item.");
                }

                currentItem = item;
                currentItemSet = truth;
            }

            return currentItem;
        }

        public static T SingleOrDefault<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            if (source == null)
            {
                return default;
            }

            if (predicate == null)
            {
                return default;
            }

            T matchingItem = default;
            bool matchingItemSet = falsity;
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    continue;
                }

                if (matchingItemSet)
                {
                    throw new InvalidOperationException(
                        @"The source contains more than 1 match.");
                }

                matchingItem = item;
                matchingItemSet = truth;
            }

            return matchingItem;
        }

        public static IEnumerable<T> Except<T>(
            IEnumerable<T> finiteSource,
            IEnumerable<T> finiteExceptions)
        {
            if (finiteSource == null)
            {
                yield break;
            }

            if (finiteExceptions == null)
            {
                foreach (var item in finiteSource)
                {
                    yield return item;
                }

                yield break;
            }

            var exceptions = XLinkedList<T>.Create(
                finiteExceptions);
            foreach (var item in finiteSource)
            {
                if (exceptions.Contains(item))
                {
                    continue;
                }

                yield return item;
            }
        }

        public static IEnumerable<T> Distinct<T>(
            IEnumerable<T> finiteSource)
        {
            var set = new xofz.Framework.Lots.SetLot<T>();
            foreach (var item in finiteSource)
            {
                if (set.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Union<T>(
            IEnumerable<T> finiteSource1,
            IEnumerable<T> finiteSource2)
        {
            var set = new xofz.Framework.Lots.SetLot<T>();
            if (finiteSource1 != null)
            {
                foreach (var item in finiteSource1)
                {
                    if (set.Add(item))
                    {
                        yield return item;
                    }
                }
            }

            if (finiteSource2 != null)
            {
                foreach (var item in finiteSource2)
                {
                    if (set.Add(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IEnumerable<T> Intersect<T>(
            IEnumerable<T> finiteSource1,
            IEnumerable<T> finiteSource2)
        {
            var set = new xofz.Framework.Lots.SetLot<T>();
            if (finiteSource1 == null || finiteSource2 == null)
            {
                yield break;
            }

            foreach (var item in finiteSource1)
            {
                set.Add(item);
            }

            foreach (var item in finiteSource2)
            {
                if (set.Remove(item))
                {
                    yield return item;
                }
            }
        }

        public static T ElementAt<T>(
            IEnumerable<T> source,
            long index)
        {
            if (source == null)
            {
                return default;
            }

            if (index < zero)
            {
                return default;
            }

            long indexer = zero;
            foreach (var item in source)
            {
                if (indexer == index)
                {
                    return item;
                }

                checked
                {
                    ++indexer;
                }
            }

            return default;
        }

        public static long IndexOf<T>(
            IEnumerable<T> source,
            T o)
        {
            if (source == null)
            {
                return minusOne;
            }

            long index = zero;
            foreach (var item in source)
            {
                if (o?.Equals(item) ?? item == null)
                {
                    return index;
                }

                checked
                {
                    ++index;
                }
            }

            return index;
        }

        protected const byte
            zero = 0,
            one = 1;
        protected const short
            minusOne = -1;
        protected const bool
            truth = true,
            falsity = false;
    }
}
