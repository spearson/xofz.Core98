namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class MethodWebV2
        : MethodWeb
    {
        public MethodWebV2()
        {
            this.locker = new object();
        }

        protected MethodWebV2(
            ICollection<Dependency> dependencies)
            : this(dependencies, new object())
        {
        }

        protected MethodWebV2(
            object locker)
        {
            this.locker = locker;
        }

        protected MethodWebV2(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies)
        {
            this.locker = locker;
        }

        protected override void register(
            Dependency dependency)
        {
            lock (this.locker)
            {
                this.dependencies?.Add(
                    dependency);
            }
        }

        public virtual bool Unregister<T>(
            string name = null)
        {
            var ds = this.dependencies;
            var unregistered = falsity;
            lock (this.locker)
            {
                foreach (var d in ds
                                  ?? EH.Empty<Dependency>())
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
                tFound = falsity,
                uFound = falsity;
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
                            tFound = truth;
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
                            uFound = truth;
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
                tFound = falsity,
                uFound = falsity,
                vFound = falsity;
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
                            tFound = truth;
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
                            uFound = truth;
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
                            vFound = truth;
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
                tFound = falsity,
                uFound = falsity,
                vFound = falsity,
                wFound = falsity;
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
                            tFound = truth;
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
                            uFound = truth;
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
                            vFound = truth;
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
                            wFound = truth;
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
                tFound = falsity,
                uFound = falsity,
                vFound = falsity,
                wFound = falsity,
                xFound = falsity;
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
                            tFound = truth;
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
                            uFound = truth;
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
                            vFound = truth;
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
                            wFound = truth;
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
                            xFound = truth;
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
                tFound = falsity,
                uFound = falsity,
                vFound = falsity,
                wFound = falsity,
                xFound = falsity,
                yFound = falsity;
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
                            tFound = truth;
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
                            uFound = truth;
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
                            vFound = truth;
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
                            wFound = truth;
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
                            xFound = truth;
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
                            yFound = truth;
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
                tFound = falsity,
                uFound = falsity,
                vFound = falsity,
                wFound = falsity,
                xFound = falsity,
                yFound = falsity,
                zFound = falsity;
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
                            tFound = truth;
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
                            uFound = truth;
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
                            vFound = truth;
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
                            wFound = truth;
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
                            xFound = truth;
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
                            yFound = truth;
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
                            zFound = truth;
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
                tFound = falsity,
                uFound = falsity,
                vFound = falsity,
                wFound = falsity,
                xFound = falsity,
                yFound = falsity,
                zFound = falsity,
                aaFound = falsity;
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
                            tFound = truth;
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
                            uFound = truth;
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
                            vFound = truth;
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
                            wFound = truth;
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
                            xFound = truth;
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
                            yFound = truth;
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
                            zFound = truth;
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
                            aaFound = truth;
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