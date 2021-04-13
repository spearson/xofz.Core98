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
            lock (this.locker ?? new object())
            {
                ds = new LinkedListLot<XTuple<object, string>>(
                    EnumerableHelpers.Select(
                        this.dependencies,
                        dep =>
                        {
                            return XTuple.Create(
                                dep.Content,
                                dep.Name);
                        }));
            }

            return ds;
        }
    }
}
