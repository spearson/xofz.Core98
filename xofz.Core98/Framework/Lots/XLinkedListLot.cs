namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class XLinkedListLot<T> 
        : Lot<T>
    {
        public XLinkedListLot()
            : this(null)
        {
        }

        public XLinkedListLot(
            XLinkedList<T> linkedList)
        {
            this.linkedList = linkedList ??
                              new XLinkedList<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual long Count => this.linkedList.Count;

        public virtual T Head => this.linkedList.Head;

        public virtual T Tail => this.linkedList.Tail;

        public virtual XLinkedListNode<T> HeadN => this.linkedList.HeadN;

        public virtual XLinkedListNode<T> TailN => this.linkedList.TailN;

        public virtual IEnumerable<XLinkedListNode<T>> GetNodes()
        {
            return this.linkedList.GetNodes();
        }

        public virtual void Append(
            IEnumerable<T> finiteSource)
        {
            this.linkedList.Append(
                finiteSource);
        }

        public virtual XLinkedListNode<T> AddHead(
            T o)
        {
            return this.linkedList.AddHead(
                o);
        }

        public virtual XLinkedListNode<T> AddHead(
            XLinkedListNode<T> newHead)
        {
            return this.linkedList.AddHead(
                newHead);
        }

        public virtual XLinkedListNode<T> AddTail(
            T o)
        {
            return this.linkedList.AddTail(
                o);
        }

        public virtual XLinkedListNode<T> AddTail(
            XLinkedListNode<T> newTail)
        {
            return this.linkedList.AddTail(
                newTail);
        }

        public virtual XLinkedListNode<T> RemoveHead()
        {
            return this.linkedList.RemoveHead();
        }

        public virtual XLinkedListNode<T> RemoveTail()
        {
            return this.linkedList.RemoveTail();
        }

        public virtual XLinkedListNode<T> AddAfter(
            XLinkedListNode<T> node,
            T o)
        {   
            return this.linkedList.AddAfter(
                node, 
                o);
        }

        public virtual XLinkedListNode<T> AddAfter(
            XLinkedListNode<T> node,
            XLinkedListNode<T> newNode)
        {
            return this.linkedList.AddAfter(
                node,
                newNode);
        }

        public virtual XLinkedListNode<T> AddBefore(
            XLinkedListNode<T> node,
            T o)
        {
            return this.linkedList.AddBefore(
                node,
                o);
        }

        public virtual XLinkedListNode<T> AddBefore(
            XLinkedListNode<T> node,
            XLinkedListNode<T> newNode)
        {
            return this.linkedList.AddBefore(
                node,
                newNode);
        }

        public virtual XLinkedListNode<T> Add(
            T item)
        {
            return this.linkedList.AddTail(
                item);
        }

        public virtual void Clear()
        {
            this.linkedList.Clear();
        }

        public virtual bool Contains(
            T o)
        {
            
            return this.linkedList.Contains(
                o);
        }

        public virtual void CopyTo(
            T[] array)
        {
            const byte zero = 0;
            this.linkedList.CopyTo(
                array, 
                zero);
        }

        public virtual void CopyTo(
            T[] array,
            int arrayIndex)
        {
            this.linkedList.CopyTo(
                array, 
                arrayIndex);
        }

        public virtual bool Remove(
            T o)
        {
            return this.linkedList.Remove(
                o);
        }

        protected readonly XLinkedList<T> linkedList;
    }
}
