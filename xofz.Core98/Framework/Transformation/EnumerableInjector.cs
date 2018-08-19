namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableInjector
    {
        public virtual IEnumerable<T> Inject<T>(
            IEnumerable<T> source,
            ICollection<long> injectionPoints,
            params T[] injections)
        {
            var nullInjections = injections == null;
            if (source == null)
            {
                if (nullInjections)
                {
                    yield break;
                }

                foreach (var injection in injections)
                {
                    yield return injection;
                }

                yield break;
            }

            if (nullInjections)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            if (injectionPoints == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long counter = 0;
            long index = 0;
            var il = injections.Length;
            foreach (var item in source)
            {                
                foreach (var ip in injectionPoints)
                {
                    if (ip == counter && index < il)
                    {
                        yield return injections[index];
                        ++index;
                    }
                }

                ++counter;
                yield return item;
            }
        }
    }
}
