namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class XLinkedListLotter
        : LotterV2
    {
        public Lot<T> Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new XLinkedListLot<T>(
                XLinkedList<T>.Create(finiteSource));
        }

        public ICollection<T> Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return XLinkedList<T>.Create(
                finiteSource);
        }
    }
}
