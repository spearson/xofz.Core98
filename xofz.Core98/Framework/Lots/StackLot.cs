namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class StackLot<T> : Lot<T>
    {
        public StackLot()
        {
            this.stack = new Stack<T>();
        }

        public StackLot(
            int capacity)
        {
            this.stack = new Stack<T>(capacity);
        }

        public StackLot(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                this.stack = new Stack<T>();
                return;
            }

            var s = finiteSource as Stack<T>;
            if (s != null)
            {
                this.stack = s;
                return;
            }

            this.stack = new Stack<T>(finiteSource);
        }

        public virtual long Count => this.stack.Count;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<T> source = this;

            return source.GetEnumerator();
        }

        public virtual T[] ToArray()
        {
            return this.stack.ToArray();
        }

        public virtual T Peek()
        {
            return this.stack.Peek();
        }

        public virtual T Pop()
        {
            return this.stack.Pop();
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
            this.stack.Clear();
        }

        public virtual void CopyTo(
            T[] array)
        {
            this.stack.CopyTo(array, 0);
        }

        public virtual void Push(
            T item)
        {
            this.stack.Push(item);
        }

        public virtual void TrimExcess()
        {
            this.stack.TrimExcess();
        }

        protected readonly Stack<T> stack;
    }
}
