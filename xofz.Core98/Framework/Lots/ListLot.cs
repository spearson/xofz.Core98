namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ListLot<T> : Lot<T>
    {
        public ListLot()
            : this(new List<T>())
        {
        }

        public ListLot(int capacity)
            : this(new List<T>(capacity))
        {
        }

        public ListLot(IEnumerable<T> source)
            : this(new List<T>(source))
        {
        }

        public ListLot(List<T> list)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public long Count => this.list.Count;

        public long Capacity => this.list.Capacity;

        public T this[int index]
        {
            get => this.list[index];

            set => this.list[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void CopyTo(T[] array)
        {
            this.list.CopyTo(array);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            this.list.CopyTo(index, array, arrayIndex, count);
        }

        public void Add(T item)
        {
            this.list.Add(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            this.list.AddRange(collection);
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return this.list.AsReadOnly();
        }

        public int BinarySearch(T item)
        {
            return this.list.BinarySearch(item);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return this.list.BinarySearch(item, comparer);
        }

        public int BinarySearch(
            int index,
            int count,
            T item,
            IComparer<T> comparer)
        {
            return this.list.BinarySearch(index, count, item, comparer);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        public List<TOutput> ConvertAll<TOutput>(
            Converter<T, TOutput> converter)
        {
            return this.list.ConvertAll(converter);
        }

        public bool Exists(Predicate<T> match)
        {
            return this.list.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return this.list.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return this.list.FindAll(match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return this.list.FindIndex(match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return this.list.FindIndex(startIndex, match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return this.list.FindIndex(startIndex, count, match);
        }

        public T FindLast(Predicate<T> match)
        {
            return this.list.FindLast(match);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return this.list.FindLastIndex(match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return this.list.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return this.list.FindLastIndex(startIndex, count, match);
        }

        public void ForEach(Action<T> action)
        {
            this.list.ForEach(action);
        }

        public List<T> GetRange(int index, int count)
        {
            return this.list.GetRange(index, count);
        }

        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        public int IndexOf(T item, int index)
        {
            return this.list.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return this.list.IndexOf(item, index, count);
        }

        public void Insert(int index, T item)
        {
            this.list.Insert(index, item);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            this.list.InsertRange(index, collection);
        }

        public bool Remove(T item)
        {
            return this.list.Remove(item);
        }

        public int RemoveAll(Predicate<T> match)
        {
            return this.list.RemoveAll(match);
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        public void RemoveRange(int index, int count)
        {
            this.list.RemoveRange(index, count);
        }

        public void Reverse()
        {
            this.list.Reverse();
        }

        public void Reverse(int index, int count)
        {
            this.list.Reverse(index, count);
        }

        public void Sort()
        {
            this.list.Sort();
        }

        public void Sort(IComparer<T> comparer)
        {
            this.list.Sort(comparer);
        }

        public void Sort(Comparison<T> comparison)
        {
            this.list.Sort(comparison);
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            this.list.Sort(index, count, comparer);
        }

        public T[] ToArray()
        {
            return this.list.ToArray();
        }

        public void TrimExcess()
        {
            this.list.TrimExcess();
        }

        public int LastIndexOf(T item)
        {
            return this.list.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return this.list.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return this.list.LastIndexOf(item, index, count);
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return this.list.TrueForAll(match);
        }

        protected readonly List<T> list;
    }
}
