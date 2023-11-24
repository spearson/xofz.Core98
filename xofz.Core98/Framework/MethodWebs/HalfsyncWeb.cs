namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;

    public class HalfsyncWeb : MethodWeb
    {
        public HalfsyncWeb()
        {
        }

        protected HalfsyncWeb(
            ICollection<Dependency> dependencies)
            : base(dependencies)
        {
        }

        protected HalfsyncWeb(
            object locker)
            : base(locker)
        {
        }

        protected HalfsyncWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }

        public virtual bool Unregister<T>(
            string name = null)
        {
            var deps = this.dependencies;
            var unregistered = falsity;
            if (deps == null)
            {
                return unregistered;
            }

            lock (this.locker)
            {
                foreach (var d in deps)
                {
                    if (this.tryGet(
                            d.Content,
                            d.Name,
                            name,
                            out T _))
                    {
                        unregistered = deps.Remove(d);
                        break;
                    }
                }
            }

            return unregistered;
        }
    }
}
