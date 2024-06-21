namespace xofz.Framework.MethodWebManagers
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

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
            return EH.FirstOrNull(
                    matchingWebs)
                ?.Web;
        }

        public virtual T Shuffle<T>()
            where T : MethodWeb
        {
            foreach (var webHolder in this.shuffleWebs()
                                      ?? EH.Empty<NamedMethodWebHolder>())
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
            var matchingWebs = new List<
                ShufflingObject<NamedMethodWebHolder>>();

            lock (this.locker)
            {
                foreach (var webHolder in
                         this.webs ?? EH.Empty<NamedMethodWebHolder>())
                {
                    matchingWebs.Add(
                        new ShufflingObject<NamedMethodWebHolder>(
                            webHolder));
                }
            }

            matchingWebs.Sort();

            return new XLinkedListLot<NamedMethodWebHolder>(
                XLinkedList<NamedMethodWebHolder>.Create(
                    EH.Select(
                        matchingWebs,
                        so => so.O)));
        }
    }
}