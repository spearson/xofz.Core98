namespace xofz.Framework.Materialization
{
    using System.Collections.Generic;

    public sealed class ArrayMaterializer : Materializer
    {
        public MaterializedEnumerable<T> Materialize<T>(
            IEnumerable<T> source)
        {
            var ll = new LinkedList<T>(source);
            var array = new T[ll.Count];

            ll.CopyTo(array, 0);

            return new ArrayMaterializedEnumerable<T>(
                array);
        }
    }
}
