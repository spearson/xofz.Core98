namespace xofz.Framework
{
    using System.Collections.Generic;

    public class MethodWeb
    {
        public MethodWeb()
        {
            this.dependencies = new LinkedList<Dependency>();
        }

        public virtual void RegisterDependency(
            object dependency,
            string name = null)
        {
            if (dependency == null)
            {
                throw new System.ArgumentNullException(
                    nameof(dependency));
            }

            this.dependencies.Add(
                new Dependency
                {
                    Content = dependency,
                    Name = name
                });
        }

        public virtual T Run<T>(
            Do<T> method = null,
            string dependencyName = null)
        {
            var ds = this.dependencies;
            T t;
            foreach (var d in ds)
            {
                if (d.Name != dependencyName)
                {
                    continue;
                }

                if (!(d.Content is T))
                {
                    continue;
                }

                t = (T)d.Content;
                goto invoke;
            }

            return default(T);

        invoke:
            method?.Invoke(t);

            return t;
        }

        public virtual XTuple<T, U> Run<T, U>(
            Do<T, U> method = null,
            string tName = null,
            string uName = null)
        {
            var ds = this.dependencies;
            T t;
            var missing = false;
            foreach (var d in ds)
            {
                if (d.Name != tName)
                {
                    continue;
                }

                if (!(d.Content is T))
                {
                    continue;
                }

                t = (T)d.Content;
                goto checkU;
            }

            t = default(T);
            missing = true;

        checkU:
            U u;
            foreach (var d in ds)
            {
                if (d.Name != uName)
                {
                    continue;
                }

                if (!(d.Content is U))
                {
                    continue;
                }

                u = (U)d.Content;
                goto invoke;
            }

            u = default(U);
            missing = true;

        invoke:
            if (!missing)
            {
                method?.Invoke(t, u);
            }

            return XTuple.Create(t, u);
        }

        public virtual XTuple<T, U, V> Run<T, U, V>(
            Do<T, U, V> method = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            var ds = this.dependencies;
            T t;
            var missing = false;
            foreach (var d in ds)
            {
                if (d.Name != tName)
                {
                    continue;
                }

                if (!(d.Content is T))
                {
                    continue;
                }

                t = (T)d.Content;
                goto checkU;
            }

            t = default(T);
            missing = true;

        checkU:
            U u;
            foreach (var d in ds)
            {
                if (d.Name != uName)
                {
                    continue;
                }

                if (!(d.Content is U))
                {
                    continue;
                }

                u = (U)d.Content;
                goto checkV;
            }

            u = default(U);
            missing = true;

        checkV:
            V v;
            foreach (var d in ds)
            {
                if (d.Name != vName)
                {
                    continue;
                }

                if (!(d.Content is V))
                {
                    continue;
                }

                v = (V)d.Content;
                goto invoke;
            }

            v = default(V);
            missing = true;

        invoke:
            if (!missing)
            {
                method?.Invoke(t, u, v);
            }

            return XTuple.Create(t, u, v);
        }

        public virtual XTuple<T, U, V, W> Run<T, U, V, W>(
            Do<T, U, V, W> method = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            var ds = this.dependencies;
            T t;
            var missing = false;
            foreach (var d in ds)
            {
                if (d.Name != tName)
                {
                    continue;
                }

                if (!(d.Content is T))
                {
                    continue;
                }

                t = (T)d.Content;
                goto checkU;
            }

            t = default(T);
            missing = true;

        checkU:
            U u;
            foreach (var d in ds)
            {
                if (d.Name != uName)
                {
                    continue;
                }

                if (!(d.Content is U))
                {
                    continue;
                }

                u = (U)d.Content;
                goto checkV;
            }

            u = default(U);
            missing = true;

        checkV:
            V v;
            foreach (var d in ds)
            {
                if (d.Name != vName)
                {
                    continue;
                }

                if (!(d.Content is V))
                {
                    continue;
                }

                v = (V)d.Content;
                goto checkW;
            }

            v = default(V);
            missing = true;

        checkW:
            W w;
            foreach (var d in ds)
            {
                if (d.Name != wName)
                {
                    continue;
                }

                if (!(d.Content is W))
                {
                    continue;
                }

                w = (W)d.Content;
                goto invoke;
            }

            w = default(W);
            missing = true;

        invoke:
            if (!missing)
            {
                method?.Invoke(t, u, v, w);
            }

            return XTuple.Create(t, u, v, w);
        }

        protected readonly ICollection<Dependency> dependencies;

        protected class Dependency
        {
            public virtual string Name { get; set; }

            public virtual object Content { get; set; }
        }
    }
}
