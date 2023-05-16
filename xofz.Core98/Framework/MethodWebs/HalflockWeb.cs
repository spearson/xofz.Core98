namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;

    public class HalflockWeb
        : MethodWeb
    {
        public HalflockWeb()
        {
            this.locker = new object();
        }

        protected HalflockWeb(
            ICollection<Dependency> dependencies)
            : this(dependencies, new object())
        {
        }

        protected HalflockWeb(
            object locker)
            : this(new XLinkedList<Dependency>(), locker)
        {
        }

        protected HalflockWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies)
        {
            this.locker = locker;
        }

        protected override void register(
            Dependency dependency)
        {
            lock (this.locker)
            {
                base.register(
                    dependency);
            }
        }

        protected readonly object locker;
    }
}
