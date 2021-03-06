namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableSpreader
    {
        public virtual IEnumerable<T> Spread<T>(
            IEnumerable<T> source,
            int spread)
        {
            if (source == null)
            {
                yield break;
            }

            if (spread < 1)
            {
                yield break;
            }

            var counter = 0;
            foreach (var item in source)
            {
                while (counter < spread)
                {
                    yield return item;
                    ++counter;
                }

                counter = 0;
            }
        }
    }
}
