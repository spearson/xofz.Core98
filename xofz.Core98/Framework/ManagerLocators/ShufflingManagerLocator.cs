namespace xofz.Framework.ManagerLocators
{
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class ShufflingManagerLocator
        : ManagerLocator, System.IComparable
    {
        public virtual MethodWebManager Shuffle()
        {
            var matchingManagers = this.shuffleManagers();
            return EH.FirstOrNull(
                    matchingManagers)?.
                Manager;
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

        protected virtual Lot<NamedManagerHolder> shuffleManagers()
        {
            var matchingManagers = new ListLot<
                ShufflingObject<NamedManagerHolder>>();

            lock (this.locker)
            {
                foreach (var managerHolder in this.managers ??
                                              EH.Empty<NamedManagerHolder>())
                {
                    matchingManagers?.Add(
                        new ShufflingObject<NamedManagerHolder>(
                            new NamedManagerHolder
                            {
                                Manager = managerHolder?.Manager,
                                Name = managerHolder?.Name
                            }));
                }
            }

            matchingManagers?.Sort();

            return new XLinkedListLot<NamedManagerHolder>(
                XLinkedList<NamedManagerHolder>.Create(
                    EH.Select(
                        matchingManagers,
                        so => so.O)));
        }

        public virtual int CompareTo(
            object obj)
        {
            var soThis = new ShufflingObject(this);
            var soOther = new ShufflingObject(obj);

            return soThis.CompareTo(soOther);
        }
    }
}
