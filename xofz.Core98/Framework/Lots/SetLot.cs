namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class SetLot<T>
        : Lot<T>
    {
        public SetLot()
            : this(null)
        {
        }

        protected SetLot(
            ICollection<T> collection)
        {
            this.collection = collection ??
                              new XLinkedList<T>();
        }

        public virtual long Count => this.collection.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool Add(
            T item)
        {
            var c = this.collection;
            var itemIsNull = item == null;
            foreach (var t in c)
            {
                if (t?.Equals(item) ?? itemIsNull)
                {
                    return falsity;
                }
            }

            c.Add(item);
            return truth;
        }

        public virtual bool Contains(
            T item)
        {
            return this.collection.Contains(
                       item);
        }

        public virtual bool Remove(
            T item)
        {
            return this.collection.Remove(
                       item);
        }

        protected readonly ICollection<T> collection;

        protected const bool
            truth = true,
            falsity = false;
    }
}
