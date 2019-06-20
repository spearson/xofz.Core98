namespace xofz.Framework
{
    using System.Collections.Generic;

    public class ThreadSafeMethodWeb 
        : MethodWeb
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
        {
            this.locker = locker;
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

                    var content = d.Content;
                    if (!(content is T))
                    {
                        continue;
                    }

                    t = (T)content;
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
                tFound = false,
                uFound = false;
            lock (this.locker)
            {
                foreach (var d in ds)
                {
                    if (tFound && uFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
                    if (!tFound)
                    {
                        if (name != tName)
                        {
                            goto checkU;
                        }

                        if (!(content is T))
                        {
                            goto checkU;
                        }

                        t = (T)content;
                        tFound = true;
                        continue;
                    }

                    checkU:
                    if (!uFound)
                    {
                        if (name != uName)
                        {
                            continue;
                        }

                        if (!(content is U))
                        {
                            continue;
                        }

                        u = (U)content;
                        uFound = true;
                    }
                }
            }

            if (tFound && uFound)
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
                tFound = false,
                uFound = false,
                vFound = false;
            lock (this.locker)
            {
                foreach (var d in ds)
                {
                    if (tFound && uFound && vFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
                    if (!tFound)
                    {
                        if (name != tName)
                        {
                            goto checkU;
                        }

                        if (!(content is T))
                        {
                            goto checkU;
                        }

                        t = (T)content;
                        tFound = true;
                        continue;
                    }

                    checkU:
                    if (!uFound)
                    {
                        if (name != uName)
                        {
                            goto checkV;
                        }

                        if (!(content is U))
                        {
                            goto checkV;
                        }

                        u = (U)content;
                        uFound = true;
                        continue;
                    }

                    checkV:
                    if (!vFound)
                    {
                        if (name != vName)
                        {
                            continue;
                        }

                        if (!(content is V))
                        {
                            continue;
                        }

                        v = (V)content;
                        vFound = true;
                    }
                }
            }

            if (tFound && uFound && vFound)
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
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false;
            lock (this.locker)
            {
                foreach (var d in ds)
                {
                    if (tFound && uFound && vFound && wFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
                    if (!tFound)
                    {
                        if (name != tName)
                        {
                            goto checkU;
                        }

                        if (!(content is T))
                        {
                            goto checkU;
                        }

                        t = (T)content;
                        tFound = true;
                        continue;
                    }

                    checkU:
                    if (!uFound)
                    {
                        if (name != uName)
                        {
                            goto checkV;
                        }

                        if (!(content is U))
                        {
                            goto checkV;
                        }

                        u = (U)content;
                        uFound = true;
                        continue;
                    }

                    checkV:
                    if (!vFound)
                    {
                        if (name != vName)
                        {
                            goto checkW;
                        }

                        if (!(content is V))
                        {
                            goto checkW;
                        }

                        v = (V)content;
                        vFound = true;
                        continue;
                    }

                    checkW:
                    if (!wFound)
                    {
                        if (name != wName)
                        {
                            continue;
                        }

                        if (!(content is W))
                        {
                            continue;
                        }

                        w = (W)content;
                        wFound = true;
                    }
                }
            }

            if (tFound && uFound && vFound && wFound)
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