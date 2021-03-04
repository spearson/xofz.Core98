namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class ManagerLocatorV2
        : ManagerLocator
    {
        public virtual MethodWebManager Shuffle()
        {
            ICollection<NamedManagerHolder> ms;
            ListLot<NamedManagerHolder> ll;

            lock (this.locker ?? new object())
            {
                ms = this.managers;
                ll = new ListLot<NamedManagerHolder>(
                    ms);
            }

            ll.Sort();

            const byte zero = 0;
            try
            {
                return ll[zero]?.Manager;
            }
            catch
            {
                return default;
            }
        }
    }
}
