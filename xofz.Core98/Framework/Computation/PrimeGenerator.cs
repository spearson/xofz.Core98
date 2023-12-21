namespace xofz.Framework.Computation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class PrimeGenerator
    {
        public PrimeGenerator()
            : this(
                new PrimeTester(),
                new long[]
                {
                    firstPrime,
                    secondPrime
                })
        {
        }

        public PrimeGenerator(
            IEnumerable<long> finiteSet)
            : this(
                new PrimeTester(),
                finiteSet)
        {
        }

        public PrimeGenerator(
            PrimeTester tester)
            : this(
                tester,
                new long[]
                {
                    firstPrime,
                    secondPrime
                })
        {
        }

        public PrimeGenerator(
            PrimeTester tester,
            IEnumerable<long> finiteSet)
        {
            this.tester = tester;
            this.currentLinkedList = new XLinkedListLot<long>(
                XLinkedList<long>.Create(finiteSet));
        }

        public virtual Lot<long> CurrentSet => this.currentLinkedList;

        public virtual long NextPrime()
        {
            return this.collectPrime();
        }

        public virtual IEnumerable<long> Generate()
        {
            IEnumerable<long> source = this.currentLinkedList;
            foreach (var prime in source)
            {
                yield return prime;
            }

            while (truth)
            {
                yield return this.collectPrime();
            }
        }

        protected virtual long collectPrime()
        {
            var ll = this.currentLinkedList;
            if (ll == null)
            {
                return firstPrime;
            }

            ll.AddTail(ll.Tail + firstPrime);
            while (!this.tester.RelativelyPrime(ll, truth))
            {
                var node = ll.RemoveTail();
                if (node == null)
                {
                    return firstPrime;
                }

                ll.AddTail(
                    node.O + firstPrime);
            }

            return ll.Tail;
        }

        protected readonly XLinkedListLot<long> currentLinkedList;
        protected readonly PrimeTester tester;

        protected const byte
            firstPrime = 2,
            secondPrime = 3;
        protected const bool 
            truth = true;
    }
}