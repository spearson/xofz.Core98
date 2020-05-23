namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class QueueLot<T> 
        : Lot<T>
    {
        public QueueLot()
        {
            this.queue = new Queue<T>();
        }

        public QueueLot(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                this.queue = new Queue<T>();
                return;
            }

            if (finiteSource is Queue<T> q)
            {
                this.queue = q;
                return;
            }

            this.queue = new Queue<T>(finiteSource);
        }

        public virtual long Count => this.queue.Count;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<T> source = this;
            return source.GetEnumerator();
        }

        public virtual T[] ToArray()
        {
            return this.queue.ToArray();
        }

        public virtual T Peek()
        {
            return this.queue.Peek();
        }

        public virtual T Dequeue()
        {
            return this.queue.Dequeue();
        }

        public virtual bool Contains(
            T item)
        {
            return this.queue.Contains(item);
        }

        public virtual void Clear()
        {
            this.queue.Clear();
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.queue.CopyTo(array, 0);
        }

        public virtual void Enqueue(
            T item)
        {
            this.queue.Enqueue(item);
        }

        public virtual void TrimExcess()
        {
            this.queue.TrimExcess();
        }

        protected readonly Queue<T> queue;
    }
}
