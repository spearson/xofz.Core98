namespace xofz.Framework
{
    using System.Collections.Generic;

    public class ThreadSafeMethodWeb : MethodWeb
    {
        public ThreadSafeMethodWeb()
        {
            this.locker = new object();
        }

        protected ThreadSafeMethodWeb(
            ICollection<Dependency> dependencies)
            : this(dependencies, new object())
        {
        }

        protected ThreadSafeMethodWeb(
            object locker)
            : this(new LinkedList<Dependency>(), locker)
        {
        }

        protected ThreadSafeMethodWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies)
        {
            this.locker = locker;
        }

        public override bool RegisterDependency(
            object dependency,
            string name = null)
        {
            if (dependency == null)
            {
                return false;
            }

            lock (this.locker)
            {
                this.dependencies.Add(
                    new Dependency
                    {
                        Content = dependency,
                        Name = name
                    });
            }

            return true;
        }

        public virtual bool Unregister<T>(
            string name = null)
        {
            var ds = this.dependencies;
            var unregistered = false;
            lock (this.locker)
            {
                foreach (var d in ds)
                {
                    if (d.Name != name)
                    {
                        continue;
                    }

                    if (!(d.Content is T))
                    {
                        continue;
                    }

                    ds.Remove(d);
                    unregistered = true;
                    break;
                }
            }

            return unregistered;
        }

        public override T Run<T>(
            Do<T> method = null,
            string dependencyName = null)
        {
            var ds = this.dependencies;
            T t;
            lock (this.locker)
            {
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
            }

            return default(T);

            invoke:
            method?.Invoke(t);

            return t;
        }

        public override XTuple<T, U> Run<T, U>(
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
            lock (this.locker)
            {
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

                        if (!(d.Content is T))
                        {
                            goto checkU;
                        }

                        t = (T)d.Content;
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

                        if (!(d.Content is U))
                        {
                            continue;
                        }

                        u = (U)d.Content;
                        uMissing = false;
                    }
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

        public override XTuple<T, U, V> Run<T, U, V>(
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
            lock (this.locker)
            {
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

                        if (!(d.Content is T))
                        {
                            goto checkU;
                        }

                        t = (T)d.Content;
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

                        if (!(d.Content is U))
                        {
                            goto checkV;
                        }

                        u = (U)d.Content;
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

                        if (!(d.Content is V))
                        {
                            continue;
                        }

                        v = (V)d.Content;
                        vMissing = false;
                    }
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

        public override XTuple<T, U, V, W> Run<T, U, V, W>(
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
            lock (this.locker)
            {
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

                        if (!(d.Content is T))
                        {
                            goto checkU;
                        }

                        t = (T)d.Content;
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

                        if (!(d.Content is U))
                        {
                            goto checkV;
                        }

                        u = (U)d.Content;
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

                        if (!(d.Content is V))
                        {
                            goto checkW;
                        }

                        v = (V)d.Content;
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

                        if (!(d.Content is W))
                        {
                            continue;
                        }

                        w = (W)d.Content;
                        wMissing = false;
                    }
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

        protected readonly object locker;
    }
}