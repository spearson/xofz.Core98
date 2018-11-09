namespace xofz.Framework.Computation
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class PrimeTester
    {
        public virtual bool RelativelyPrime(
            IEnumerable<long> finiteSource,
            bool onlyCheckLast)
        {
            if (finiteSource == null)
            {
                return true;
            }

            if (finiteSource is Lot<long> lot)
            {
                return this.RelativelyPrime(
                    lot, onlyCheckLast);
            }

            return this.RelativelyPrime(
                new LinkedListLot<long>(
                    finiteSource),
                onlyCheckLast);
        }

        public virtual bool RelativelyPrime(
            Lot<long> lot, 
            bool onlyCheckLast)
        {
            if (lot.Count == 0)
            {
                return true;
            }

            var ll = lot as LinkedListLot<long>
                     ?? new LinkedListLot<long>(lot);

            if (onlyCheckLast)
            {
                var numberToCheck = ll.Last.Value;
                var squareRoot = (long)Math.Sqrt(numberToCheck);
                foreach (var number in EnumerableHelpers.Where(
                    ll,
                    n => n <= squareRoot))
                {
                    if (numberToCheck % number == 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            while (true)
            {
                var lowestNumber = ll.First.Value;
                ll.RemoveFirst();
                if (ll.Count == 0)
                {
                    return true;
                }

                foreach (var number in ll)
                {
                    if (number % lowestNumber == 0)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
