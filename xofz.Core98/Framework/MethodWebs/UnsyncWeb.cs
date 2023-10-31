using System.Collections.Generic;

namespace xofz.Framework.MethodWebs
{
    public class UnsyncWeb 
        : MethodWeb
    {
        public UnsyncWeb()
        {
        }

        protected UnsyncWeb(
            ICollection<Dependency> dependencies)
            : base(dependencies)
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

            return unregistered;
        }
    }
}
