namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableDragger
    {
        public virtual IEnumerable<T> Drag<T>(
            IEnumerable<T> source,
            MaterializedEnumerable<int> dragLengths)
        {
            var e = source.GetEnumerator();
            foreach (var length in dragLengths)
            {
                e.MoveNext();
                for (var i = 0; i < length; ++i)
                {
                    yield return e.Current;
                }
            }

            while (e.MoveNext())
            {
                yield return e.Current;
            }

            e.Dispose();
        }
    }
}
