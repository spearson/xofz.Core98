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

            const byte
                zero = 0,
                one = 1;
            if (displaceCount < one)
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
                long counter = zero;
                while (counter < displaceCount)
                {
                    ++counter;

                    e.MoveNext();
                    displacedItems.Add(e.Current);
                }

                counter = zero;
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
