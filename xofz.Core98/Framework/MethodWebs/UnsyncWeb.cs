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
            var ds = this.dependencies;
            var unregistered = falsity;
            foreach (var d in ds
                              ?? EnumerableHelpers.Empty<Dependency>())
            {
                if (this.tryGet(
                    d.Content,
                    d.Name,
                    name,
                    out T _))
                {
                    unregistered = ds?.Remove(d)
                                   ?? falsity;
                    break;
                }
            }

            return unregistered;
        }
    }
}
