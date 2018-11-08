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
            return this.RelativelyPrime(
                new LinkedListLot<long>(
                    finiteSource),
                onlyCheckLast);
        }

        public virtual bool RelativelyPrime(
            ICollection<long> collection,
            bool onlyCheckLast)
        {
            var ll = collection as LinkedList<long>;
            if (ll != null)
            {
                return this.RelativelyPrime(
                    ll, 
                    onlyCheckLast);
            }

            return this.RelativelyPrime(
                new LinkedListLot<long>(
                    collection),
                onlyCheckLast);
        }

        public virtual bool RelativelyPrime(
            LinkedList<long> ll,
            bool onlyCheckLast)
        {
            return this.RelativelyPrime(
                new LinkedListLot<long>(ll),
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
