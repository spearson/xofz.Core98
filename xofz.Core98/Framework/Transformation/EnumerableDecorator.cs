namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableDecorator
    {
        public virtual IEnumerable<T> Decorate<T>(
            IEnumerable<T> source,
            Do<T> action)
        {
            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                action?.Invoke(item);
                yield return item;
            }
        }
    }
}
