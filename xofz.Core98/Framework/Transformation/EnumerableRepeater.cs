namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using Materialization;

    public class EnumerableRepeater
    {
        public virtual MaterializedEnumerable<T> Repeat<T>(IEnumerable<T> source, int times)
        {
            var linkedList = new LinkedList<T>(source);
            var list = new List<T>();
            for (var i = 0; i < times; ++i)
            {
                list.AddRange(linkedList);
            }

            return new OrderedMaterializedEnumerable<T>(list);
        }
    }
}
