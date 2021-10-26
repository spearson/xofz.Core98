namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class XLinkedListLotter
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new XLinkedListLot<T>(
                XLinkedList<T>.Create(finiteSource));
        }

        ICollection<T> LotterV2.Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return XLinkedList<T>.Create(
                finiteSource);
        }
    }
}
