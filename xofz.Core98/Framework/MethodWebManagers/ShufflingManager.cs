namespace xofz.Framework.MethodWebManagers
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using xofz.Framework.Lots;

    public class ShufflingManager
        : MethodWebManagerV2, System.IComparable
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

        public virtual MethodWeb Shuffle()
        {
            var matchingWebs = this.shuffleWebs();
            return EnumerableHelpers.FirstOrDefault(
                    matchingWebs)?.
                Web;
        }

        public virtual T Shuffle<T>()
            where T : MethodWeb
        {
            foreach (var webHolder in this.shuffleWebs()
                                      ?? EnumerableHelpers.Empty<NamedMethodWebHolder>())
            {
                if (webHolder?.Web is T matchingWeb)
                {
                    return matchingWeb;
                }
            }

            return default;
        }

        public virtual int CompareTo(
            object obj)
        {
            var soThis = new ShufflingObject(this);
            var soOther = new ShufflingObject(obj);

            return soThis.CompareTo(soOther);
        }

        protected virtual Lot<NamedMethodWebHolder> shuffleWebs()
        {
            ICollection<NamedMethodWebHolder> ws;
            var matchingWebs = new ListLot<ShufflingObject>();

            lock (this.locker ?? new object())
            {
                ws = this.webs;
                foreach (var webHolder in ws
                                          ?? EnumerableHelpers.Empty<NamedMethodWebHolder>())
                {
                    matchingWebs?.Add(
                        new ShufflingObject(
                            new NamedMethodWebHolder
                            {
                                Web = webHolder?.Web,
                                Name = webHolder?.Name
                            }));
                }
            }

            matchingWebs?.Sort();

            return new LinkedListLot<NamedMethodWebHolder>(
                EnumerableHelpers.Select(
                    matchingWebs,
                    so => so.O as NamedMethodWebHolder));
        }

        [System.Obsolete(@"This field does nothing.  Check ShufflingObject out.")]
        protected readonly RNGCryptoServiceProvider randomGen;
    }
}
