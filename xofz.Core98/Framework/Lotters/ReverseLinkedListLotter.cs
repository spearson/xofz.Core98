namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class ReverseLinkedListLotter
        : LotterV2
    {
        ICollection<T> LotterV2.Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return ReverseLinkedList<T>.CreateReverse(
                finiteSource);
        }

        Lot<T> Lotter.Materialize<T>(IEnumerable<T> finiteSource)
        {
            return new ReverseLinkedListLot<T>(
                ReverseLinkedList<T>.CreateReverse(
                    finiteSource));
        }
    }
}
