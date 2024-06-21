namespace xofz.Framework.MethodWebManagers
{
    using System.Collections.Generic;

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
            if (ws == null)
            {
                return falsity;
            }

            var targetHolder = EnumerableHelpers.FirstOrNull(
                ws,
                webHolder => webHolder.Name == webName);
            if (targetHolder == null)
            {
                return falsity;
            }

            return ws.Remove(targetHolder);
        }
    }
}