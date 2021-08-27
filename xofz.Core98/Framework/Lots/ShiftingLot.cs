namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class ShiftingLot<T> 
        : GetArray<T>
    {
        public ShiftingLot(
            long capacity)
        {
            if (capacity < zero)
            {
                capacity = zero;
            }

            this.capacity = capacity;
            this.linkedList = new XLinkedList<T>();
        }

        public virtual T this[long index]
        {
            get
            {
                var a = this.currentArray;
                if (a == null)
                {
                    return default;
                }

                return a[index];
            }
        }

        public virtual long MaxSize => this.capacity;

        public virtual long CurrentSize => this.linkedList.Count;

        public virtual long Count => this.CurrentSize;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.linkedList?.GetEnumerator()
                   ?? EnumerableHelpers
                       .Empty<T>()
                       .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<T> source = this;
            return source?.GetEnumerator()
                ?? EnumerableHelpers
                    .Empty<T>()
                    .GetEnumerator();
        }

        public virtual void ShiftRight(
            T input)
        {
            var ll = this.linkedList;
            ll?.AddHead(input);
            var c = this.capacity;

            while (ll?.Count > c)
            {
                ll.RemoveTail();
            }

            var array = new T[
                ll?.Count ?? zero];
            ll?.CopyTo(
                array, 
                zero);
            this.setCurrentArray(
                array);
        }

        public virtual void ShiftLeft(
            T input)
        {
            var ll = this.linkedList;
            ll?.AddTail(input);
            var c = this.capacity;

            while (ll?.Count > c)
            {
                ll.RemoveHead();
            }

            var array = new T[
                ll?.Count ?? zero];
            ll?.CopyTo(
                array, 
                zero);
            this.setCurrentArray(
                array);
        }

        protected virtual void setCurrentArray(
            T[] currentArray)
        {
            this.currentArray = currentArray;
        }

        protected T[] currentArray;
        protected readonly long capacity;
        protected readonly XLinkedList<T> linkedList;
        protected const byte zero = 0;
    }
}
