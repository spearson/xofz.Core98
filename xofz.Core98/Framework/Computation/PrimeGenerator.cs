namespace xofz.Framework.Computation
{
    using System.Collections.Generic;

    public class PrimeGenerator
    {
        public PrimeGenerator()
            : this(
                new PrimeTester(),
                new LinkedList<long>(new long[]
                {
                    2,
                    3
                }))
        {
        }

        public PrimeGenerator(
            IEnumerable<long> currentSet)
            : this(new PrimeTester(), currentSet)
        {
        }

        public PrimeGenerator(
            PrimeTester tester)
            : this(
                tester,
                new LinkedList<long>(new long[]
                {
                    2,
                    3
                }))
        {
        }

        public PrimeGenerator(
            PrimeTester tester,
            IEnumerable<long> currentSet)
        {
            this.tester = tester;

            if (currentSet is LinkedList<long> set)
            {
                this.currentSet = set;
                return;
            }

            this.currentSet = new LinkedList<long>(currentSet);
        }

        public ICollection<long> CurrentSet => this.currentSet;

        public virtual long NextPrime()
        {
            return this.collectPrime();
        }

        public virtual IEnumerable<long> Generate()
        {
            IEnumerable<long> cs = this.currentSet;
            foreach (var prime in cs)
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
            var cs = this.currentSet;
            cs.AddLast(cs.Last.Value + 2);
            while (!this.tester.RelativelyPrime(cs, true))
            {
                var node = cs.Last;
                cs.RemoveLast();
                cs.AddLast(node.Value + 2);
            }

            return cs.Last.Value;
        }

        private readonly LinkedList<long> currentSet;
        private readonly PrimeTester tester;
    }
}
