namespace xofz.Framework.Computation
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class PrimeGenerator
    {
        public PrimeGenerator()
            : this(
                new PrimeTester(),
                XLinkedList<long>.Create(new long[]
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
                XLinkedList<long>.Create(new long[]
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

            while (true)
            {
                yield return this.collectPrime();
            }
        }

        protected virtual long collectPrime()
        {
            const bool truth = true;
            var ll = this.currentLinkedList;
            ll?.AddTail(
                (long)(ll?.Tail + firstPrimality));
            while (!this.tester.RelativelyPrime(ll, truth))
            {
                var node = ll?.RemoveTail();
                ll?.AddTail(
                    node?.O + firstPrimality
                    ?? firstPrimality);
            }

            return ll?.Tail ?? firstPrimality;
        }

        protected readonly XLinkedListLot<long> currentLinkedList;
        protected readonly PrimeTester tester;
        protected const byte firstPrimality = 2;
        protected const byte secondPrime = 3;
    }
}