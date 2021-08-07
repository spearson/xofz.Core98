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
                injections?.Length ?? minusOne);
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
                injections?.Count ?? minusOne);
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
                injections?.Count ?? minusOne);
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

            if (nullInjections || injectionsCount < one || injectionIndices == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long currentItemIndex = zero, currentInjectionIndex = zero;
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

        protected const byte
            zero = 0,
            one = 1;
        protected const short
            minusOne = -1;
    }
}
