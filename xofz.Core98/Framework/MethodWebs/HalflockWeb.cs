namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;

    public class HalflockWeb
        : MethodWeb
    {
        public HalflockWeb()
            : base(null)
        {
        }

        protected HalflockWeb(
            object locker)
            : base(null, locker)
        {
        }

        protected HalflockWeb(
            ICollection<Dependency> dependencies,
            object locker = null)
            : base(dependencies, locker)
        {
        }
    }
}
