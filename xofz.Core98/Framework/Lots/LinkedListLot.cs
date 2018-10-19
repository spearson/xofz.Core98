namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class LinkedListLot<T> : Lot<T>
    {
        public LinkedListLot()
        {
            this.linkedList = new LinkedList<T>();
        }

        public LinkedListLot(IEnumerable<T> source)
        {
            this.linkedList = new LinkedList<T>(source);
        }

        public LinkedListLot(LinkedList<T> linkedList)
        {
            this.linkedList = linkedList;
        }

        long Lot<T>.Count => this.linkedList.Count;

        public LinkedListNode<T> First => this.linkedList.First;

        public LinkedListNode<T> Last => this.linkedList.Last; 

        public IEnumerator<T> GetEnumerator()
        {
            return this.linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public LinkedListNode<T> Find(T value)
        {
            return this.linkedList.Find(value);
        }

        public LinkedListNode<T> FindLast(T value)
        {
            return this.linkedList.FindLast(value);
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            return this.linkedList.AddFirst(value);
        }

        public LinkedListNode<T> AddLast(T value)
        {
            return this.linkedList.AddLast(value);
        }

        public bool Contains(T value)
        {
            return this.linkedList.Contains(value);
        }

        public bool Remove(T value)
        {
            return this.linkedList.Remove(value);
        }

        public void AddAfter(LinkedListNode<T> node, T value)
        {
            this.linkedList.AddAfter(node, value);
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            this.linkedList.AddAfter(node, newNode);
        }

        public void AddBefore(LinkedListNode<T> node, T value)
        {
            this.linkedList.AddBefore(node, value);
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            this.linkedList.AddBefore(node, newNode);
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            this.linkedList.AddFirst(node);
        }

        public void AddLast(LinkedListNode<T> node)
        {
            this.linkedList.AddLast(node);
        }

        public void Clear()
        {
            this.linkedList.Clear();
        }

        public void CopyTo(T[] array)
        {
            this.linkedList.CopyTo(array, 0);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.linkedList.GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            this.linkedList.OnDeserialization(sender);
        }

        public void Remove(LinkedListNode<T> node)
        {
            this.linkedList.Remove(node);
        }

        public void RemoveFirst()
        {
            this.linkedList.RemoveFirst();
        }

        public void RemoveLast()
        {
            this.linkedList.RemoveLast();
        }

        protected readonly LinkedList<T> linkedList;
    }
}
