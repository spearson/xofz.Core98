namespace xofz.Framework.MethodWebManagers
{
    using System.Collections.Generic;
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

        protected ShufflingManager(
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
            var matchingWebs = new ListLot<
                ShufflingObject<NamedMethodWebHolder>>();

            lock (this.locker)
            {
                ws = this.webs;
                foreach (var webHolder in ws
                                          ?? EnumerableHelpers.
                                              Empty<NamedMethodWebHolder>())
                {
                    matchingWebs?.Add(
                        new ShufflingObject<NamedMethodWebHolder>(
                            new NamedMethodWebHolder
                            {
                                Web = webHolder?.Web,
                                Name = webHolder?.Name
                            }));
                }
            }

            matchingWebs?.Sort();

            return new XLinkedListLot<NamedMethodWebHolder>(
                XLinkedList<NamedMethodWebHolder>.Create(
                    EnumerableHelpers.Select(
                        matchingWebs,
                        so => so.O)));
        }
    }
}
