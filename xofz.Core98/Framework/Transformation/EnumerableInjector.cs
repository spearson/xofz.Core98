namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableInjector
    {
        public virtual IEnumerable<T> Inject<T>(
            IEnumerable<T> source,
            Lot<long> injectionPoints,
            params T[] injections)
        {
            return this.injectProtected(
                source,
                injectionPoints,
                injections,
                injections?.Length ?? -1);
        }

        public virtual IEnumerable<T> Inject<T>(
            IEnumerable<T> source,
            Lot<long> injectionPoints,
            ICollection<T> injections)
        {
            return this.injectProtected(
                source,
                injectionPoints,
                injections,
                injections?.Count ?? -1);
        }

        public virtual IEnumerable<T> Inject<T>(
            IEnumerable<T> source,
            Lot<long> injectionPoints,
            Lot<T> injections)
        {
            return this.injectProtected(
                source,
                injectionPoints,
                injections,
                injections?.Count ?? -1);
        }

        protected virtual IEnumerable<T> injectProtected<T>(
            IEnumerable<T> source,
            Lot<long> injectionPoints,
            IEnumerable<T> injections,
            long injectionsCount)
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

            if (injectionsCount < 1)
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
            long currentInjectionIndex = 0;
            var injectionsE = injections.GetEnumerator();
            foreach (var item in source)
            {
                if (currentInjectionIndex < injectionsCount)
                {
                    foreach (var injectionPoint in injectionPoints)
                    {
                        if (injectionPoint != counter)
                        {
                            continue;
                        }

                        if (currentInjectionIndex >= injectionsCount)
                        {
                            break;
                        }

                        injectionsE.MoveNext();
                        yield return injectionsE.Current;
                        ++currentInjectionIndex;
                    }
                }

                checked
                {
                    ++counter;
                }

                yield return item;
            }

            injectionsE.Dispose();
        }
    }
}
