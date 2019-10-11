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

            if (kickPoint < 1)
            {
                yield break;
            }

            long kickCounter = 0;
            foreach (var item in source)
            {
                ++kickCounter;
                if (kickCounter == kickPoint)
                {
                    kickCounter = 0;
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

            long kickCounter = 0;
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
    }
}
