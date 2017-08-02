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
            var linkedList = this.shiftList;
            linkedList.AddLast(input);

            if (linkedList.Count > this.capacity)
            {
                linkedList.RemoveFirst();
            }

            var array = new bool[linkedList.Count];
            var e = linkedList.GetEnumerator();
            for (var i = 0; i < array.Length; ++i)
            {
                e.MoveNext();
                array[i] = e.Current;
            }

            this.setCurrentArray(array);
            e.Dispose();
        }

        public void ShiftRight(bool input)
        {
            var linkedList = this.shiftList;
            linkedList.AddFirst(input);

            if (linkedList.Count > this.capacity)
            {
                linkedList.RemoveLast();
            }

            var array = new bool[linkedList.Count];
            var e = linkedList.GetEnumerator();
            for (var i = 0; i < array.Length; ++i)
            {
                e.MoveNext();
                array[i] = e.Current;
            }

            this.setCurrentArray(array);
            e.Dispose();
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
