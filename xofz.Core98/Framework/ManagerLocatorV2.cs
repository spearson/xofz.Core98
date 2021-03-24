namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class ManagerLocatorV2
        : ManagerLocator
    {
        public virtual MethodWebManagerV2 Shuffle()
        {
            var matchingManagers = this.shuffleManagers();
            return EH.FirstOrDefault(
                matchingManagers);
        }

        public virtual T Shuffle<T>()
            where T : MethodWebManagerV2
        {

            foreach (var manager in this.shuffleManagers()
                                ?? EH.Empty<MethodWebManagerV2>())
            {
                if (manager is T matchingManager)
                {
                    return matchingManager;
                }
            }

            return default;
        }

        protected virtual Lot<MethodWebManagerV2> shuffleManagers()
        {
            ICollection<NamedManagerHolder> ms;
            ListLot<MethodWebManagerV2> matchingManagers
                = new ListLot<MethodWebManagerV2>();

            lock (this.locker ?? new object())
            {
                ms = this.managers;
                foreach (var managerHolder in ms ??
                                              EH.Empty<NamedManagerHolder>())
                {
                    if (managerHolder?.Manager is MethodWebManagerV2 managerV2)
                    {
                        matchingManagers.Add(managerV2);
                    }
                }
            }

            matchingManagers.Sort();
            return matchingManagers;
        }
    }
}
