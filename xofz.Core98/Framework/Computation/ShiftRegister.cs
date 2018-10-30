namespace xofz.Framework.Computation
{
    using System.Collections.Generic;

    public class ShiftRegister
    {
        public ShiftRegister(int capacity)
        {
            this.capacity = capacity;
            this.shiftList = new LinkedList<bool>();
        }

        public bool this[int index] => this.currentArray[index];

        public int CurrentSize => this.shiftList.Count;

        public void ShiftLeft(bool input)
        {
            var ll = this.shiftList;
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

        public void ShiftRight(bool input)
        {
            var ll = this.shiftList;
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

        private void setCurrentArray(bool[] currentArray)
        {
            this.currentArray = currentArray;
        }

        private bool[] currentArray;
        private readonly int capacity;
        private readonly LinkedList<bool> shiftList;
    }
}
