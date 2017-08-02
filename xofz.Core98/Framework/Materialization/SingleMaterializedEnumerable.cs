namespace xofz.Framework.Materialization
{
    using System.Collections;
    using System.Collections.Generic;

    public sealed class SingleMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public SingleMaterializedEnumerable(T item)
        {
            this.item = item;
        }

        long MaterializedEnumerable<T>.Count => 1;

        public IEnumerator<T> GetEnumerator()
        {
            yield return this.item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void CopyTo(T[] array)
        {
            array[0] = this.item;
        }

        public bool Contains(T item)
        {
            return this.item.Equals(item);
        }

        private readonly T item;
    }
}
