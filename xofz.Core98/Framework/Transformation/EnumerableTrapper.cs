namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableTrapper<T>
    {
        public virtual ICollection<T> TrappedCollection => this.trapper;

        public virtual IEnumerable<T> Trap(IEnumerable<T> source)
        {
            ICollection<T> t = new LinkedList<T>();
            this.setTrapper(t);
            // we can use any type of ICollection<T> here
            var fieldReference = this.trapper;

            foreach (var item in source)
            {
                fieldReference.Add(item);
                yield return item;
            }
        }

        public virtual void TrapNow(IEnumerable<T> source)
        {
            ICollection<T> t = new LinkedList<T>();
            this.setTrapper(t);
            // we can use any type of ICollection<T> here
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

        protected volatile ICollection<T> trapper;
    }
}
