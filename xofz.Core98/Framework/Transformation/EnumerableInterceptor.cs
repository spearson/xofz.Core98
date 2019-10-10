namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableInterceptor
    {
        public virtual IEnumerable<T> Intercept<T>(
            IEnumerable<T> source,
            T interception,
            long interceptionPoint)
        {
            if (source == null)
            {
                if (interceptionPoint < 1)
                {
                    yield return interception;
                }

                yield break;
            }

            long indexer = 0;
            foreach (var item in source)
            {
                if (indexer == interceptionPoint)
                {
                    yield return interception;
                }

                yield return item;

                checked
                {
                    ++indexer;
                }
            }
        }

        public virtual IEnumerable<T> Intercept<T>(
            IEnumerable<T> source,
            ICollection<T> interception,
            long interceptionPoint)
        {
            long indexer = 0;
            var nullInterception = interception == null;
            if (source == null)
            {
                if (interceptionPoint < 1)
                {
                    if (nullInterception)
                    {
                        yield break;
                    }

                    foreach (var intercept in interception)
                    {
                        yield return intercept;
                    }
                }

                yield break;
            }

            if (nullInterception)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            foreach (var item in source)
            {
                if (indexer == interceptionPoint)
                {
                    foreach (var intercept in interception)
                    {
                        yield return intercept;
                    }
                }

                yield return item;

                checked
                {
                    ++indexer;
                }
            }
        }

        public virtual IEnumerable<T> Intercept<T>(
            IEnumerable<T> source,
            IEnumerable<T> interceptions,
            ICollection<long> interceptionPoints)
        {
            if (source == null)
            {
                yield break;
            }

            if (interceptions == null || interceptionPoints == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            long indexer = 0;
            using (var interceptionsEnumerator = interceptions.GetEnumerator())
            {
                foreach (var item in source)
                {
                    if (interceptionPoints.Contains(indexer))
                    {
                        interceptionsEnumerator.MoveNext();
                        yield return interceptionsEnumerator.Current;
                    }

                    yield return item;

                    checked
                    {
                        ++indexer;
                    }
                }
            }
        }
    }
}
