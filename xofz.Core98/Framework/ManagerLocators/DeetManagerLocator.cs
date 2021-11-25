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
                ms = new XLinkedListLot<XTuple<MethodWebManager, string>>(
                    XLinkedList<XTuple<MethodWebManager, string>>.Create(
                        EnumerableHelpers.Select(
                            this.managers,
                            man => XTuple.Create(
                                man?.Manager,
                                man?.Name))));
            }

            return ms;
        }
    }
}
