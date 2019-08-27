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

        public override XTuple<T, U, V, W, X> Run<T, U, V, W, X>(
            Do<T, U, V, W, X> method = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            var ds = this.dependencies;
            var t = default(T);
            var u = default(U);
            var v = default(V);
            var w = default(W);
            var x = default(X);
            bool
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false,
                xFound = false;
            lock (this.locker)
            {
                foreach (var d in ds)
                {
                    if (tFound && uFound && vFound && wFound && xFound)
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
                            goto checkX;
                        }

                        if (!(content is W))
                        {
                            goto checkX;
                        }

                        w = (W)content;
                        wFound = true;
                        continue;
                    }

                    checkX:
                    if (!xFound)
                    {
                        if (name != xName)
                        {
                            continue;
                        }

                        if (!(content is X))
                        {
                            continue;
                        }

                        x = (X)content;
                        xFound = true;
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
            var t = default(T);
            var u = default(U);
            var v = default(V);
            var w = default(W);
            var x = default(X);
            var y = default(Y);
            bool
                tFound = false,
                uFound = false,
                vFound = false,
                wFound = false,
                xFound = false,
                yFound = false;
            lock (this.locker)
            {
                foreach (var d in ds)
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
                            goto checkX;
                        }

                        if (!(content is W))
                        {
                            goto checkX;
                        }

                        w = (W)content;
                        wFound = true;
                        continue;
                    }

                    checkX:
                    if (!xFound)
                    {
                        if (name != xName)
                        {
                            goto checkY;
                        }

                        if (!(content is X))
                        {
                            goto checkY;
                        }

                        x = (X)content;
                        xFound = true;
                        continue;
                    }

                    checkY:
                    if (!yFound)
                    {
                        if (name != yName)
                        {
                            continue;
                        }

                        if (!(content is Y))
                        {
                            continue;
                        }

                        y = (Y)content;
                        yFound = true;
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
            var t = default(T);
            var u = default(U);
            var v = default(V);
            var w = default(W);
            var x = default(X);
            var y = default(Y);
            var z = default(Z);
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
                foreach (var d in ds)
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
                            goto checkX;
                        }

                        if (!(content is W))
                        {
                            goto checkX;
                        }

                        w = (W)content;
                        wFound = true;
                        continue;
                    }

                    checkX:
                    if (!xFound)
                    {
                        if (name != xName)
                        {
                            goto checkY;
                        }

                        if (!(content is X))
                        {
                            goto checkY;
                        }

                        x = (X)content;
                        xFound = true;
                        continue;
                    }

                    checkY:
                    if (!yFound)
                    {
                        if (name != yName)
                        {
                            goto checkZ;
                        }

                        if (!(content is Y))
                        {
                            goto checkZ;
                        }

                        y = (Y)content;
                        yFound = true;
                        continue;
                    }

                    checkZ:
                    if (!zFound)
                    {
                        if (name != zName)
                        {
                            continue;
                        }

                        if (!(content is Z))
                        {
                            continue;
                        }

                        z = (Z)content;
                        zFound = true;
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
            var t = default(T);
            var u = default(U);
            var v = default(V);
            var w = default(W);
            var x = default(X);
            var y = default(Y);
            var z = default(Z);
            var aa = default(AA);
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
                foreach (var d in ds)
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
                            goto checkX;
                        }

                        if (!(content is W))
                        {
                            goto checkX;
                        }

                        w = (W)content;
                        wFound = true;
                        continue;
                    }

                    checkX:
                    if (!xFound)
                    {
                        if (name != xName)
                        {
                            goto checkY;
                        }

                        if (!(content is X))
                        {
                            goto checkY;
                        }

                        x = (X)content;
                        xFound = true;
                        continue;
                    }

                    checkY:
                    if (!yFound)
                    {
                        if (name != yName)
                        {
                            goto checkZ;
                        }

                        if (!(content is Y))
                        {
                            goto checkZ;
                        }

                        y = (Y)content;
                        yFound = true;
                        continue;
                    }

                    checkZ:
                    if (!zFound)
                    {
                        if (name != zName)
                        {
                            goto checkAA;
                        }

                        if (!(content is Z))
                        {
                            goto checkAA;
                        }

                        z = (Z)content;
                        zFound = true;
                        continue;
                    }

                    checkAA:
                    if (!aaFound)
                    {
                        if (name != aaName)
                        {
                            continue;
                        }

                        if (!(content is AA))
                        {
                            continue;
                        }

                        aa = (AA)content;
                        aaFound = true;
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