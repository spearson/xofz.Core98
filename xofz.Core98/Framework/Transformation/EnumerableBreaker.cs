namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableBreaker
    {
        public virtual IEnumerable<T> AddBreak<T>(
            IEnumerable<T> source, 
            Gen<T, bool> breakCondition)
        {
            if (source == null)
            {
                yield break;
            }

            var bcIsNull = breakCondition == null;
            foreach (var item in source)
            {
                if (!bcIsNull && breakCondition(item))
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
            if (source == null)
            {
                yield break;
            }

            var bcsAreNull = breakConditions == null;
            foreach (var item in source)
            {
                if (bcsAreNull)
                {
                    yield return item;
                    continue;
                }

                foreach (var breakCondition in breakConditions)
                {
                    if (breakCondition == null)
                    {
                        continue;
                    }

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
