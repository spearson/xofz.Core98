namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class EnumerableSplitter
    {
        public virtual Lot<T>[] Split<T>(
            IEnumerable<T> finiteSource,
            int splits)
        {
            if (finiteSource == null)
            {
                return new Lot<T>[0];
            }

            if (splits < 2)
            {
                return new Lot<T>[]
                {
                    new LinkedListLot<T>(finiteSource)
                };
            }

            Lot<T>[] array = new Lot<T>[splits];
            for (var i = 0; i < splits; ++i)
            {
                array[i] = new LinkedListLot<T>();
            }

            var enumerator = finiteSource.GetEnumerator();
            if (enumerator == null)
            {
                return array;
            }

            LinkedListLot<T> currentLL;
            if (enumerator.MoveNext())
            {
                currentLL = array[0] as LinkedListLot<T>;
                currentLL?.AddLast(enumerator.Current);
            }
            
            var zeroFilled = true;
            while (enumerator.MoveNext())
            {
                for (var i = 0; i < splits; ++i)
                {
                    if (zeroFilled && i == 0)
                    {
                        continue;
                    }

                    zeroFilled = false;
                    currentLL = array[i] as LinkedListLot<T>;
                    currentLL?.AddLast(enumerator.Current);
                    if (i < splits - 1)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                    }
                }

                zeroFilled = false;
            }

            enumerator.Dispose();

            return array;
        }
    }
}
