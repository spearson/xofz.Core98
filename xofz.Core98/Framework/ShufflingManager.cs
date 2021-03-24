namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public class ShufflingManager
        : MethodWebManagerV2
    {
        public ShufflingManager()
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingManager(
            ICollection<NamedMethodWebHolder> webs)
            : base(webs)
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingManager(
            object locker)
            : base(locker)
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingManager(
            RNGCryptoServiceProvider randomGen)
        {
            this.randomGen = randomGen;
        }

        protected ShufflingManager(
            ICollection<NamedMethodWebHolder> webs,
            object locker)
            : base(webs, locker)
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingManager(
            ICollection<NamedMethodWebHolder> webs,
            RNGCryptoServiceProvider randomGen)
            : base(webs)
        {
            this.randomGen = randomGen;
        }

        protected ShufflingManager(
            RNGCryptoServiceProvider randomGen,
            object locker)
            : base(locker)
        {
            this.randomGen = randomGen;
        }

        protected ShufflingManager(
            RNGCryptoServiceProvider randomGen,
            ICollection<NamedMethodWebHolder> webs,
            object locker)
            : base(webs, locker)
        {
            this.randomGen = randomGen;
        }

        public override int CompareTo(
            object obj)
        {
            const short nOne = -1;
            const byte
                zero = 0,
                one = 1;
            if (obj is ShufflingManager other)
            {
                var thisBuffer = new byte[one];
                var otherBuffer = new byte[one];
                this?.randomGen?.GetBytes(thisBuffer);
                other?.randomGen?.GetBytes(otherBuffer);

                var thisNumber = thisBuffer[zero];
                var otherNumber = otherBuffer[zero];
                return thisNumber > otherNumber
                    ? one
                    : otherNumber > thisNumber
                        ? nOne
                        : zero;
            }

            return one;
        }

        protected readonly RNGCryptoServiceProvider randomGen;
    }
}
