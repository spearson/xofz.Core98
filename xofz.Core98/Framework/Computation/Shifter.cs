namespace xofz.Framework.Computation
{
    using System.Collections;
    using System.Collections.Generic;

    public class Shifter<T> : MaterializedEnumerable<T>
    {
        public Shifter(int capacity)
        {
            this.capacity = capacity;
            this.linkedList = new LinkedList<T>();
        }

        public virtual T this[int index] => this.currentArray[index];

        public virtual int CurrentSize => this.linkedList.Count;

        long MaterializedEnumerable<T>.Count => this.CurrentSize;

        public IEnumerator<T> GetEnumerator()
        {
            return this.linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void ShiftRight(T input)
        {
            var ll = this.linkedList;
            ll.AddFirst(input);

            if (ll.Count > this.capacity)
            {
                ll.RemoveLast();
            }

            var array = new T[ll.Count];
            ll.CopyTo(array, 0);
            this.setCurrentArray(array);
        }

        public virtual void ShiftLeft(T input)
        {
            var ll = this.linkedList;
            ll.AddLast(input);

            if (ll.Count > this.capacity)
            {
                ll.RemoveFirst();
            }

            var array = new T[ll.Count];
            ll.CopyTo(array, 0);
            this.setCurrentArray(array);
        }

        private void setCurrentArray(T[] currentArray)
        {
            this.currentArray = currentArray;
        }

        private T[] currentArray;
        private readonly int capacity;
        private readonly LinkedList<T> linkedList;
    }
}
