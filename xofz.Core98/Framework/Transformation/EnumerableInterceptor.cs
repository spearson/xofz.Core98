namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableInterceptor
    {
        public virtual IEnumerable<T> Intercept<T>(
            IEnumerable<T> source, 
            T interception, 
            int interceptionPoint)
        {
            var counter = 0;
            foreach (var item in source)
            {
                yield return item;
                ++counter;
                if (counter == interceptionPoint)
                {
                    yield return interception;
                }
            }
        }

        public virtual IEnumerable<T> Intercept<T>(
            IEnumerable<T> source,
            MaterializedEnumerable<T> interception,
            int interceptionPoint)
        {
            var counter = 0;
            foreach (var item in source)
            {
                yield return item;
                ++counter;
                if (counter != interceptionPoint)
                {
                    continue;
                }

                foreach (var intercept in interception)
                {
                    yield return intercept;
                }
            }
        }
    }
}
