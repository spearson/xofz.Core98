namespace xofz
{
    using System.Collections.Generic;

    public interface Materializer
    {
        MaterializedEnumerable<T> Materialize<T>(IEnumerable<T> source);
    }
}
