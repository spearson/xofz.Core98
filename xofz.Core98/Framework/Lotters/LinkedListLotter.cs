namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class LinkedListLotter 
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            return new LinkedListLot<T>(
                source);
        }

        public ICollection<T> Collect<T>(
            IEnumerable<T> source)
        {
            return new List<T>(
                source);
        }
    }
}
