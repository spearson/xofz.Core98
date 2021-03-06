namespace xofz.Framework.Lots
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ArrayLot<T> 
        : GetArray<T>
    {
        public ArrayLot(
            T[] array)
        {
            if (array == null)
            {
                this.array = new T[0];
                return;
            }

            this.array = array;
        }

        public virtual long Count => this.array.Length;

        public virtual T this[long index]
        {
            get => this.array[index];

            set => this.array[index] = value;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)this.array)
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

        public virtual bool Contains(
            T item)
        {
            return EnumerableHelpers.Contains(
                this.array,
                item);
        }

        public virtual void CopyTo(
            T[] array)
        {
            Array.Copy(
                this.array, 
                array, 
                array.Length);
        }

        protected readonly T[] array;
        protected const short nOne = -1;
    }
}
