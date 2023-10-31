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

            if (finiteSource is QueueLot<T> lot)
            {
                this.queue = lot.queue
                             ?? new Queue<T>();
                return;
            }

            this.queue = new Queue<T>(
                finiteSource);
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
            return this.queue?.GetEnumerator()
                ?? EnumerableHelpers
                    .Empty<T>()
                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<T> source = this;
            return source.GetEnumerator();
        }

        public virtual T[] ToArray()
        {
            return this.queue?.ToArray();
        }

        public virtual T Peek()
        {
            var q = this.queue;
            if (q == null)
            {
                return default;
            }

            return q.Peek();
        }

        public virtual T Dequeue()
        {
            var q = this.queue;
            if (q == null)
            {
                return default;
            }

            return q.Dequeue();
        }

        public virtual bool Contains(
            T item)
        {
            return this.queue?.Contains(
                item) ?? falsity;
        }

        public virtual void Clear()
        {
            this.queue?.Clear();
        }

        public virtual void CopyTo(
            T[] array)
        {
            const byte zero = 0;
            this.queue?.CopyTo(
                array, 
                zero);
        }

        public virtual void Enqueue(
            T item)
        {
            this.queue?.Enqueue(item);
        }

        public virtual void TrimExcess()
        {
            this.queue?.TrimExcess();
        }

        protected readonly Queue<T> queue;
        protected const short nOne = -1;
        protected const bool falsity = false;
    }
}
