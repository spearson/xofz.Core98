﻿namespace xofz
{
    using System.Security.Cryptography;

    public class ShufflingObject
        : ShufflingObject<object>
    {
        public ShufflingObject()
            : base(new object())
        {
        }

        public ShufflingObject(
            object o)
            : base(o)
        {
        }

        protected ShufflingObject(
            RandomNumberGenerator randomGen,
            object o)
            : base(randomGen, o)
        {
        }
    }

    public class ShufflingObject<T>
        : System.IComparable
    {
        public ShufflingObject()
            : this(
                RandomNumberGenerator.Create(),
                default)
        {
        }

        public ShufflingObject(
            T o)
            : this(
                RandomNumberGenerator.Create(),
                o)
        {
        }

        protected ShufflingObject(
            RandomNumberGenerator randomGen,
            T o)
        {
            this.randomGen = randomGen;
            this.ob = o;
        }

        public virtual T O
        {
            get => this.ob;

            set => this.ob = value;
        }

        public virtual RandomNumberGenerator R
        {
            get => this.randomGen;

            set => this.randomGen = value;
        }

        public virtual int CompareTo(
            object obj)
        {
            const short minusOne = -1;
            const byte
                zero = 0,
                one = 1;
            if (obj is ShufflingObject<T> other)
            {
                var thisBuffer = new byte[one];
                var otherBuffer = new byte[one];
                this.R?.GetBytes(thisBuffer);
                other.R?.GetBytes(otherBuffer);

                var thisNumber = thisBuffer[zero];
                var otherNumber = otherBuffer[zero];
                return thisNumber > otherNumber
                    ? one
                    : otherNumber > thisNumber
                        ? minusOne
                        : zero;
            }

            return one;
        }

        protected T ob;
        protected RandomNumberGenerator randomGen;
    }
}