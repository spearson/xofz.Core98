namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class SetLot<T>
        : Lot<T>
    {
        public SetLot()
            : this(new LinkedList<T>())
        {
        }

        protected SetLot(
            ICollection<T> collection)
        {
            this.collection = collection;
        }

        public virtual long Count => this.collection?.Count ?? -1;

        public IEnumerator<T> GetEnumerator()
        {
            return this.collection?.GetEnumerator()
                   ?? EnumerableHelpers.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Add(
            T item)
        {
            var c = this.collection;
            if (c == null)
            {
                return false;
            }

            var itemIsNull = item == null;
            foreach (var t in c)
            {
                if (t?.Equals(item) ?? itemIsNull)
                {
                    return false;
                }
            }

            c.Add(item);
            return true;
        }

        public virtual bool Contains(
            T item)
        {
            var c = this.collection;
            if (c == null)
            {
                return false;
            }

            return c.Contains(item);
        }

        public virtual bool Remove(
            T item)
        {
            var c = this.collection;
            if (c == null)
            {
                return false;
            }

            return c.Remove(item);
        }

        protected readonly ICollection<T> collection;
    }
}
