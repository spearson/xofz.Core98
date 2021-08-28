namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class XLinkedListLot<T> 
        : Lot<T>
    {
        public XLinkedListLot()
            : this(new XLinkedList<T>())
        {
        }

        public XLinkedListLot(
            XLinkedList<T> linkedList)
        {
            this.linkedList = linkedList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var ll = this.linkedList;
            return ll?.GetEnumerator() ??
                   EnumerableHelpers.Empty<T>().
                       GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual long Count => this.linkedList?.Count ?? minusOne;

        public virtual T Head
        {
            get
            {
                var ll = this.linkedList;
                if (ll == null)
                {
                    return default;
                }

                return ll.Head;
            }
        }

        public virtual T Tail
        {
            get
            {
                var ll = this.linkedList;
                if (ll == null)
                {
                    return default;
                }

                return ll.Tail;
            }
        }

        public virtual XLinkedListNode<T> HeadN => this.linkedList?.HeadN;

        public virtual XLinkedListNode<T> TailN => this.linkedList?.TailN;

        public virtual void AddHead(
            T o)
        {
            this.linkedList?.AddHead(o);
        }

        public virtual void AddTail(
            T o)
        {
            this.linkedList?.AddTail(o);
        }

        public virtual void RemoveHead()
        {
            this.linkedList?.RemoveHead();
        }

        public virtual void RemoveTail()
        {
            this.linkedList?.RemoveTail();
        }

        public virtual void Clear()
        {
            this.linkedList?.Clear();
        }

        public virtual bool Contains(
            T o)
        {
            
            return this.linkedList?.Contains(o) ?? falsity;
        }

        public virtual void CopyTo(
            T[] array)
        {
            const byte zero = 0;
            this.linkedList?.CopyTo(array, zero);
        }

        public virtual void CopyTo(
            T[] array,
            int arrayIndex)
        {
            this.linkedList?.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(
            T o)
        {
            return this.linkedList?.Remove(
                o) ?? falsity;
        }

        protected readonly XLinkedList<T> linkedList;
        protected const short minusOne = -1;
        protected const bool falsity = false;
    }
}
