namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableKicker
    {
        public virtual IEnumerable<T> Kick<T>(
            IEnumerable<T> source, 
            int kickPoint)
        {
            var kickCounter = 0;
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
            MaterializedEnumerable<int> kickPoints)
        {
            var kickCounter = 0;
            foreach (var item in source)
            {
                ++kickCounter;
                var shouldKick = false;
                foreach (var kp in kickPoints)
                {
                    if (kp == kickCounter)
                    {
                        shouldKick = true;
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
