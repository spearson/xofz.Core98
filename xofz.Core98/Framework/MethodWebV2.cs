namespace xofz.Framework
{
    using System.Collections.Generic;

    public class MethodWebV2
        : ThreadSafeMethodWeb
    {
        public MethodWebV2()
        {
        }

        protected MethodWebV2(
            ICollection<Dependency> dependencies)
            : base(dependencies)
        {
        }

        protected MethodWebV2(
            object locker)
            : base(locker)
        {
        }

        protected MethodWebV2(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }
    }
}
