namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class ArrayLotter 
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> finiteSource)
        {
            return new ArrayLot<T>(
                EnumerableHelpers.ToArray(
                    finiteSource));
        }

        ICollection<T> LotterV2.Collect<T>(
            IEnumerable<T> finiteSource)
        {
            return EnumerableHelpers.ToArray(
                finiteSource);
        }
    }
}
