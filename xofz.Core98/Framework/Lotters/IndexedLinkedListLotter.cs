namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class IndexedLinkedListLotter
        : LotterV2
    {
        ICollection<T> LotterV2.Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return IndexedLinkedList<T>.CreateIndexed(
                finiteSource);
        }

        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new IndexedLinkedListLot<T>(
                IndexedLinkedList<T>.CreateIndexed(
                    finiteSource));
        }
    }
}
