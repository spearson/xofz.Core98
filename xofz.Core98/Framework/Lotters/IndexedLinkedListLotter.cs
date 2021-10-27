namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class IndexedLinkedListLotter
        : LotterV3
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new IndexedLinkedListLot<T>(
                IndexedLinkedList<T>.CreateIndexed(
                    finiteSource));
        }

        ICollection<T> LotterV2.Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return IndexedLinkedList<T>.CreateIndexed(
                finiteSource);
        }

        GetArray<T> LotterV3.Index<T>(
            IEnumerable<T> finiteSource)
        {
            return new IndexedLinkedListLot<T>(
                IndexedLinkedList<T>.CreateIndexed(
                    finiteSource));
        }
    }
}
