namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;

    public sealed class StackMaterializer : Materializer
    {
        MaterializedEnumerable<T> Materializer.Materialize<T>(IEnumerable<T> source)
        {
            return new StackMaterializedEnumerable<T>(
                new Stack<T>(source));
        }
    }
}
