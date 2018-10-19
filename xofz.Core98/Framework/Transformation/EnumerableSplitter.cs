namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableSplitter
    {
        public virtual Lot<T>[] Split<T>(
            IEnumerable<T> source,
            int splits)
        {
            if (source == null)
            {
                return new Lot<T>[0];
            }

            if (splits < 2)
            {
                return new Lot<T>[]
                {
                    new LinkedListLot<T>(source)
                };
            }

            var array = new LinkedListLot<T>[splits];
            for (var i = 0; i < splits; ++i)
            {
                array[i] = new LinkedListLot<T>();
            }

            var enumerator = source.GetEnumerator();
            if (enumerator == null)
            {
                return new Lot<T>[0];
            }

            if (enumerator.MoveNext())
            {
                array[0].AddLast(enumerator.Current);
            }
            
            var zeroFilled = true;
            while (enumerator.MoveNext())
            {
                loop:
                for (var i = 0; i < splits; ++i)
                {
                    if (zeroFilled && i == 0)
                    {
                        continue;
                    }

                    zeroFilled = false;
                    array[i].AddLast(enumerator.Current);
                    if (i < splits - 1)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                    }
                }

                zeroFilled = false;
                if (enumerator.MoveNext())
                {
                    goto loop;
                }
            }

            enumerator.Dispose();

            return array;
        }
    }
}
