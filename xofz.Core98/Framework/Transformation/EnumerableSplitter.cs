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
            return this.SplitV2(
                finiteSource,
                splits);
        }

        public virtual Lot<T>[] SplitV2<T>(
            IEnumerable<T> finiteSource,
            long splitCount)
        {
            const byte
                zero = 0,
                one = 1,
                two = 2;
            const bool
                truth = true,
                falsity = false;
            if (finiteSource == null)
            {
                return new Lot<T>[zero];
            }

            if (splitCount < two)
            {
                return new Lot<T>[]
                {
                    new LinkedListLot<T>(finiteSource)
                };
            }

            var array = new Lot<T>[splitCount];
            for (long splitIndex = zero; splitIndex < splitCount; ++splitIndex)
            {
                array[splitIndex] = new LinkedListLot<T>();
            }

            var enumerator = finiteSource.GetEnumerator();
            if (enumerator == null)
            {
                return array;
            }

            LinkedListLot<T> currentLL;
            if (enumerator.MoveNext())
            {
                currentLL = array[zero] as LinkedListLot<T>;
                currentLL?.AddLast(enumerator.Current);
            }

            var zeroFilled = truth;
            while (enumerator.MoveNext())
            {
                for (long splitIndex = zero; splitIndex < splitCount; ++splitIndex)
                {
                    if (zeroFilled && splitIndex == zero)
                    {
                        continue;
                    }

                    zeroFilled = falsity;
                    currentLL = array[splitIndex] as LinkedListLot<T>;
                    currentLL?.AddLast(enumerator.Current);
                    if (splitIndex < splitCount - one)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                    }
                }

                zeroFilled = falsity;
            }

            enumerator.Dispose();

            return array;
        }
    }
}
