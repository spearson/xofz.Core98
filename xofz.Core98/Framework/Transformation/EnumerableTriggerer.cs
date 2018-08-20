namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableTriggerer
    {
        public IEnumerable<T> Trigger<T>(
            IEnumerable<T> source,
            ICollection<long> triggerIndices,
            Action<T> trigger)
        {
            if (source == null)
            {
                yield break;
            }

            if (triggerIndices == null || trigger == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long currentIndex = 0;
            foreach (var item in source)
            {                
                if (triggerIndices.Contains(currentIndex))
                {
                    trigger(item);
                }

                ++currentIndex;
                yield return item;
            }
        }
    }
}
