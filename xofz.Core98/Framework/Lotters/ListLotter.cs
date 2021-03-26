namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class ListLotter 
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            return new ListLot<T>(
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
