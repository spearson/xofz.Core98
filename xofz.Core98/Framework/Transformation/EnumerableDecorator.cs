namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableDecorator
    {
        public virtual IEnumerable<T> Decorate<T>(
            IEnumerable<T> source,
            Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }
    }
}
