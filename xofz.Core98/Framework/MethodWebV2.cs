namespace xofz.Framework
{
    using System.Collections.Generic;

    public class MethodWebV2
        : MethodWeb
    {
        public MethodWebV2()
            : base(null)
        {
        }

        protected MethodWebV2(
            object locker)
            : base(null, locker)
        {
        }

        protected MethodWebV2(
            ICollection<Dependency> dependencies,
            object locker = null)
            : base(dependencies, locker)
        {
        }

        public virtual bool Unregister<T>(
            string name = null)
        {
            var unregistered = falsity;
            var ds = this.dependencies;
            lock (this.locker)
            {
                foreach (var d in ds)
                {
                    if (this.tryGet(
                            d.Content,
                            d.Name,
                            name,
                            out T _))
                    {
                        unregistered = ds.Remove(d);
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
            T t;
            lock (this.locker)
            {
                foreach (var d in this.dependencies)
                {
                    if (this.tryGet(
                        d.Content,
                        d.Name,
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
            T t = default;
            U u = default;
            bool
                tFound = falsity,
                uFound = falsity;
            lock (this.locker)
            {
                foreach (var d in this.dependencies)
                {
                    if (tFound && uFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
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
            T t = default;
            U u = default;
            V v = default;
            bool
                tFound = falsity,
                uFound = falsity,
                vFound = falsity;
            lock (this.locker)
            {
                foreach (var d in this.dependencies)
                {
                    if (tFound && uFound && vFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
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
                foreach (var d in this.dependencies)
                {
                    if (tFound && uFound && vFound && wFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
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
                foreach (var d in this.dependencies)
                {
                    if (tFound && uFound && vFound && wFound && xFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
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
                foreach (var d in this.dependencies)
                {
                    if (tFound && uFound && vFound && wFound && xFound &&
                        yFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
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
                foreach (var d in this.dependencies)
                {
                    if (tFound && uFound && vFound && wFound && xFound &&
                        yFound && zFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
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
                foreach (var d in this.dependencies)
                {
                    if (tFound && uFound && vFound && wFound && xFound &&
                        yFound && zFound && aaFound)
                    {
                        goto invoke;
                    }

                    var name = d.Name;
                    var content = d.Content;
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
    }
}