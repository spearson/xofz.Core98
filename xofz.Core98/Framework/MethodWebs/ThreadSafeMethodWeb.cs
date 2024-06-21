namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;

    public class ThreadSafeMethodWeb
        : MethodWebV2
    {
        public ThreadSafeMethodWeb()
        {
        }

        protected ThreadSafeMethodWeb(
            ICollection<Dependency> dependencies)
            : base(dependencies)
        {
        }

        protected ThreadSafeMethodWeb(
            object locker)
            : base(locker)
        {
        }

        protected ThreadSafeMethodWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }
    }
}