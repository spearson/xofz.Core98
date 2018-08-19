namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableFrontBackLoader
    {
        public virtual IEnumerable<T> FrontLoad<T>(
            IEnumerable<T> source,
            params T[] frontItems)
        {
            if (frontItems == null)
            {
                goto provideSource;
            }

            foreach (var frontItem in frontItems)
            {
                yield return frontItem;
            }

            provideSource:
            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return item;
            }
        }

        public virtual IEnumerable<T> BackLoad<T>(
            IEnumerable<T> source,
            params T[] backItems)
        {
            if (source == null)
            {
                goto provideBackItems;
            }

            foreach (var item in source)
            {
                yield return item;
            }

            provideBackItems:
            if (backItems == null)
            {
                yield break;
            }

            foreach (var backItem in backItems)
            {
                yield return backItem;
            }
        }
    }
}
