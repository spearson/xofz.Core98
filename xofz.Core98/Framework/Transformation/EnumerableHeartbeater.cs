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
            if (source == null)
            {
                yield break;
            }

            const byte 
                zero = 0,
                one = 1;
            if (interval < one)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            int counter = zero;
            foreach (var item in source)
            {
                yield return item;
                ++counter;
                if (counter == interval)
                {
                    yield return heartbeat;
                    counter = zero;
                }
            }
        }
    }
}
