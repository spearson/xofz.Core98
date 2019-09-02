namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerablePartitioner
    {
        public virtual IEnumerable<ICollection<T>> Partition<T>(
            IEnumerable<T> source,
            int partitionSize)
        {
            if (source == null)
            {
                yield break;
            }

            if (partitionSize < 1)
            {
                yield break;
            }

            using (var e = source.GetEnumerator())
            {
                var indexer = 0;
                T nextItem = default;

                partition:
                ICollection<T> currentPartition = new LinkedList<T>();
                if (indexer > 0)
                {
                    currentPartition.Add(nextItem);
                }

                while (indexer < partitionSize && e.MoveNext())
                {
                    currentPartition.Add(e.Current);
                    ++indexer;
                }

                yield return currentPartition;
                if (!e.MoveNext())
                {
                    yield break;
                }

                nextItem = e.Current;
                indexer = 1;
                goto partition;
            }
        }
    }
}
