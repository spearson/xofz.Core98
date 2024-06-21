namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;

    public class CopyWeb
        : MethodWebV2
    {
        public CopyWeb()
        {
        }

        protected CopyWeb(
            ICollection<Dependency> dependencies)
            : base(dependencies)
        {
        }

        protected CopyWeb(
            object locker)
            : base(locker)
        {
        }

        protected CopyWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }

        public virtual void CopyTo(
            MethodWeb other)
        {
            ICollection<Dependency> depCopy;
            lock (this.locker)
            {
                depCopy = XLinkedList<Dependency>.Create(
                    this.dependencies);
            }

            foreach (var dep in depCopy)
            {
                other.RegisterDependency(
                    dep?.Content,
                    dep?.Name);
            }
        }

        public virtual void CopyFrom(
            CopyWeb other)
        {
            ICollection<Dependency> depToCopy;
            lock (other.locker)
            {
                depToCopy = XLinkedList<Dependency>.Create(
                    other.dependencies);
            }

            foreach (var dep in depToCopy)
            {
                this.RegisterDependency(
                    dep?.Content,
                    dep?.Name);
            }
        }
    }
}