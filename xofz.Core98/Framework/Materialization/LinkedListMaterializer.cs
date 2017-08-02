namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;

    public sealed class LinkedListMaterializer : Materializer
    {
        MaterializedEnumerable<T> Materializer.Materialize<T>(IEnumerable<T> source)
        {
            return new LinkedListMaterializedEnumerable<T>(
                new LinkedList<T>(source));
        }
    }
}
