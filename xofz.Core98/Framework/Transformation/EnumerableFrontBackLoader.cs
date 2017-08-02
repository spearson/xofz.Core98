namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableFrontBackLoader
    {
        public virtual IEnumerable<T> FrontLoad<T>(IEnumerable<T> source, params T[] frontItems)
        {
            foreach (var frontItem in frontItems)
            {
                yield return frontItem;
            }

            foreach (var item in source)
            {
                yield return item;
            }
        }

        public virtual IEnumerable<T> BackLoad<T>(IEnumerable<T> source, params T[] backItems)
        {
            foreach (var item in source)
            {
                yield return item;
            }

            foreach (var backItem in backItems)
            {
                yield return backItem;
            }
        }
    }
}
