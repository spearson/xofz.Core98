﻿namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableDragger
    {
        public virtual IEnumerable<T> Drag<T>(
            IEnumerable<T> source,
            Lot<int> dragLengths)
        {
            if (source == null)
            {
                yield break;
            }

            if (dragLengths == null)
            {
                foreach (var item in source)
                {
                    yield return item;
                }

                yield break;
            }

            const byte zero = 0;
            using (var e = source.GetEnumerator())
            {
                foreach (var length in dragLengths)
                {
                    e.MoveNext();
                    for (int i = zero; i < length; ++i)
                    {
                        yield return e.Current;
                    }
                }

                while (e.MoveNext())
                {
                    yield return e.Current;
                }
            }
        }
    }
}