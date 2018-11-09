namespace xofz
{
    using System.Collections.Generic;

    public interface Lotter
    {
        Lot<T> Materialize<T>(IEnumerable<T> finiteSource);
    }
}
