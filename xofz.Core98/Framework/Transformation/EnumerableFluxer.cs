namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableFluxer
    {
        public EnumerableFluxer()
            : this(null)
        {
        }

        protected EnumerableFluxer(
            System.Random rng)
        {
            this.rng = rng
                ?? new System.Random();
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

            const byte
                zero = 0,
                one = 1;
            if (min < one)
            {
                yield break;
            }

            if (min > max)
            {
                yield break;
            }

            var maxExclusive = max + one;
            if (max == int.MaxValue)
            {
                maxExclusive = max;
            }

            using (var e = source.GetEnumerator())
            {
                int indexer = zero;
                T nextItem = default;

                chunk:
                ICollection<T> currentChunk = new XLinkedList<T>();
                if (indexer > zero)
                {
                    currentChunk.Add(nextItem);
                }

                var nextChunkSize = this.rng.Next(min, maxExclusive);
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
                indexer = one;
                goto chunk;
            }
        }

        protected readonly System.Random rng;
    }
}
