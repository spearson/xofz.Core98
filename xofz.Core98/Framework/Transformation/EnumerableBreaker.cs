namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableBreaker
    {
        public virtual IEnumerable<T> AddBreak<T>(
            IEnumerable<T> source, 
            Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
                else
                {
                    yield break;
                }
            }
        }

        public virtual IEnumerable<T> AddBreak<T>(
            IEnumerable<T> source, 
            params Func<T, bool>[] predicates)
        {
            foreach (var item in source)
            {
                foreach (var predicate in predicates)
                {
                    if (!predicate(item))
                    {
                        yield break;
                    }
                }

                yield return item;
            }
        }
    }
}
