namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableBreaker
    {
        public virtual IEnumerable<T> AddBreak<T>(
            IEnumerable<T> source, 
            Gen<T, bool> breakCondition)
        {
            foreach (var item in source)
            {
                if (breakCondition(item))
                {
                    yield break;
                }

                yield return item;
            }
        }

        public virtual IEnumerable<T> AddBreak<T>(
            IEnumerable<T> source, 
            params Gen<T, bool>[] breakConditions)
        {
            foreach (var item in source)
            {
                foreach (var breakCondition in breakConditions)
                {
                    if (breakCondition(item))
                    {
                        yield break;
                    }
                }

                yield return item;
            }
        }
    }
}
