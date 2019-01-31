namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableTrapper<T>
    {
        public virtual ICollection<T> TrappedCollection => this.trapper;

        public virtual IEnumerable<T> Trap(IEnumerable<T> source)
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

            ICollection<T> t = new LinkedList<T>();
            this.setTrapper(t);
            // we can use any type of ICollection<T> here
            // (that supports adding items)
            var fieldReference = this.trapper;

            foreach (var item in source)
            {
                fieldReference.Add(item);
                yield return item;
            }
        }

        public virtual void TrapNow(IEnumerable<T> source)
        {
            if (source is ICollection<T> collection)
            {
                this.setTrapper(collection);
                return;
            }

            ICollection<T> t = new LinkedList<T>();
            this.setTrapper(t);
            if (source == default(IEnumerable<T>))
            {
                return;
            }

            // we can use any type of ICollection<T> here
            // (that supports adding items)
            var fieldReference = this.trapper;

            foreach (var item in source)
            {
                fieldReference.Add(item);
            }
        }

        protected virtual void setTrapper(ICollection<T> trapper)
        {
            this.trapper = trapper;
        }

        protected ICollection<T> trapper;
    }
}
