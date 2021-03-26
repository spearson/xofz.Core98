namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class ManagerLocatorV2
        : ManagerLocator
    {
        public virtual MethodWebManager Shuffle()
        {
            var matchingManagers = this.shuffleManagers();
            return EH.FirstOrDefault(
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
            ICollection<NamedManagerHolder> ms;
            var matchingManagers = new ListLot<ShufflingObject>();

            lock (this.locker ?? new object())
            {
                ms = this.managers;
                foreach (var managerHolder in ms ??
                                              EH.Empty<NamedManagerHolder>())
                {
                    matchingManagers?.Add(
                        new ShufflingObject(
                            new NamedManagerHolder
                            {
                                Manager = managerHolder?.Manager,
                                Name = managerHolder?.Name,
                                Webs = managerHolder?.Webs
                            }));
                }
            }

            matchingManagers?.Sort();

            return new LinkedListLot<NamedManagerHolder>(
                EH.Select(
                    matchingManagers,
                    so => so.O as NamedManagerHolder));
        }
    }
}
