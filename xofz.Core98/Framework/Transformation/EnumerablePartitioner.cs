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

            const byte
                zero = 0,
                one = 1;
            if (partitionSize < one)
            {
                yield break;
            }

            using (var e = source.GetEnumerator())
            {
                int indexer = zero;
                T nextItem = default;

                partition:
                ICollection<T> currentPartition = new XLinkedList<T>();
                if (indexer > zero)
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
                indexer = one;
                goto partition;
            }
        }
    }
}