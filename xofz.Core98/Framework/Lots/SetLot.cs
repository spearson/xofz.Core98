namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class SetLot<T>
        : Lot<T>
    {
        public SetLot()
            : this(new XLinkedList<T>())
        {
        }

        protected SetLot(
            ICollection<T> collection)
        {
            this.collection = collection;
        }

        public virtual long Count
        {
            get
            {
                const short nOne = -1;
                return this.collection?.Count
                       ?? nOne;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.collection?.GetEnumerator()
                   ?? EnumerableHelpers
                       .Empty<T>()
                       .GetEnumerator();
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
                return falsity;
            }

            var itemIsNull = item == null;
            foreach (var t in c)
            {
                if (t?.Equals(item) ?? itemIsNull)
                {
                    return falsity;
                }
            }

            c.Add(item);
            return true;
        }

        public virtual bool Contains(
            T item)
        {
            return this.collection?.Contains(
                       item)
                   ?? falsity;
        }

        public virtual bool Remove(
            T item)
        {
            return this.collection?.Remove(
                       item)
                   ?? falsity;
        }

        protected readonly ICollection<T> collection;
        protected const bool falsity = false;
    }
}
