namespace xofz.Framework.Lotters
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public sealed class ArrayLotter : Lotter
    {
        Lot<T> Lotter.Materialize<T>(
            IEnumerable<T> source)
        {
            var ll = new LinkedList<T>(source);
            var array = new T[ll.Count];

            ll.CopyTo(array, 0);

            return new ArrayLot<T>(
                array);
        }
    }
}
