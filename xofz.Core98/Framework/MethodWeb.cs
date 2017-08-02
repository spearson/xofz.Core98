namespace xofz.Framework
{
    using System.Collections.Generic;

    public class MethodWeb
    {
        public MethodWeb()
        {
            this.dependencies = new List<Dependency>(0xFFFF);
        }

        public virtual void RegisterDependency(
            object dependency,
            string name = null)
        {
            this.dependencies.Add(
                new Dependency
                {
                    Content = dependency,
                    Name = name
                });
        }

        public virtual T Run<T>(
            Action<T> method = null,
            string dependencyName = null)
        {
            var ds = this.dependencies;
            for (var i = 0; i < ds.Count; ++i)
            {
                var d = ds[i];
                if (d.Name != dependencyName)
                {
                    continue;
                }

                if (!(d.Content is T))
                {
                    continue;
                }

                var t = (T)d.Content;
                method?.Invoke(t);

                return t;
            }

            return default(T);
        }

        public virtual U Run<T, U>(
            Func<T, U> method,
            string dependencyName = null)
        {
            var ds = this.dependencies;
            for (var i = 0; i < ds.Count; ++i)
            {
                var d = ds[i];
                if (d.Name != dependencyName)
                {
                    continue;
                }

                if (!(d.Content is T))
                {
                    continue;
                }

                return method.Invoke((T)d.Content);
            }

            return default(U);
        }

        private readonly List<Dependency> dependencies;

        private class Dependency
        {
            public virtual string Name { get; set; }

            public virtual object Content { get; set; }
        }
    }
}
