namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableDisplacer
    {
        public virtual IEnumerable<T> Displace<T>(
            IEnumerable<T> source, 
            int displaceCount)
        {
            var enumerator = source.GetEnumerator();
            var counter = 0;
            var displacedItems = new LinkedList<T>();
            while (counter < displaceCount)
            {
                enumerator.MoveNext();
                ++counter;

                displacedItems.AddLast(enumerator.Current);
            }

            counter = 0;
            while (counter < displaceCount)
            {
                enumerator.MoveNext();
                ++counter;

                yield return enumerator.Current;
            }

            foreach (var di in displacedItems)
            {
                yield return di;
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            enumerator.Dispose();
        }
    }
}
