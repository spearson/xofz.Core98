namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class LinkedListLotter : Lotter
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            return new LinkedListLot<T>(
                new LinkedList<T>(source));
        }
    }
}
