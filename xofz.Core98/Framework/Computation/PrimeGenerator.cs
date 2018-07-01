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
            LinkedList<long> currentSet)
            : this(new PrimeTester(), currentSet)
        {
        }

        public PrimeGenerator(
            PrimeTester tester,
            LinkedList<long> currentSet)
        {
            this.tester = tester;
            this.currentSet = currentSet;
        }

        public LinkedList<long> CurrentSet => this.currentSet;

        public virtual long NextPrime()
        {
            return this.collectPrime();
        }

        public virtual IEnumerable<long> Generate()
        {
            foreach (var prime in this.currentSet)
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
