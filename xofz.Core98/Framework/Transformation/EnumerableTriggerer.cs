namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableTriggerer
    {
        public IEnumerable<T> Trigger<T>(
            IEnumerable<T> source,
            ICollection<long> triggerIndices,
            Do<T> trigger)
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

            const byte zero = 0;
            long currentIndex = zero;
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