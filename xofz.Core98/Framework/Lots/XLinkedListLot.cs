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
                return ll == null 
                    ? default 
                    : ll.Head;
            }
        }

        public virtual T Tail
        {
            get
            {
                var ll = this.linkedList;
                return ll == null 
                    ? default 
                    : ll.Tail;
            }
        }

        public virtual XLinkedListNode<T> HeadN => this.linkedList?.HeadN;

        public virtual XLinkedListNode<T> TailN => this.linkedList?.TailN;

        public virtual IEnumerable<XLinkedListNode<T>> GetNodes()
        {
            return this.linkedList?.GetNodes();
        }

        public virtual XLinkedListNode<T> GetNode(
            T item)
        {
            return this.linkedList?.GetNode(item);
        }

        public virtual void Append(
            IEnumerable<T> finiteSource)
        {
            this.linkedList?.Append(
                finiteSource);
        }

        public virtual XLinkedListNode<T> AddHead(
            T o)
        {
            return this.linkedList?.AddHead(o);
        }

        public virtual XLinkedListNode<T> AddHead(
            XLinkedListNode<T> newHead)
        {
            return this.linkedList?.AddHead(newHead);
        }

        public virtual XLinkedListNode<T> AddTail(
            T o)
        {
            return this.linkedList?.AddTail(o);
        }

        public virtual XLinkedListNode<T> AddTail(
            XLinkedListNode<T> newTail)
        {
            return this.linkedList?.AddTail(
                newTail);
        }

        public virtual XLinkedListNode<T> RemoveHead()
        {
            return this.linkedList?.RemoveHead();
        }

        public virtual XLinkedListNode<T> RemoveTail()
        {
            return this.linkedList?.RemoveTail();
        }

        public virtual XLinkedListNode<T> AddAfter(
            XLinkedListNode<T> node,
            T o)
        {
            return this.linkedList?.AddAfter(node, o);
        }

        public virtual XLinkedListNode<T> AddAfter(
            XLinkedListNode<T> node,
            XLinkedListNode<T> newNode)
        {
            return this.linkedList?.AddAfter(
                node,
                newNode);
        }

        public virtual XLinkedListNode<T> AddBefore(
            XLinkedListNode<T> node,
            T o)
        {
            return this.linkedList?.AddBefore(
                node,
                o);
        }

        public virtual XLinkedListNode<T> AddBefore(
            XLinkedListNode<T> node,
            XLinkedListNode<T> newNode)
        {
            return this.linkedList?.AddBefore(
                node,
                newNode);
        }

        public virtual XLinkedListNode<T> Add(
            T item)
        {
            return this.linkedList?.AddTail(
                item);
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
