namespace xofz
{
    using System.Collections.Generic;

    public interface LotterV2
        : Lotter
    {
        ICollection<T> Collect<T>(
            IEnumerable<T> finiteSource);
    }
}
