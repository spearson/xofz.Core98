using System.Collections.Generic;

namespace xofz.Framework.MethodWebManagers
{
    public class UnsyncManager 
        : MethodWebManager
    {
        public UnsyncManager()
        {
        }

        protected UnsyncManager(
            ICollection<NamedMethodWebHolder> webs)
            : base(webs)
        {
        }

        public virtual bool RemoveWeb(
            string webName)
        {
            var ws = this.webs;
            var targetHolder = EnumerableHelpers.FirstOrNull(
                ws,
                webHolder => webHolder?.Name == webName);
            return ws?.Remove(targetHolder)
                      ?? falsity;
        }
    }
}
