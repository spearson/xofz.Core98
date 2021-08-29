namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class ListLotter 
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new ListLot<T>(
                finiteSource);
        }

        public ICollection<T> Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return new List<T>(
                finiteSource);
        }
    }
}
