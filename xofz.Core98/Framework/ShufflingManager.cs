namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public class ShufflingManager
        : MethodWebManagerV2
    {
        public ShufflingManager()
        {
        }

        protected ShufflingManager(
            ICollection<NamedMethodWebHolder> webs)
            : base(webs)
        {
        }

        protected ShufflingManager(
            object locker)
            : base(locker)
        {
        }

        [System.Obsolete]
        protected ShufflingManager(
            RNGCryptoServiceProvider randomGen)
        {
        }

        protected ShufflingManager(
            ICollection<NamedMethodWebHolder> webs,
            object locker)
            : base(webs, locker)
        {
        }

        [System.Obsolete]
        protected ShufflingManager(
            ICollection<NamedMethodWebHolder> webs,
            RNGCryptoServiceProvider randomGen)
            : base(webs)
        {
        }

        [System.Obsolete]
        protected ShufflingManager(
            RNGCryptoServiceProvider randomGen,
            object locker)
            : base(locker)
        {
        }

        [System.Obsolete]
        protected ShufflingManager(
            RNGCryptoServiceProvider randomGen,
            ICollection<NamedMethodWebHolder> webs,
            object locker)
            : base(webs, locker)
        {
        }

        public override int CompareTo(
            object obj)
        {
            if (obj is ShufflingManager otherM)
            {
                var soThis = new ShufflingObject(this);
                var soOther = new ShufflingObject(otherM);
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
    }
}
