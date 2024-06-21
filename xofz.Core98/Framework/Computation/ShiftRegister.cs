namespace xofz.Framework.Computation
{
    public class ShiftRegister
    {
        public ShiftRegister(
            int capacity)
        {
            this.capacity = capacity;
            this.bitLinkedList = new XLinkedList<bool>();
        }

        public virtual bool this[int index] => this.currentArray[index];

        public virtual int CurrentSize => this.bitLinkedList.Count;

        public virtual void ShiftLeft(
            bool input)
        {
            var ll = this.bitLinkedList;
            ll.AddTail(input);

            while (ll.Count > this.capacity)
            {
                ll.RemoveHead();
            }

            var array = new bool[ll.Count];
            ll.CopyTo(array, zero);

            this.setCurrentArray(array);
        }

        public virtual void ShiftRight(
            bool input)
        {
            var ll = this.bitLinkedList;
            ll.AddHead(input);

            while (ll.Count > this.capacity)
            {
                ll.RemoveTail();
            }

            var array = new bool[ll.Count];
            ll.CopyTo(array, zero);

            this.setCurrentArray(array);
        }

        protected virtual void setCurrentArray(
            bool[] currentArray)
        {
            this.currentArray = currentArray;
        }

        protected bool[] currentArray;
        protected readonly int capacity;
        protected readonly XLinkedList<bool> bitLinkedList;
        protected const byte zero = 0;
    }
}