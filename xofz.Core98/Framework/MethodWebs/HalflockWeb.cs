namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;

    public class HalflockWeb
        : MethodWeb
    {
        public HalflockWeb()
            : this(null)
        {
        }

        protected HalflockWeb(
            object locker)
            : this(null, locker)
        {
        }

        protected HalflockWeb(
            ICollection<Dependency> dependencies,
            object locker = null)
            : base(dependencies)
        {
            this.locker = locker ??
                          new object();
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
