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
            Lot<long> injectionIndices,
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

            if (nullInjections || injectionsCount < 1 || injectionIndices == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long currentItemIndex = 0, currentInjectionIndex = 0;
            var injectionsE = injections.GetEnumerator();
            foreach (var item in source)
            {
                if (currentInjectionIndex < injectionsCount)
                {
                    foreach (var injectionIndex in injectionIndices)
                    {
                        if (injectionIndex != currentItemIndex)
                        {
                            continue;
                        }

                        injectionsE.MoveNext();
                        yield return injectionsE.Current;

                        ++currentInjectionIndex;
                        if (currentInjectionIndex >= injectionsCount)
                        {
                            break;
                        }
                    }
                }

                yield return item;

                checked
                {
                    ++currentItemIndex;
                }
            }

            injectionsE.Dispose();
        }
    }
}
