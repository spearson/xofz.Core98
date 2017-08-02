namespace xofz
{
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public interface MaterializedEnumerable<T> : IEnumerable<T>
    {
        long Count { get; }
    }

    public static class MaterializedEnumerable
    {
        public static MaterializedEnumerable<T> Empty<T>()
        {
            return new LinkedListMaterializedEnumerable<T>();
        }
    }
}