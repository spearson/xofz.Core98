namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableFluxer
    {
        public EnumerableFluxer()
            : this(new Random())
        {
        }

        protected EnumerableFluxer(
            Random rng)
        {
            this.rng = rng;
        }

        public virtual IEnumerable<ICollection<T>> Flux<T>(
            IEnumerable<T> source,
            int min,
            int max)
        {
            if (source == null)
            {
                yield break;
            }

            if (min < 1)
            {
                yield break;
            }

            if (min > max)
            {
                yield break;
            }

            var maxExclusive = max + 1;
            if (max == int.MaxValue)
            {
                maxExclusive = max;
            }

            using (var e = source.GetEnumerator())
            {
                var indexer = 0;
                T nextItem = default;

                chunk:
                ICollection<T> currentChunk = new LinkedList<T>();
                var nextChunkSize = this.rng.Next(min, maxExclusive);
                if (indexer > 0)
                {
                    currentChunk.Add(nextItem);
                }

                while (indexer < nextChunkSize && e.MoveNext())
                {
                    currentChunk.Add(e.Current);
                    ++indexer;
                }

                yield return currentChunk;
                if (!e.MoveNext())
                {
                    yield break;
                }

                nextItem = e.Current;
                indexer = 1;
                goto chunk;
            }
        }

        protected readonly Random rng;
    }
}
