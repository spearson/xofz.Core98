namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using Materialization;

    public class EnumerableConnector
    {
        public virtual MaterializedEnumerable<T> Connect<T>(
            IEnumerable<T>[] sources)
        {
            var result = new LinkedList<T>();
            var enumerators = new IEnumerator<T>[sources.Length];

            for (var i = 0; i < sources.Length; ++i)
            {
                enumerators[i] = sources[i].GetEnumerator();
                enumerators[i].MoveNext();
                result.AddLast(enumerators[i].Current);
                while (enumerators[i].MoveNext())
                {
                    result.AddLast(enumerators[i].Current);
                }
            }

            return new LinkedListMaterializedEnumerable<T>(result);
        }
    }
}
