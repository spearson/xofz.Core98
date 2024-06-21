namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class DeetWeb
        : MethodWebV2
    {
        public virtual IEnumerable<XTuple<object, string>> ViewDependencies()
        {
            IEnumerable<XTuple<object, string>> ds;
            lock (this.locker)
            {
                ds = new XLinkedListLot<XTuple<object, string>>(
                    XLinkedList<XTuple<object, string>>.Create(
                        EnumerableHelpers.Select(
                            this.dependencies,
                            dep => XTuple.Create(
                                dep?.Content,
                                dep?.Name))));
            }

            return ds;
        }
    }
}