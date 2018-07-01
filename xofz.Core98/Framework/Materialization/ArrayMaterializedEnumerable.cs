namespace xofz.Framework.Materialization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public sealed class ArrayMaterializedEnumerable<T> : MaterializedEnumerable<T>
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

        public bool Contains(T item)
        {
            return new Collection<T>(this.array).Contains(item);
        }

        public void CopyTo(T[] array)
        {
            Array.Copy(this.array, array, array.Length);
        }

        private readonly T[] array;
    }
}
