namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;

    public sealed class ListMaterializer : Materializer
    {
        MaterializedEnumerable<T> Materializer.Materialize<T>(
            IEnumerable<T> source)
        {
            return new ListMaterializedEnumerable<T>(source);
        }
    }
}
