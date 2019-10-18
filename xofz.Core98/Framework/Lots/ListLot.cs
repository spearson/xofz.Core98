namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ListLot<T> 
        : GetArray<T>
    {
        public ListLot()
            : this(new List<T>())
        {
        }

        public ListLot(
            long capacity)
        {
            this.list = new List<T>((int)capacity);
        }

        public ListLot(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                this.list = new List<T>();
                return;
            }

            var l = finiteSource as List<T>;
            if (l != null)
            {
                this.list = l;
                return;
            }

            this.list = new List<T>(finiteSource);
        }

        public virtual long Count => this.list.Count;

        public virtual long Capacity
        {
            get => this.list.Capacity;

            set => this.list.Capacity = (int)value;
        }

        public virtual T this[long index]
        {
            get => this.list[(int)index];

            set => this.list[(int)index] = value;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<T> source = this;
            return source.GetEnumerator();
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.list.CopyTo(array);
        }

        public virtual void CopyTo(
            T[] array, 
            int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public virtual void CopyTo(
            int index, 
            T[] array, 
            int arrayIndex,
            int count)
        {
            this.list.CopyTo(
                index, 
                array, 
                arrayIndex,
                count);
        }

        public virtual void Add(
            T item)
        {
            this.list.Add(item);
        }

        public virtual void AddRange(
            IEnumerable<T> collection)
        {
            this.list.AddRange(collection);
        }

        public virtual ReadOnlyCollection<T> AsReadOnly()
        {
            return this.list.AsReadOnly();
        }

        public virtual int BinarySearch(
            T item)
        {
            return this.list.BinarySearch(item);
        }

        public virtual int BinarySearch(
            T item, 
            IComparer<T> comparer)
        {
            return this.list.BinarySearch(item, comparer);
        }

        public virtual int BinarySearch(
            int index,
            int count,
            T item,
            IComparer<T> comparer)
        {
            return this.list.BinarySearch(index, count, item, comparer);
        }

        public virtual void Clear()
        {
            this.list.Clear();
        }

        public virtual bool Contains(
            T item)
        {
            return this.list.Contains(item);
        }

        public virtual List<TOutput> ConvertAll<TOutput>(
            Converter<T, TOutput> converter)
        {
            return this.list.ConvertAll(converter);
        }

        public virtual bool Exists(
            Predicate<T> match)
        {
            return this.list.Exists(match);
        }

        public virtual T Find(
            Predicate<T> match)
        {
            return this.list.Find(match);
        }

        public virtual List<T> FindAll(
            Predicate<T> match)
        {
            return this.list.FindAll(match);
        }

        public virtual int FindIndex(
            Predicate<T> match)
        {
            return this.list.FindIndex(match);
        }

        public virtual int FindIndex(
            int startIndex, 
            Predicate<T> match)
        {
            return this.list.FindIndex(startIndex, match);
        }

        public virtual int FindIndex(
            int startIndex, 
            int count, 
            Predicate<T> match)
        {
            return this.list.FindIndex(startIndex, count, match);
        }

        public virtual T FindLast(
            Predicate<T> match)
        {
            return this.list.FindLast(match);
        }

        public virtual int FindLastIndex(
            Predicate<T> match)
        {
            return this.list.FindLastIndex(match);
        }

        public virtual int FindLastIndex(
            int startIndex, 
            Predicate<T> match)
        {
            return this.list.FindLastIndex(startIndex, match);
        }

        public virtual int FindLastIndex(
            int startIndex, 
            int count, 
            Predicate<T> match)
        {
            return this.list.FindLastIndex(startIndex, count, match);
        }

        public virtual void ForEach(
            Action<T> action)
        {
            this.list.ForEach(action);
        }

        public virtual List<T> GetRange(
            int index, 
            int count)
        {
            return this.list.GetRange(index, count);
        }

        public virtual int IndexOf(
            T item)
        {
            return this.list.IndexOf(item);
        }

        public virtual int IndexOf(
            T item, 
            int index)
        {
            return this.list.IndexOf(item, index);
        }

        public virtual int IndexOf(
            T item, 
            int index, 
            int count)
        {
            return this.list.IndexOf(item, index, count);
        }

        public virtual void Insert(
            int index, 
            T item)
        {
            this.list.Insert(index, item);
        }

        public virtual void InsertRange(
            int index,
            IEnumerable<T> collection)
        {
            this.list.InsertRange(index, collection);
        }

        public virtual bool Remove(
            T item)
        {
            return this.list.Remove(item);
        }

        public virtual int RemoveAll(
            Predicate<T> match)
        {
            return this.list.RemoveAll(match);
        }

        public virtual void RemoveAt(
            int index)
        {
            this.list.RemoveAt(index);
        }

        public virtual void RemoveRange(
            int index,
            int count)
        {
            this.list.RemoveRange(index, count);
        }

        public virtual void Reverse()
        {
            this.list.Reverse();
        }

        public virtual void Reverse(
            int index, 
            int count)
        {
            this.list.Reverse(index, count);
        }

        public virtual void Sort()
        {
            this.list.Sort();
        }

        public virtual void Sort(
            IComparer<T> comparer)
        {
            this.list.Sort(comparer);
        }

        public virtual void Sort(
            Comparison<T> comparison)
        {
            this.list.Sort(comparison);
        }

        public virtual void Sort(
            int index, 
            int count,
            IComparer<T> comparer)
        {
            this.list.Sort(index, count, comparer);
        }

        public virtual T[] ToArray()
        {
            return this.list.ToArray();
        }

        public virtual void TrimExcess()
        {
            this.list.TrimExcess();
        }

        public virtual int LastIndexOf(
            T item)
        {
            return this.list.LastIndexOf(item);
        }

        public virtual int LastIndexOf(
            T item, 
            int index)
        {
            return this.list.LastIndexOf(item, index);
        }

        public virtual int LastIndexOf(
            T item, 
            int index,
            int count)
        {
            return this.list.LastIndexOf(item, index, count);
        }

        public virtual bool TrueForAll(
            Predicate<T> match)
        {
            return this.list.TrueForAll(match);
        }

        protected readonly List<T> list;
    }
}
