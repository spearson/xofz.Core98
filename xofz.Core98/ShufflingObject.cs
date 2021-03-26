namespace xofz
{
    using System;
    using System.Security.Cryptography;

    public class ShufflingObject
        : System.IComparable
    {
        public ShufflingObject()
            : this(
                new RNGCryptoServiceProvider(),
                new object())
        {
        }

        public ShufflingObject(
            object o)
            : this(
                new RNGCryptoServiceProvider(),
                o)
        {
        }

        protected ShufflingObject(
            RNGCryptoServiceProvider randomGen,
            object o)
        {
            this.randomGen = randomGen;
            this.ob = o;
        }

        public virtual Object O
        {
            get => this.ob;

            set => this.ob = value;
        }

        public virtual RNGCryptoServiceProvider R
        {
            get => this.randomGen;

            set => this.randomGen = value;
        }

        public virtual int CompareTo(
            object obj)
        {
            const short nOne = -1;
            const byte
                zero = 0,
                one = 1;
            if (obj is ShufflingObject other)
            {
                var thisBuffer = new byte[one];
                var otherBuffer = new byte[one];
                this?.R?.GetBytes(thisBuffer);
                other?.R?.GetBytes(otherBuffer);

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

        protected object ob;
        protected RNGCryptoServiceProvider randomGen;
    }
}
