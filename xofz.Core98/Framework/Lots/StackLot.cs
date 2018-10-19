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

        public StackLot(int capacity)
        {
            this.stack = new Stack<T>(capacity);
        }

        public StackLot(IEnumerable<T> source)
        {
            this.stack = new Stack<T>(source);
        }

        public StackLot(Stack<T> stack)
        {
            this.stack = stack;
        }

        long Lot<T>.Count => this.stack.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            return this.stack.ToArray();
        }

        public T Peek()
        {
            return this.stack.Peek();
        }

        public T Pop()
        {
            return this.stack.Pop();
        }

        public bool Contains(T item)
        {
            return this.stack.Contains(item);
        }

        public void Clear()
        {
            this.stack.Clear();
        }

        public void CopyTo(T[] array)
        {
            this.stack.CopyTo(array, 0);
        }

        public void Push(T item)
        {
            this.stack.Push(item);
        }

        public void TrimExcess()
        {
            this.stack.TrimExcess();
        }

        protected readonly Stack<T> stack;
    }
}
