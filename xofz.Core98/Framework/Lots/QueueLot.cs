namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class QueueLot<T> : Lot<T>
    {
        public QueueLot()
        {
            this.queue = new Queue<T>();
        }

        public QueueLot(Queue<T> queue)
        {
            this.queue = queue;
        }

        long Lot<T>.Count => this.queue.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            return this.queue.ToArray();
        }

        public T Peek()
        {
            return this.queue.Peek();
        }

        public T Dequeue()
        {
            return this.queue.Dequeue();
        }

        public bool Contains(T item)
        {
            return this.queue.Contains(item);
        }

        public void Clear()
        {
            this.queue.Clear();
        }

        public void CopyTo(T[] array)
        {
            this.queue.CopyTo(array, 0);
        }

        public void Enqueue(T item)
        {
            this.queue.Enqueue(item);
        }

        public void TrimExcess()
        {
            this.queue.TrimExcess();
        }

        protected readonly Queue<T> queue;
    }
}
