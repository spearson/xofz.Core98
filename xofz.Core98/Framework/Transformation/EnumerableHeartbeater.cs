namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableHeartbeater
    {
        public virtual IEnumerable<T> AddHeartbeat<T>(
            IEnumerable<T> source, 
            T heartbeat, 
            int interval)
        {
            var nullSource = source == null;
            if (nullSource)
            {
                yield break;
            }

            if (interval < 1)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
                yield break;
            }

            var counter = 0;
            foreach (var item in source)
            {
                yield return item;
                ++counter;
                if (counter == interval)
                {
                    counter = 0;
                    yield return heartbeat;
                }
            }
        }
    }
}
