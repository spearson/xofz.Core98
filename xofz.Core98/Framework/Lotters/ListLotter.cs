namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class ListLotter 
        : LotterV3
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new ListLot<T>(
                finiteSource);
        }

        ICollection<T> LotterV2.Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return new List<T>(
                finiteSource);
        }

        GetArray<T> LotterV3.Index<T>(
            IEnumerable<T> finiteSource)
        {
            return new ListLot<T>(
                finiteSource);
        }
    }
}
