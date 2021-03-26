namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class ArrayLotter 
        : LotterV2
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            return new ArrayLot<T>(
                EnumerableHelpers.ToArray(
                    source));
        }

        ICollection<T> LotterV2.Collect<T>(
            IEnumerable<T> source)
        {
            return EnumerableHelpers.ToArray(
                source);
        }
    }
}
