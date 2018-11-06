namespace xofz.Framework.Computation
{
    using System.Collections.Generic;

    public class ShiftRegister
    {
        public ShiftRegister(int capacity)
        {
            this.capacity = capacity;
            this.bitLinkedList = new LinkedList<bool>();
        }

        public virtual bool this[int index] => this.currentArray[index];

        public virtual int CurrentSize => this.bitLinkedList.Count;

        public virtual void ShiftLeft(bool input)
        {
            var ll = this.bitLinkedList;
            ll.AddLast(input);

            while (ll.Count > this.capacity)
            {
                ll.RemoveFirst();
            }

            var array = new bool[ll.Count];
            var e = ll.GetEnumerator();
            for (var i = 0; i < array.Length; ++i)
            {
                e.MoveNext();
                array[i] = e.Current;
            }

            e.Dispose();
            this.setCurrentArray(array);
        }

        public virtual void ShiftRight(bool input)
        {
            var ll = this.bitLinkedList;
            ll.AddFirst(input);

            while (ll.Count > this.capacity)
            {
                ll.RemoveLast();
            }

            var array = new bool[ll.Count];
            var e = ll.GetEnumerator();
            for (var i = 0; i < array.Length; ++i)
            {
                e.MoveNext();
                array[i] = e.Current;
            }

            e.Dispose();
            this.setCurrentArray(array);
        }

        protected virtual void setCurrentArray(bool[] currentArray)
        {
            this.currentArray = currentArray;
        }

        private bool[] currentArray;
        private readonly int capacity;
        private readonly LinkedList<bool> bitLinkedList;
    }
}
