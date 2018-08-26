namespace xofz.Framework.Materialization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ArrayMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public ArrayMaterializedEnumerable(T[] array)
        {
            this.array = array;
        }

        long MaterializedEnumerable<T>.Count => this.array.Length;

        public T this[int index]
        {
            get => this.array[index];

            set => this.array[index] = value;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)this.array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.array.GetEnumerator();
        }

        public virtual bool Contains(T item)
        {
            return EnumerableHelpers.Contains(
                this.array,
                item);
        }

        public void CopyTo(T[] array)
        {
            Array.Copy(this.array, array, array.Length);
        }

        protected readonly T[] array;
    }
}
