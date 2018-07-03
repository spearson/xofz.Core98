﻿namespace xofz
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

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

        public static T First<T>(
            IEnumerable<T> source)
        {
            if (source == default(IEnumerable<T>))
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
            Func<T, bool> predicate)
        {
            if (source == default(IEnumerable<T>))
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
            Func<T, bool> predicate)
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
            Func<T, bool> predicate)
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

        public static List<T> ToList<T>(
            IEnumerable<T> source)
        {
            var l = new List<T>();
            l.AddRange(source);

            return l;
        }

        public static List<T> OrderBy<T, TKey>(
            IEnumerable<T> source,
            Func<T, TKey> keySelector)
        {
            return orderBy(
                source,
                keySelector,
                false);
        }

        public static List<T> OrderByDescending<T, TKey>(
            IEnumerable<T> source,
            Func<T, TKey> keySelector)
        {
            return orderBy(
                source,
                keySelector,
                true);
        }

        private static List<T> orderBy<T, TKey>(
            IEnumerable<T> source,
            Func<T, TKey> keySelector,
            bool descending)
        {
            if (source == default(IEnumerable<T>))
            {
                return new List<T>();
            }

            if (keySelector == default(Func<T, TKey>))
            {
                return new List<T>();
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

            return finalList;
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
            if (source == default(IEnumerable<T>))
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
            Func<T, int> valueComputer)
        {
            if (source == null)
            {
                return 0;
            }

            var sum = 0;
            foreach (var item in source)
            {
                sum += valueComputer(item);
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
    }
}