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
                    2,
                    3
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
                    2,
                    3
                }))
        {
        }

        public PrimeGenerator(
            PrimeTester tester,
            IEnumerable<long> finiteSet)
        {
            this.tester = tester;

            if (finiteSet is LinkedListLot<long> set)
            {
                this.currentSet = set;
                return;
            }

            this.currentSet = new LinkedListLot<long>(
                finiteSet);
        }

        public PrimeGenerator(
            PrimeTester tester,
            LinkedListLot<long> currentSet)
        {
            this.tester = tester;
            this.currentSet = currentSet;
        }

        public Lot<long> CurrentSet => this.currentSet;

        public virtual long NextPrime()
        {
            return this.collectPrime();
        }

        public virtual IEnumerable<long> Generate()
        {
            IEnumerable<long> source = this.currentSet;
            foreach (var prime in source)
            {
                yield return prime;
            }

            while (true)
            {
                yield return this.collectPrime();
            }
        }

        private long collectPrime()
        {
            var ll = this.currentSet;
            ll.AddLast(ll.Last.Value + 2);
            while (!this.tester.RelativelyPrime(ll, true))
            {
                var node = ll.Last;
                ll.RemoveLast();
                ll.AddLast(node.Value + 2);
            }

            return ll.Last.Value;
        }

        private readonly LinkedListLot<long> currentSet;
        private readonly PrimeTester tester;
    }
}
