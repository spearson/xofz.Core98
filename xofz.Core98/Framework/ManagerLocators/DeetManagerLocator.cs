namespace xofz.Framework.ManagerLocators
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class DeetManagerLocator 
        : ManagerLocator
    {
        public virtual IEnumerable<XTuple<MethodWebManager, string>> ViewManagers()
        {
            IEnumerable<XTuple<MethodWebManager, string>> ms;
            lock (this.locker)
            {
                ms = new LinkedListLot<XTuple<MethodWebManager, string>>(
                    EnumerableHelpers.Select(
                        this.managers,
                        man =>
                        {
                            return XTuple.Create(
                                man?.Manager,
                                man?.Name);
                        }));
            }

            return ms;
        }
    }
}
