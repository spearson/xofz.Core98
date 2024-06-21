namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class LinkedListLot<T>
        : Lot<T>
    {
        public LinkedListLot()
        {
            this.linkedList = new LinkedList<T>();
        }

        public LinkedListLot(
            IEnumerable<T> finiteSource)
        {
            switch (finiteSource)
            {
                case null:
                    this.linkedList = new LinkedList<T>();
                    return;
                case LinkedList<T> ll:
                    this.linkedList = ll;
                    return;
                case LinkedListLot<T> lot:
                    this.linkedList = lot.linkedList;
                    return;
                default:
                    this.linkedList = new LinkedList<T>(
                        finiteSource);
                    break;
            }
        }

        public LinkedListLot(
            IEnumerator<T> finiteEnumerator)
        {
            var ll = new LinkedList<T>();

            if (finiteEnumerator == null)
            {
                this.linkedList = ll;
                return;
            }

            while (finiteEnumerator.MoveNext())
            {
                ll.AddLast(
                    finiteEnumerator.Current);
            }

            this.linkedList = ll;
        }

        public virtual long Count => this.linkedList.Count;

        public virtual LinkedListNode<T> First => this.linkedList.First;

        public virtual LinkedListNode<T> Last => this.linkedList.Last;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<T> source = this;

            return source.GetEnumerator();
        }

        public virtual LinkedListNode<T> Find(
            T value)
        {
            return this.linkedList.Find(value);
        }

        public virtual LinkedListNode<T> FindLast(
            T value)
        {
            return this.linkedList.FindLast(value);
        }

        public virtual LinkedListNode<T> AddFirst(
            T value)
        {
            return this.linkedList.AddFirst(value);
        }

        public virtual LinkedListNode<T> AddLast(
            T value)
        {
            return this.linkedList.AddLast(value);
        }

        public virtual bool Contains(
            T value)
        {
            return this.linkedList?.Contains(value)
                   ?? falsity;
        }

        public virtual bool Remove(
            T value)
        {
            return this.linkedList?.Remove(value)
                   ?? falsity;
        }

        public virtual void AddFirst(
            LinkedListNode<T> node)
        {
            this.linkedList.AddFirst(node);
        }

        public virtual void AddLast(
            LinkedListNode<T> node)
        {
            this.linkedList.AddLast(node);
        }

        public virtual void AddAfter(
            LinkedListNode<T> node,
            T value)
        {
            this.linkedList.AddAfter(
                node,
                value);
        }

        public virtual void AddAfter(
            LinkedListNode<T> node,
            LinkedListNode<T> newNode)
        {
            this.linkedList.AddAfter(
                node,
                newNode);
        }

        public virtual void AddBefore(
            LinkedListNode<T> node,
            T value)
        {
            this.linkedList.AddBefore(
                node,
                value);
        }

        public virtual void AddBefore(
            LinkedListNode<T> node,
            LinkedListNode<T> newNode)
        {
            this.linkedList.AddBefore(
                node,
                newNode);
        }

        public virtual void Clear()
        {
            this.linkedList.Clear();
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.linkedList.CopyTo(
                array,
                zero);
        }

        public virtual void GetObjectData(
            SerializationInfo info,
            StreamingContext context)
        {
            this.linkedList.GetObjectData(
                info,
                context);
        }

        public virtual void OnDeserialization(
            object sender)
        {
            this.linkedList.OnDeserialization(
                sender);
        }

        public virtual void Remove(
            LinkedListNode<T> node)
        {
            this.linkedList.Remove(node);
        }

        public virtual void RemoveFirst()
        {
            this.linkedList.RemoveFirst();
        }

        public virtual void RemoveLast()
        {
            this.linkedList.RemoveLast();
        }

        protected readonly LinkedList<T> linkedList;
        protected const byte zero = 0;
        protected const bool falsity = false;
    }
}