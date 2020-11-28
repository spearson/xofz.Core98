namespace xofz.Framework
{
    using System.Collections.Generic;
    using EH = xofz.EnumerableHelpers;

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

            var d = new Dependency
            {
                Content = dependency,
                Name = name
            };

            lock (this.locker ?? new object())
            {
                this.dependencies.Add(d);
            }

            return true;
        }

        public virtual bool Unregister<T>(
            string name = null)
        {
            var ds = this.dependencies
                ?? new LinkedList<Dependency>();
            var unregistered = false;
            lock (this.locker ?? new object())
            {
                foreach (var d in ds)
                {
                    if (this.tryGet(
                        d.Content,
                        d.Name,
                        name,
                        out T _))
                    {
                        ds.Remove(d);
                        unregistered = true;
                        break;
                    }
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
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (this.tryGet(
                        d?.Content,
                        d?.Name,
                        dependencyName,
                        out t))
                    {
                        goto invoke;
                    }
                }
            }

            return default;

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
            T t = default;
            U u = default;
            bool
                tFound = false,
                uFound = false;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (tFound && uFound)
                    {
                        goto invoke;
                    }

                    var name = d?.Name;
                    var content = d?.Content;
                    if (!tFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            tName,
                            out t))
                        {
                            tFound = true;
                            continue;
                        }
                    }

                    if (!uFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            uName,
                            out u))
                        {
                            uFound = true;
                        }
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
            T t = default;
            U u = default;
            V v = default;
            bool
                tFound = false,
                uFound = false,
                vFound = false;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (tFound && uFound && vFound)
                    {
                        goto invoke;
                    }

                    var name = d?.Name;
                    var content = d?.Content;
                    if (!tFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            tName,
                            out t))
                        {
                            tFound = true;
                            continue;
                        }
                    }

                    if (!uFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            uName,
                            out u))
                        {
                            uFound = true;
                            continue;
                        }
                    }

                    if (!vFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            vName,
                            out v))
                        {
                            vFound = true;
                        }
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
            T t = default;
            U u = default;
            V v = default;
            W w = default;
            bool
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (tFound && uFound && vFound && wFound)
                    {
                        goto invoke;
                    }

                    var name = d?.Name;
                    var content = d?.Content;
                    if (!tFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            tName,
                            out t))
                        {
                            tFound = true;
                            continue;
                        }
                    }

                    if (!uFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            uName,
                            out u))
                        {
                            uFound = true;
                            continue;
                        }
                    }

                    if (!vFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            vName,
                            out v))
                        {
                            vFound = true;
                            continue;
                        }
                    }

                    if (!wFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            wName,
                            out w))
                        {
                            wFound = true;
                        }
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

        public override XTuple<T, U, V, W, X> Run<T, U, V, W, X>(
            Do<T, U, V, W, X> method = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            var ds = this.dependencies;
            T t = default;
            U u = default;
            V v = default;
            W w = default;
            X x = default;
            bool
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false,
                xFound = false;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (tFound && uFound && vFound && wFound && xFound)
                    {
                        goto invoke;
                    }

                    var name = d?.Name;
                    var content = d?.Content;
                    if (!tFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            tName,
                            out t))
                        {
                            tFound = true;
                            continue;
                        }
                    }

                    if (!uFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            uName,
                            out u))
                        {
                            uFound = true;
                            continue;
                        }
                    }

                    if (!vFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            vName,
                            out v))
                        {
                            vFound = true;
                            continue;
                        }
                    }

                    if (!wFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            wName,
                            out w))
                        {
                            wFound = true;
                            continue;
                        }
                    }

                    if (!xFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            xName,
                            out x))
                        {
                            xFound = true;
                        }
                    }
                }
            }

            if (tFound && uFound && vFound && wFound && xFound)
            {
                goto invoke;
            }

            return XTuple.Create(t, u, v, w, x);

            invoke:
            method?.Invoke(t, u, v, w, x);

            return XTuple.Create(t, u, v, w, x);
        }

        public override XTuple<T, U, V, W, X, Y> Run<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> method = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null)
        {
            var ds = this.dependencies;
            T t = default;
            U u = default;
            V v = default;
            W w = default;
            X x = default;
            Y y = default;
            bool
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false,
                xFound = false,
                yFound = false;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (tFound && uFound && vFound && wFound && xFound &&
                        yFound)
                    {
                        goto invoke;
                    }

                    var name = d?.Name;
                    var content = d?.Content;
                    if (!tFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            tName,
                            out t))
                        {
                            tFound = true;
                            continue;
                        }
                    }

                    if (!uFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            uName,
                            out u))
                        {
                            uFound = true;
                            continue;
                        }
                    }

                    if (!vFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            vName,
                            out v))
                        {
                            vFound = true;
                            continue;
                        }
                    }

                    if (!wFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            wName,
                            out w))
                        {
                            wFound = true;
                            continue;
                        }
                    }

                    if (!xFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            xName,
                            out x))
                        {
                            xFound = true;
                            continue;
                        }
                    }

                    if (!yFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            yName,
                            out y))
                        {
                            yFound = true;
                        }
                    }
                }
            }

            if (tFound && uFound && vFound && wFound && xFound && yFound)
            {
                goto invoke;
            }

            return XTuple.Create(t, u, v, w, x, y);

            invoke:
            method?.Invoke(t, u, v, w, x, y);

            return XTuple.Create(t, u, v, w, x, y);
        }

        public override XTuple<T, U, V, W, X, Y, Z> Run<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> method = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null)
        {
            var ds = this.dependencies;
            T t = default;
            U u = default;
            V v = default;
            W w = default;
            X x = default;
            Y y = default;
            Z z = default;
            bool
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false,
                xFound = false,
                yFound = false,
                zFound = false;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (tFound && uFound && vFound && wFound && xFound &&
                        yFound && zFound)
                    {
                        goto invoke;
                    }

                    var name = d?.Name;
                    var content = d?.Content;
                    if (!tFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            tName,
                            out t))
                        {
                            tFound = true;
                            continue;
                        }
                    }

                    if (!uFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            uName,
                            out u))
                        {
                            uFound = true;
                            continue;
                        }
                    }

                    if (!vFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            vName,
                            out v))
                        {
                            vFound = true;
                            continue;
                        }
                    }

                    if (!wFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            wName,
                            out w))
                        {
                            wFound = true;
                            continue;
                        }
                    }

                    if (!xFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            xName,
                            out x))
                        {
                            xFound = true;
                            continue;
                        }
                    }

                    if (!yFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            yName,
                            out y))
                        {
                            yFound = true;
                            continue;
                        }
                    }

                    if (!zFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            zName,
                            out z))
                        {
                            zFound = true;
                        }
                    }
                }
            }

            if (tFound && uFound && vFound && wFound && xFound && yFound &&
                zFound)
            {
                goto invoke;
            }

            return XTuple.Create(t, u, v, w, x, y, z);

            invoke:
            method?.Invoke(t, u, v, w, x, y, z);

            return XTuple.Create(t, u, v, w, x, y, z);
        }

        public override XTuple<T, U, V, W, X, Y, Z, AA> Run<T, U, V, W, X, Y, Z,
            AA>(
            Do<T, U, V, W, X, Y, Z, AA> method = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null,
            string aaName = null)
        {
            var ds = this.dependencies;
            T t = default;
            U u = default;
            V v = default;
            W w = default;
            X x = default;
            Y y = default;
            Z z = default;
            AA aa = default;
            bool
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false,
                xFound = false,
                yFound = false,
                zFound = false,
                aaFound = false;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
                {
                    if (tFound && uFound && vFound && wFound && xFound &&
                        yFound && zFound && aaFound)
                    {
                        goto invoke;
                    }

                    var name = d?.Name;
                    var content = d?.Content;
                    if (!tFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            tName,
                            out t))
                        {
                            tFound = true;
                            continue;
                        }
                    }

                    if (!uFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            uName,
                            out u))
                        {
                            uFound = true;
                            continue;
                        }
                    }

                    if (!vFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            vName,
                            out v))
                        {
                            vFound = true;
                            continue;
                        }
                    }

                    if (!wFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            wName,
                            out w))
                        {
                            wFound = true;
                            continue;
                        }
                    }

                    if (!xFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            xName,
                            out x))
                        {
                            xFound = true;
                            continue;
                        }
                    }

                    if (!yFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            yName,
                            out y))
                        {
                            yFound = true;
                            continue;
                        }
                    }

                    if (!zFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            zName,
                            out z))
                        {
                            zFound = true;
                            continue;
                        }
                    }

                    if (!aaFound)
                    {
                        if (this.tryGet(
                            content,
                            name,
                            aaName,
                            out aa))
                        {
                            aaFound = true;
                        }
                    }
                }
            }

            if (tFound && uFound && vFound && wFound && xFound && yFound &&
                zFound && aaFound)
            {
                goto invoke;
            }

            return XTuple.Create(t, u, v, w, x, y, z, aa);

            invoke:
            method?.Invoke(t, u, v, w, x, y, z, aa);

            return XTuple.Create(t, u, v, w, x, y, z, aa);
        }

        protected readonly object locker;
    }
}