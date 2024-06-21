namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableKicker
    {
        public virtual IEnumerable<T> Kick<T>(
            IEnumerable<T> source,
            long kickPoint)
        {
            if (source == null)
            {
                yield break;
            }

            const byte one = 1;
            if (kickPoint < one)
            {
                yield break;
            }

            long kickCounter = zero;
            foreach (var item in source)
            {
                ++kickCounter;
                if (kickCounter == kickPoint)
                {
                    kickCounter = zero;
                    continue;
                }

                yield return item;
            }
        }

        public virtual IEnumerable<T> Kick<T>(
            IEnumerable<T> source,
            ICollection<long> kickPoints)
        {
            if (source == null)
            {
                yield break;
            }

            if (kickPoints == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long kickCounter = zero;
            foreach (var item in source)
            {
                checked
                {
                    ++kickCounter;
                }

                var shouldKick = false;
                foreach (var kickPoint in kickPoints)
                {
                    if (kickPoint == kickCounter)
                    {
                        shouldKick = true;
                        break;
                    }
                }

                if (shouldKick)
                {
                    continue;
                }

                yield return item;
            }
        }

        protected const byte
            zero = 0;
    }
}