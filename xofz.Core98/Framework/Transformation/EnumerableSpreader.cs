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

            const byte
                zero = 0,
                one = 1;
            if (spread < one)
            {
                yield break;
            }

            int counter = zero;
            foreach (var item in source)
            {
                while (counter < spread)
                {
                    yield return item;
                    ++counter;
                }

                counter = zero;
            }
        }
    }
}