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
                T nextCurrent = default;
                byte indexer = 0;

                addAndReturnChunk:
                var nextChunkSize = this.rng.Next(min, maxExclusive);
                var ll = new LinkedList<T>();
                if (indexer > 0)
                {
                    ll.AddLast(nextCurrent);
                }

                while (indexer < nextChunkSize && e.MoveNext())
                {
                    ll.AddLast(e.Current);
                    ++indexer;
                }

                yield return ll;
                if (!e.MoveNext())
                {
                    yield break;
                }

                nextCurrent = e.Current;
                indexer = 1;
                goto addAndReturnChunk;
            }
        }

        protected readonly Random rng;
    }
}
