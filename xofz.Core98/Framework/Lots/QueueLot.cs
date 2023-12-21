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
            switch (finiteSource)
            {
                case null:
                    this.queue = new Queue<T>();
                    return;
                case Queue<T> q:
                    this.queue = q;
                    return;
                case QueueLot<T> lot:
                    this.queue = lot.queue;
                    return;
                default:
                    this.queue = new Queue<T>(
                        finiteSource);
                    break;
            }
        }

        public QueueLot(
            IEnumerator<T> finiteEnumerator)
        {
            var q = new Queue<T>();

            if (finiteEnumerator == null)
            {
                this.queue = q;
                return;
            }

            while (finiteEnumerator.MoveNext())
            {
                q?.Enqueue(
                    finiteEnumerator.Current);
            }

            this.queue = q;
        }

        public virtual long Count => this.queue?.Count
                                     ?? nOne;

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
            return this.queue.Contains(
                item);
        }

        public virtual void Clear()
        {
            this.queue.Clear();
        }

        public virtual void CopyTo(
            T[] array)
        {
            const byte zero = 0;
            this.queue.CopyTo(
                array, 
                zero);
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
        protected const short nOne = -1;
    }
}
