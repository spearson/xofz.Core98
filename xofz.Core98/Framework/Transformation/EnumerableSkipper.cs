namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Materialization;

    public class EnumerableSkipper
    {
        public virtual IEnumerable<T> Skip<T>(
            IEnumerable<T> source, 
            int skipPoint)
        {
            var enumerator = source.GetEnumerator();
            var counter = 0;
            while (enumerator.MoveNext())
            {
                if (counter == skipPoint)
                {
                    counter = 0;
                    yield return enumerator.Current;
                }

                ++counter;
            }

            enumerator.Dispose();
        }

        public virtual MaterializedEnumerable<T> SkipThrough<T>(
            IEnumerable<T> finiteSource, 
            int skipPoint)
        {
            var ll = new LinkedList<T>(finiteSource);
            var result = new List<T>();
            for (var i = 0; i < skipPoint; ++i)
            {
                result.AddRange(this.Skip(ll, skipPoint));
                ll.RemoveFirst();
            }

            return new ListMaterializedEnumerable<T>(result);
        }
    }
}
