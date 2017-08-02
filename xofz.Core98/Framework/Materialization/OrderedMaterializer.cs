namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;

    public sealed class OrderedMaterializer : Materializer
    {
        MaterializedEnumerable<T> Materializer.Materialize<T>(IEnumerable<T> source)
        {
            return new OrderedMaterializedEnumerable<T>(
                new List<T>(source));
        }
    }
}
