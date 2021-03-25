namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public class ShufflingWeb
        : MethodWebV2
    {
        public ShufflingWeb()
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingWeb(
            ICollection<Dependency> dependencies)
            : base(dependencies)
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingWeb(
            object locker)
            : base(locker)
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen)
        {
            this.randomGen = randomGen;
        }

        protected ShufflingWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
            this.randomGen = new RNGCryptoServiceProvider();
        }

        protected ShufflingWeb(
            ICollection<Dependency> dependencies,
            RNGCryptoServiceProvider randomGen)
            : base(dependencies)
        {
            this.randomGen = randomGen;
        }

        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen,
            object locker)
            : base(locker)
        {
            this.randomGen = randomGen;
        }

        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen,
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
            this.randomGen = randomGen;
        }

        public override int CompareTo(
            object obj)
        {
            if (obj is ShufflingWeb other)
            {
                const byte
                    zero = 0,
                    one = 1;
                const short nOne = -1;
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

            return base.CompareTo(obj);
        }

        protected readonly RNGCryptoServiceProvider randomGen;
    }
}
