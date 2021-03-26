namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public class ShufflingWeb
        : MethodWebV2
    {
        public ShufflingWeb()
        {
        }

        protected ShufflingWeb(
            ICollection<Dependency> dependencies)
            : base(dependencies)
        {
        }

        protected ShufflingWeb(
            object locker)
            : base(locker)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen)
        {
        }

        protected ShufflingWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            ICollection<Dependency> dependencies,
            RNGCryptoServiceProvider randomGen)
            : base(dependencies)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen,
            object locker)
            : base(locker)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen,
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }

        protected ShufflingWeb(
            MethodWeb copy,
            LotterV2 lotter)
            : base(copy, lotter)
        {
        }

        public override int CompareTo(
            object obj)
        {
            if (obj is ShufflingWeb otherW)
            {
                var soThis = new ShufflingObject(this);
                var soOther = new ShufflingObject(otherW);
                return soThis.CompareTo(soOther);
            }

            if (obj is ShufflingObject otherO)
            {
                var soThis = new ShufflingObject(this);
                return soThis.CompareTo(otherO);
            }

            return base.CompareTo(obj);
        }

        [System.Obsolete(@"This field does nothing.  Check ShufflingObject out.")]
        protected readonly RNGCryptoServiceProvider randomGen;

        protected class ShufflingDependency
            : ShufflingObject
        {
            public virtual object Content { get; set; }

            public virtual string Name { get; set; }

        }
    }
}