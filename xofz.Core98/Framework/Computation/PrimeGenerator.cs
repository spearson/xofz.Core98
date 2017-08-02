namespace xofz.Framework.Computation
{
    using System.Collections.Generic;

    public class PrimeGenerator
    {
        public PrimeGenerator()
        {
            this.tester = new PrimeTester();
            this.currentSet = new LinkedList<long>(new long[] { 2, 3 });
        }

        public PrimeGenerator(LinkedList<long> currentSet)
        {
            this.tester = new PrimeTester();
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
            this.currentSet.AddLast(this.currentSet.Last.Value + 2);
            while (!this.tester.RelativelyPrime(this.currentSet, true))
            {
                var node = this.currentSet.Last;
                this.currentSet.RemoveLast();
                this.currentSet.AddLast(node.Value + 2);
            }

            return this.currentSet.Last.Value;
        }

        private readonly LinkedList<long> currentSet;
        private readonly PrimeTester tester;
    }
}
