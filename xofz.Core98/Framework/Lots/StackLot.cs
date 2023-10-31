namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class StackLot<T> 
        : Lot<T>
    {
        public StackLot()
        {
            this.stack = new Stack<T>();
        }

        public StackLot(
            long capacity)
        {
            this.stack = new Stack<T>(
                (int)capacity);
        }

        public StackLot(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                this.stack = new Stack<T>();
                return;
            }

            if (finiteSource is Stack<T> s)
            {
                this.stack = s;
                return;
            }

            this.stack = new Stack<T>(finiteSource);
        }

        public StackLot(
            IEnumerator<T> finiteEnumerator)
        {
            var s = new Stack<T>();
            if (finiteEnumerator == null)
            {
                goto assign;
            }

            while (finiteEnumerator.MoveNext())
            {
                s.Push(finiteEnumerator.Current);
            }

            assign:
            this.stack = s;
        }

        public virtual long Count => this.stack?.Count
                                     ?? nOne;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.stack?.GetEnumerator()
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
            return this.stack?.ToArray();
        }

        public virtual T Peek()
        {
            var s = this.stack;
            return s == null 
                ? default 
                : s.Peek();
        }

        public virtual T Pop()
        {
            var s = this.stack;
            return s == null 
                ? default 
                : s.Pop();
        }

        public virtual bool Contains(
            T item)
        {
            return EnumerableHelpers.Contains(
                this.stack,
                item);
        }

        public virtual void Clear()
        {
            this.stack?.Clear();
        }

        public virtual void CopyTo(
            T[] array)
        {
            const byte zero = 0;
            this.stack?.CopyTo(
                array, 
                zero);
        }

        public virtual void Push(
            T item)
        {
            this.stack?.Push(
                item);
        }

        public virtual void TrimExcess()
        {
            this.stack?.TrimExcess();
        }

        protected readonly Stack<T> stack;
        protected const short nOne = -1;
        protected const bool falsity = false;
    }
}
