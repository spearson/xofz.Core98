namespace xofz.Framework.ManagerLocators
{
    using System.Collections.Generic;
    using xofz.Framework.Computation;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class ShufflingManagerLocator
        : ManagerLocator, System.IComparable
    {
        public ShufflingManagerLocator()
        {
        }

        protected ShufflingManagerLocator(
            ICollection<NamedManagerHolder> managers)
            : base(managers)
        {
        }

        protected ShufflingManagerLocator(
            object locker)
            : base(locker)
        {
        }

        protected ShufflingManagerLocator(
            ICollection<NamedManagerHolder> managers,
            object locker)
            : base(managers, locker)
        {
        }

        public virtual MethodWebManager Shuffle()
        {
            var matchingManagers = this.shuffleManagers();
            return EH.FirstOrNull(
                matchingManagers)?.Manager;
        }

        public virtual T Shuffle<T>()
            where T : MethodWebManager
        {
            foreach (var managerHolder in this.shuffleManagers()
                                          ?? EH.Empty<NamedManagerHolder>())
            {
                if (managerHolder?.Manager is T matchingManager)
                {
                    return matchingManager;
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

        protected virtual Lot<NamedManagerHolder> shuffleManagers()
        {
            var matchingManagers = new IndexedLinkedList<
                ShufflingObject<NamedManagerHolder>>();
            IEnumerable<NamedManagerHolder> ms;

            lock (this.locker)
            {
                ms = this.managers;
                foreach (var managerHolder in
                    ms ?? EH.Empty<NamedManagerHolder>())
                {
                    matchingManagers?.Add(
                        new ShufflingObject<NamedManagerHolder>(
                            managerHolder));
                }
            }

            var sorter = new QuickSorter();
            sorter?.SortV2(matchingManagers);

            return new IndexedLinkedListLot<NamedManagerHolder>(
                IndexedLinkedList<NamedManagerHolder>.CreateIndexed(
                    EH.Select(
                        matchingManagers,
                        so => so.O)));
        }
    }
}
