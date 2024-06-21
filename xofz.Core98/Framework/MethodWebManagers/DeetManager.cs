namespace xofz.Framework.MethodWebManagers
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class DeetManager
        : MethodWebManagerV2
    {
        public virtual IEnumerable<XTuple<MethodWeb, string>> ViewWebs()
        {
            IEnumerable<XTuple<MethodWeb, string>> ws;
            lock (this.locker)
            {
                ws = new XLinkedListLot<XTuple<MethodWeb, string>>(
                    XLinkedList<XTuple<MethodWeb, string>>.Create(
                        EnumerableHelpers.Select(
                            this.webs,
                            webHolder => XTuple.Create(
                                webHolder.Web,
                                webHolder.Name))));
            }

            return ws;
        }
    }
}