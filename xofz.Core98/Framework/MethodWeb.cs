namespace xofz.Framework
{
    using System.Collections.Generic;

    public class MethodWeb
        : MethodRunner
    {
        public MethodWeb()
            : this(new LinkedList<Dependency>())
        {
        }

        protected MethodWeb(
            ICollection<Dependency> dependencies)
        {
            this.dependencies = dependencies;
        }

        public virtual bool RegisterDependency(
            object dependency,
            string name = null)
        {
            if (dependency == null)
            {
                return false;
            }

            this.dependencies.Add(
                new Dependency
                {
                    Content = dependency,
                    Name = name
                });
            return true;
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

                var content = d.Content;
                if (!(content is T))
                {
                    continue;
                }

                t = (T)content;
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
            var t = default(T);
            var u = default(U);
            bool
                tMissing = true,
                uMissing = true;
            foreach (var d in ds)
            {
                if (!tMissing && !uMissing)
                {
                    goto invoke;
                }

                if (tMissing)
                {
                    if (d.Name != tName)
                    {
                        goto checkU;
                    }

                    var content = d.Content;
                    if (!(content is T))
                    {
                        goto checkU;
                    }

                    t = (T)content;
                    tMissing = false;
                    continue;
                }

                checkU:
                if (uMissing)
                {
                    if (d.Name != uName)
                    {
                        continue;
                    }

                    var content = d.Content;
                    if (!(content is U))
                    {
                        continue;
                    }

                    u = (U)content;
                    uMissing = false;
                }
            }

            if (!tMissing && !uMissing)
            {
                goto invoke;
            }

            return XTuple.Create(t, u);

            invoke:
            method?.Invoke(t, u);

            return XTuple.Create(t, u);
        }

        public virtual XTuple<T, U, V> Run<T, U, V>(
            Do<T, U, V> method = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            var ds = this.dependencies;
            var t = default(T);
            var u = default(U);
            var v = default(V);
            bool
                tMissing = true,
                uMissing = true,
                vMissing = true;
            foreach (var d in ds)
            {
                if (!tMissing && !uMissing && !vMissing)
                {
                    goto invoke;
                }

                if (tMissing)
                {
                    if (d.Name != tName)
                    {
                        goto checkU;
                    }

                    var content = d.Content;
                    if (!(content is T))
                    {
                        goto checkU;
                    }

                    t = (T)content;
                    tMissing = false;
                    continue;
                }

                checkU:
                if (uMissing)
                {
                    if (d.Name != uName)
                    {
                        goto checkV;
                    }

                    var content = d.Content;
                    if (!(content is U))
                    {
                        goto checkV;
                    }

                    u = (U)content;
                    uMissing = false;
                    continue;
                }

                checkV:
                if (vMissing)
                {
                    if (d.Name != vName)
                    {
                        continue;
                    }

                    var content = d.Content;
                    if (!(content is V))
                    {
                        continue;
                    }

                    v = (V)content;
                    vMissing = false;
                }
            }

            if (!tMissing && !uMissing && !vMissing)
            {
                goto invoke;
            }

            return XTuple.Create(t, u, v);

            invoke:
            method?.Invoke(t, u, v);

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
            var t = default(T);
            var u = default(U);
            var v = default(V);
            var w = default(W);
            bool
                tMissing = true,
                uMissing = true,
                vMissing = true,
                wMissing = true;
            foreach (var d in ds)
            {
                if (!tMissing && !uMissing && !vMissing && !wMissing)
                {
                    goto invoke;
                }

                if (tMissing)
                {
                    if (d.Name != tName)
                    {
                        goto checkU;
                    }

                    var content = d.Content;
                    if (!(content is T))
                    {
                        goto checkU;
                    }

                    t = (T)content;
                    tMissing = false;
                    continue;
                }

                checkU:
                if (uMissing)
                {
                    if (d.Name != uName)
                    {
                        goto checkV;
                    }

                    var content = d.Content;
                    if (!(content is U))
                    {
                        goto checkV;
                    }

                    u = (U)content;
                    uMissing = false;
                    continue;
                }

                checkV:
                if (vMissing)
                {
                    if (d.Name != vName)
                    {
                        goto checkW;
                    }

                    var content = d.Content;
                    if (!(content is V))
                    {
                        goto checkW;
                    }

                    v = (V)content;
                    vMissing = false;
                    continue;
                }

                checkW:
                if (wMissing)
                {
                    if (d.Name != wName)
                    {
                        continue;
                    }

                    var content = d.Content;
                    if (!(content is W))
                    {
                        continue;
                    }

                    w = (W)content;
                    wMissing = false;
                }
            }

            if (!tMissing && !uMissing && !vMissing && !wMissing)
            {
                goto invoke;
            }

            return XTuple.Create(t, u, v, w);

            invoke:
            method?.Invoke(t, u, v, w);

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