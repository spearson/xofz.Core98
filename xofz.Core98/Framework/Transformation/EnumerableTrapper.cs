namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableTrapper<T>
    {
        public virtual ICollection<T> TrappedCollection => this.trapper;

        public virtual IEnumerable<T> Trap(
            IEnumerable<T> source)
        {
            if (source == null)
            {
                ICollection<T> defaultCollection = new XLinkedList<T>();
                this.setTrapper(defaultCollection);
                yield break;
            }

            if (source is ICollection<T> collection)
            {
                this.setTrapper(collection);
                foreach (var item in collection)
                {
                    yield return item;
                }

                yield break;
            }

            this.setTrapper(new XLinkedList<T>());
            // we can use any type of ICollection<T> here
            // (that supports adding items)
            var fieldReference = this.trapper;

            foreach (var item in source)
            {
                fieldReference?.Add(item);
                yield return item;
            }
        }

        public virtual IEnumerable<T> Trap(
            IEnumerator<T> enumerator)
        {
            this.setTrapper(
                new XLinkedList<T>());
            if (enumerator == null)
            {
                yield break;
            }

            var t = this.trapper;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                t?.Add(current);
                yield return current;
            }
        }

        public virtual ICollection<T> TrapNow(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource is ICollection<T> collection)
            {
                this.setTrapper(collection);
                return collection;
            }

            this.setTrapper(
                new XLinkedList<T>());
            // we can use any type of ICollection<T> here
            // (that supports adding items)
            var fieldReference = this.trapper;

            if (finiteSource == null)
            {
                return fieldReference;
            }

            foreach (var item in finiteSource)
            {
                fieldReference?.Add(item);
            }

            return fieldReference;
        }

        protected virtual void setTrapper(
            ICollection<T> trapper)
        {
            this.trapper = trapper;
        }

        protected ICollection<T> trapper;
    }
}
