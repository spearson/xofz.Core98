namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableDisplacer
    {
        public virtual IEnumerable<T> Displace<T>(
            IEnumerable<T> source, 
            long displaceCount)
        {
            if (source == null)
            {
                yield break;
            }

            if (displaceCount < 1)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            using (var e = source.GetEnumerator())
            {
                ICollection<T> displacedItems = new LinkedList<T>();
                long counter = 0;
                while (counter < displaceCount)
                {
                    ++counter;

                    e.MoveNext();
                    displacedItems.Add(e.Current);
                }

                counter = 0;
                while (counter < displaceCount)
                {
                    e.MoveNext();
                    ++counter;

                    yield return e.Current;
                }

                foreach (var displacedItem in displacedItems)
                {
                    yield return displacedItem;
                }

                while (e.MoveNext())
                {
                    yield return e.Current;
                }
            }
        }
    }
}
