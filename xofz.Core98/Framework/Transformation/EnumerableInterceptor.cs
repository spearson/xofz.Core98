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
                if (interceptionPoint == 0)
                {
                    yield return interception;
                }

                yield break;
            }

            long counter = 0;
            foreach (var item in source)
            {
                if (counter == interceptionPoint)
                {
                    yield return interception;
                }

                ++counter;
                yield return item;
            }
        }

        public virtual IEnumerable<T> Intercept<T>(
            IEnumerable<T> source,
            ICollection<T> interception,
            long interceptionPoint)
        {
            long counter = 0;
            var nullInterception = interception == null;
            if (source == null)
            {
                if (interceptionPoint == 0)
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
                if (counter == interceptionPoint)
                {
                    foreach (var intercept in interception)
                    {
                        yield return intercept;
                    }
                }

                ++counter;
                yield return item;
            }
        }
    }
}
