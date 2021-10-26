namespace xofz.Framework.Lots
{
    public class IndexedLinkedListLot<T>
        : XLinkedListLot<T>, GetArray<T>
    {
        public IndexedLinkedListLot()
            : this(new IndexedLinkedList<T>())
        {
        }

        public IndexedLinkedListLot(
            IndexedLinkedList<T> ill)
            : base(ill)

        {
            this.ill = ill;
        }

        public virtual T this[long index]
        {
            get
            {
                var ll = this.ill;
                if (ll == null)
                {
                    return default;
                }

                return ll[index];
            }

            set
            {
                var ll = this.ill;
                if (ll == null)
                {
                    return;
                }

                ll[index] = value;
            }
        }

        protected readonly IndexedLinkedList<T> ill;
    }
}
