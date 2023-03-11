namespace xofz
{
    using System.Collections;
    using System.Collections.Generic;
    using EH = xofz.EnumerableHelpers;

    public class EnumerableHelper
    {
        public virtual IEnumerable<T> Empty<T>()
        {
            return EH.Empty<T>();
        }

        public virtual IEnumerable<T> Null<T>()
        {
            return EH.Null<T>();
        }

        public virtual IEnumerable<TResult> Select<T, TResult>(
            IEnumerable<T> source,
            Gen<T, TResult> selector)
        {
            return EH.Select(source, selector);
        }

        public virtual IEnumerable<TResult> SelectMany<T, TResult>(
            IEnumerable<T> source,
            Gen<T, IEnumerable<TResult>> selector)
        {
            return EH.SelectMany(source, selector);
        }

        public virtual IEnumerable<T> Where<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.Where(source, predicate);
        }

        public virtual IEnumerable<T> Where<T>(
            IEnumerable<T> source,
            Gen<T, int, bool> predicateWithIndex)
        {
            return EH.Where(source, predicateWithIndex);
        }

        public virtual IEnumerable<T> LongWhere<T>(
            IEnumerable<T> source,
            Gen<T, long, bool> predicateWithIndex)
        {
            return EH.LongWhere(source, predicateWithIndex);
        }

        public virtual IEnumerable<T> Skip<T>(
            IEnumerable<T> source,
            int skipCount)
        {
            return EH.Skip(source, skipCount);
        }

        public virtual IEnumerable<T> Take<T>(
            IEnumerable<T> source,
            int takeCount)
        {
            return EH.Take(source, takeCount);
        }

        public virtual IEnumerable<T> TakeWhile<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.TakeWhile(source, predicate);
        }

        public virtual IEnumerable<T> TakeWhile<T>(
            IEnumerable<T> source,
            Gen<T, long, bool> predicateWithIndex)
        {
            return EH.TakeWhile(source, predicateWithIndex);
        }

        public virtual IEnumerable<T> TakeLast<T>(
            IEnumerable<T> finiteSource,
            int takeCount)
        {
            return EH.TakeLast(finiteSource, takeCount);
        }

        public virtual IEnumerable<T> Concat<T>(
            IEnumerable<T> firstSource,
            IEnumerable<T> secondSource)
        {
            return EH.Concat(firstSource, secondSource);
        }

        public virtual IEnumerable<T> Append<T>(
            IEnumerable<T> source,
            T appendee)
        {
            return EH.Append(source, appendee);
        }

        public virtual IEnumerable<T> Prepend<T>(
            IEnumerable<T> source,
            T prependee)
        {
            return EH.Prepend(source, prependee);
        }

        public virtual IEnumerable<T> Insert<T>(
            IEnumerable<T> source,
            T itemToInsert,
            long insertionIndex)
        {
            return EH.Insert(
                source,
                itemToInsert,
                insertionIndex);
        }

        public virtual T First<T>(
            IEnumerable<T> source)
        {
            return EH.First(source);
        }

        public virtual T First<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.First(source, predicate);
        }

        public virtual T FirstOrDefault<T>(
            IEnumerable<T> source)
        {
            return EH.FirstOrDefault(
                source);
        }

        public virtual T FirstOrDefault<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.FirstOrDefault(
                source,
                predicate);
        }

        public virtual T FirstOrNull<T>(
            IEnumerable<T> finiteSource)
            where T : class
        {
            return EH.FirstOrNull(finiteSource);
        }

        public virtual T FirstOrNull<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
            where T : class
        {
            return EH.FirstOrNull(finiteSource, predicate);
        }

        public virtual T Last<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.Last(finiteSource);
        }

        public virtual T Last<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            return EH.Last(finiteSource, predicate);
        }

        public virtual T LastOrDefault<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.LastOrDefault(finiteSource);
        }

        public virtual T LastOrDefault<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            return EH.LastOrDefault(
                finiteSource,
                predicate);
        }

        public virtual T LastOrNull<T>(
            IEnumerable<T> finiteSource)
            where T : class
        {
            return EH.LastOrNull(
                finiteSource);
        }

        public virtual T LastOrNull<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
            where T : class
        {
            return EH.LastOrNull(
                finiteSource,
                predicate);
        }

        public virtual bool Any<T>(
            IEnumerable<T> source)
        {
            return EH.Any(source);
        }

        public virtual bool Any<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.Any(source, predicate);
        }

        public virtual bool All<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.All(source, predicate);
        }

        public virtual bool Contains<T>(
            IEnumerable<T> source,
            T item)
        {
            return EH.Contains(source, item);
        }

        public virtual IEnumerable<T> Cast<T>(
            IEnumerable source)
        {
            return EH.Cast<T>(source);
        }

        public virtual IEnumerable<T> SafeCast<T>(
            IEnumerable source)
        {
            return EH.SafeCast<T>(source);
        }

        public virtual int Count<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.Count(finiteSource);
        }

        public virtual int Count<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            return EH.Count(finiteSource, predicate);
        }

        public virtual long LongCount<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.LongCount(finiteSource);
        }

        public virtual long LongCount<T>(
            IEnumerable<T> finiteSource,
            Gen<T, bool> predicate)
        {
            return EH.LongCount(finiteSource, predicate);
        }

        public virtual T[] ToArray<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.ToArray(finiteSource);
        }

        public virtual T[] ToArray<T>(
            Lot<T> lot)
        {
            return EH.ToArray(lot);
        }

        public virtual List<T> ToList<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.ToList(finiteSource);
        }

        public virtual ICollection<T> OrderBy<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector)
        {
            return EH.OrderBy(
                finiteSource,
                keySelector);
        }

        public virtual ICollection<T> OrderBy<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return EH.OrderBy(
                finiteSource,
                keySelector,
                comparer);
        }

        public virtual ICollection<T> OrderByDescending<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector)
        {
            return EH.OrderByDescending(
                finiteSource,
                keySelector);
        }

        public virtual ICollection<T> OrderByDescending<T, TKey>(
            IEnumerable<T> finiteSource,
            Gen<T, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return EH.OrderByDescending(
                finiteSource,
                keySelector,
                comparer);
        }

        public virtual TEnd Aggregate<T, TEnd>(
            IEnumerable<T> finiteSource,
            TEnd seed,
            Gen<TEnd, T, TEnd> accumulator)
        {
            return EH.Aggregate(
                finiteSource,
                seed,
                accumulator);
        }

        public virtual IEnumerable<T> OfType<T>(
            IEnumerable source)
        {
            return EH.OfType<T>(source);
        }

        public virtual IEnumerable<T> SafeForEach<T>(
            IEnumerable<T> source)
        {
            return EH.SafeForEach(source);
        }

        public virtual IEnumerable<T> Iterate<T>(
            params T[] items)
        {
            return EH.Iterate(items);
        }

        public virtual IEnumerable<T> PrivateFieldsOfType<T>(
            object o)
        {
            return EH.PrivateFieldsOfType<T>(o);
        }

        public virtual int Sum<T>(
            IEnumerable<T> finiteSource,
            Gen<T, int> valueComputer)
        {
            return EH.Sum(finiteSource, valueComputer);
        }

        public virtual long Sum<T>(
            IEnumerable<T> finiteSource,
            Gen<T, long> valueComputer)
        {
            return EH.Sum(finiteSource, valueComputer);
        }

        public virtual int Min(
            IEnumerable<int> finiteSource)
        {
            return EH.Min(finiteSource);
        }

        public virtual long Min(
            IEnumerable<long> finiteSource)
        {
            return EH.Min(finiteSource);
        }

        public virtual int Max(
            IEnumerable<int> finiteSource)
        {
            return EH.Max(finiteSource);
        }

        public virtual long Max(
            IEnumerable<long> finiteSource)
        {
            return EH.Max(finiteSource);
        }

        public virtual ICollection<T> Reverse<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.Reverse(finiteSource);
        }

        public virtual TKey KeyLookup<TValue, TKey>(
            IEnumerable<KeyValuePair<TKey, TValue>> dictionary,
            TValue value)
        {
            return EH.KeyLookup(
                dictionary,
                value);
        }

        public virtual IEnumerable<TKey> KeysLookup<TValue, TKey>(
            IEnumerable<KeyValuePair<TKey, TValue>> dictionary,
            TValue value)
        {
            return EH.KeysLookup(
                dictionary,
                value);
        }

        public virtual T Single<T>(
            IEnumerable<T> singleSource)
        {
            return EH.Single(
                singleSource);
        }

        public virtual T Single<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.Single(
                source,
                predicate);
        }

        public virtual T SingleOrDefault<T>(
            IEnumerable<T> source)
        {
            return EH.SingleOrDefault(
                source);
        }

        public virtual T SingleOrDefault<T>(
            IEnumerable<T> source,
            Gen<T, bool> predicate)
        {
            return EH.SingleOrDefault(
                source,
                predicate);
        }

        public virtual IEnumerable<T> Except<T>(
            IEnumerable<T> finiteSource,
            IEnumerable<T> finiteExceptions)
        {
            return EH.Except(
                finiteSource,
                finiteExceptions);
        }

        public virtual IEnumerable<T> Distinct<T>(
            IEnumerable<T> finiteSource)
        {
            return EH.Distinct(
                finiteSource);
        }

        public virtual IEnumerable<T> Union<T>(
            IEnumerable<T> finiteSource1,
            IEnumerable<T> finiteSource2)
        {
            return EH.Union(
                finiteSource1,
                finiteSource2);
        }

        public virtual IEnumerable<T> Intersect<T>(
            IEnumerable<T> finiteSource1,
            IEnumerable<T> finiteSource2)
        {
            return EH.Intersect(
                finiteSource1,
                finiteSource2);
        }

        public virtual T ElementAt<T>(
            IEnumerable<T> source,
            long index)
        {
            return EH.ElementAt(
                source,
                index);
        }

        public virtual long IndexOf<T>(
            IEnumerable<T> source,
            T o)
        {
            return EH.IndexOf(
                source,
                o);
        }

        public virtual IEnumerable<T> Range<T>(
            IEnumerable<T> source,
            long startIndex,
            long count)
        {
            return EH.Range(
                source,
                startIndex,
                count);
        }

        public virtual IEnumerable<int> Range(
            int start,
            int count)
        {
            return EH.Range(
                start,
                count);
        }

        public virtual IEnumerable<int> ReverseRange(
            int start,
            int count)
        {
            return EH.ReverseRange(
                start,
                count);
        }
    }
}
