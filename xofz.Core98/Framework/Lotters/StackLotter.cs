namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class StackLotter : Lotter
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            return new StackLot<T>(
                new Stack<T>(source));
        }
    }
}
