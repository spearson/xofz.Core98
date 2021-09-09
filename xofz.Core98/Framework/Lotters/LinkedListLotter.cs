namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class LinkedListLotter 
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new LinkedListLot<T>(
                finiteSource);
        }

        public ICollection<T> Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return new LinkedList<T>(
                finiteSource);
        }
    }
}
