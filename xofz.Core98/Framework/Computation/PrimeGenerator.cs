namespace xofz.Framework.Computation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class PrimeGenerator
    {
        public PrimeGenerator()
            : this(
                new PrimeTester(),
                new LinkedListLot<long>(new long[]
                {
                    firstPrimality,
                    secondPrime
                }))
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
                new LinkedListLot<long>(new long[]
                {
                    firstPrimality,
                    secondPrime
                }))
        {
        }

        public PrimeGenerator(
            PrimeTester tester,
            IEnumerable<long> finiteSet)
        {
            this.tester = tester;

            if (finiteSet == null)
            {
                finiteSet = new LinkedListLot<long>(
                    new long[]
                    {
                        firstPrimality, secondPrime
                    });
            }

            if (finiteSet is LinkedListLot<long> ll)
            {
                this.currentLinkedList = ll;
                return;
            }

            this.currentLinkedList = new LinkedListLot<long>(
                finiteSet);
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

            while (true)
            {
                yield return this.collectPrime();
            }
        }

        protected virtual long collectPrime()
        {
            var ll = this.currentLinkedList;
            var lastNode = ll?.Last;
            ll?.AddLast(
                lastNode?.Value + firstPrimality 
                ?? firstPrimality);
            while (!this.tester.RelativelyPrime(ll, true))
            {
                var node = ll?.Last;
                ll?.RemoveLast();
                ll?.AddLast(
                    node?.Value + firstPrimality
                    ?? firstPrimality);
            }

            return ll?.Last?.Value ?? firstPrimality;
        }

        protected readonly LinkedListLot<long> currentLinkedList;
        protected readonly PrimeTester tester;
        protected const byte firstPrimality = 2;
        protected const byte secondPrime = 3;
    }
}
