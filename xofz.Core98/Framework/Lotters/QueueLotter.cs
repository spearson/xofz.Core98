namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class QueueLotter : Lotter
    {
        Lot<T> Lotter.Materialize<T>(IEnumerable<T> source)
        {
            return new QueueLot<T>(
                source);
        }
    }
}
