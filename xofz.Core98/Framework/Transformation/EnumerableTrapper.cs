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
                ICollection<T> defaultCollection = new LinkedList<T>();
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

            this.setTrapper(new LinkedList<T>());
            // we can use any type of ICollection<T> here
            // (that supports adding items)
            var fieldReference = this.trapper;

            foreach (var item in source)
            {
                fieldReference.Add(item);
                yield return item;
            }
        }

        public virtual ICollection<T> TrapNow(
            IEnumerable<T> source)
        {
            if (source is ICollection<T> collection)
            {
                this.setTrapper(collection);
                return collection;
            }

            this.setTrapper(new LinkedList<T>());
            // we can use any type of ICollection<T> here
            // (that supports adding items)
            var fieldReference = this.trapper;

            if (source == null)
            {
                return fieldReference;
            }

            foreach (var item in source)
            {
                fieldReference.Add(item);
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
