namespace xofz.Framework.MethodWebManagers
{
    using System.Collections.Generic;

    public class CopyManager
        : MethodWebManagerV2
    {
        public CopyManager()
        {
        }

        protected CopyManager(
            ICollection<NamedMethodWebHolder> webs)
            : base(webs)
        {
        }

        protected CopyManager(
            object locker)
            : base(locker)
        {
        }

        protected CopyManager(
            ICollection<NamedMethodWebHolder> webs,
            object locker)
            : base(webs, locker)
        {
        }

        public virtual void CopyTo(
            MethodWebManager other)
        {
            ICollection<NamedMethodWebHolder> websCopy;
            lock (this.locker)
            {
                websCopy = XLinkedList<NamedMethodWebHolder>.Create(
                    this.webs);
            }

            foreach (var web in websCopy)
            {
                other.AddWeb(
                    web?.Web,
                    web?.Name);
            }
        }

        public virtual void CopyFrom(
            CopyManager other)
        {
            ICollection<NamedMethodWebHolder> websToCopy;
            lock (other.locker)
            {
                websToCopy = XLinkedList<NamedMethodWebHolder>.Create(
                    other.webs);
            }

            foreach (var web in websToCopy)
            {
                this.AddWeb(
                    web?.Web,
                    web?.Name);
            }
        }
    }
}
