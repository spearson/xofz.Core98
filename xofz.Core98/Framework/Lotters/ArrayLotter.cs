namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class ArrayLotter 
        : Lotter
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            return new ArrayLot<T>(
                EnumerableHelpers.ToArray(
                    source));
        }
    }
}
