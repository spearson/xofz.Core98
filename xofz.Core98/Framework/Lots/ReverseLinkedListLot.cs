namespace xofz.Framework.Lots
{
    public class ReverseLinkedListLot<T>
        : XLinkedListLot<T>
    {
        public ReverseLinkedListLot()
            : this(new ReverseLinkedList<T>())
        {
        }

        public ReverseLinkedListLot(
            ReverseLinkedList<T> rll)
            : base(rll)
        {
        }
    }
}