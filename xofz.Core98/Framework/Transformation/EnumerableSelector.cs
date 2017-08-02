namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableSelector
    {
        public virtual IEnumerable<T> Select<T>(IEnumerable<T> source, IEnumerable<bool> selectors)
        {
            var e1 = source.GetEnumerator();
            var e2 = selectors.GetEnumerator();
            while (e1.MoveNext())
            {
                e2.MoveNext();
                if (e2.Current)
                {
                    yield return e1.Current;
                }
            }

            e1.Dispose();
            e2.Dispose();
        }
    }
}
