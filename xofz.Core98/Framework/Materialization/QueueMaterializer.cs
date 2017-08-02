namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;

    public sealed class QueueMaterializer : Materializer
    {
        MaterializedEnumerable<T> Materializer.Materialize<T>(IEnumerable<T> source)
        {
            return new QueueMaterializedEnumerable<T>(
                new Queue<T>(source));
        }
    }
}
