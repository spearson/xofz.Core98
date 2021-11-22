namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class MethodWebManagerV2
        : MethodWebManager
    {
        public MethodWebManagerV2()
        {
            this.locker = new object();
        }

        protected MethodWebManagerV2(
            ICollection<NamedMethodWebHolder> webs)
            : this(webs, new object())
        {
        }

        protected MethodWebManagerV2(
            object locker)
        {
            this.locker = locker;
        }

        protected MethodWebManagerV2(
            ICollection<NamedMethodWebHolder> webs,
            object locker)
            : base(webs)
        {
            this.locker = locker;
        }

        public override Lot<string> WebNames()
        {
            var lll = new XLinkedListLot<string>();
            lock (this.locker)
            {
                foreach (var webName in EH.Select(
                    this.webs,
                    webHolder => webHolder?.Name))
                {
                    lll.AddTail(
                        webName);
                }
            }

            return lll;
        }

        public override bool AddWeb(
            MethodWeb web,
            string name = null)
        {
            if (web == null)
            {
                return falsity;
            }

            ICollection<NamedMethodWebHolder> ws;
            lock (this.locker)
            {
                ws = this.webs;
                if (EH.Contains(
                    EH.Select(
                        ws,
                        webHolder => webHolder?.Name),
                    name))
                {
                    return falsity;
                }
            }

            this.add(
                new NamedMethodWebHolder
                {
                    Web = web,
                    Name = name
                });

            return truth;
        }

        protected override void add(
            NamedMethodWebHolder holder)
        {
            lock (this.locker)
            {
                this.webs?.Add(holder);
            }
        }

        public virtual bool RemoveWeb(
            string webName)
        {
            ICollection<NamedMethodWebHolder> ws;
            NamedMethodWebHolder targetHolder;
            bool removed;
            lock (this.locker)
            {
                ws = this.webs;
                targetHolder = EH.FirstOrNull(
                    ws,
                    webHolder => webHolder?.Name == webName);
                removed = ws?.Remove(targetHolder)
                    ?? falsity;
            }

            return removed;
        }

        public override MethodWeb AccessWeb(
            Do<MethodWeb> accessor = null,
            string webName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                        this.webs,
                        webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return web;
            }

            accessor?.Invoke(web);
            return web;
        }

        public override T AccessWeb<T>(
            Do<T> accessor = null,
            string webName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                        this.webs,
                        webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web as T;
            if (web == null)
            {
                return web;
            }

            accessor?.Invoke(web);
            return web;
        }

        

        public override T RunWeb<T>(
            Do<T> engine = null,
            string webName = null,
            string dependencyName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                        this.webs,
                        webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return default;
            }

            return web.Run(
                engine,
                dependencyName);
        }

        public override XTuple<T, U> RunWeb<T, U>(
            Do<T, U> engine = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                        this.webs,
                        webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U));
            }

            return web.Run(
                engine,
                tName,
                uName);
        }

        public override XTuple<T, U, V> RunWeb<T, U, V>(
            Do<T, U, V> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                        this.webs,
                        webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return web.Run(
                engine,
                tName,
                uName,
                vName);
        }

        public override XTuple<T, U, V, W> RunWeb<T, U, V, W>(
            Do<T, U, V, W> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.webs,
                    webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return web.Run(
                engine,
                tName,
                uName,
                vName,
                wName);
        }

        public override XTuple<T, U, V, W, X> RunWeb<T, U, V, W, X>(
            Do<T, U, V, W, X> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.webs,
                    webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X));
            }

            return web.Run(
                engine,
                tName,
                uName,
                vName,
                wName,
                xName);
        }

        public override XTuple<T, U, V, W, X, Y> RunWeb<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.webs,
                    webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y));
            }

            return web.Run(
                engine,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName);
        }

        public override XTuple<T, U, V, W, X, Y, Z> RunWeb<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.webs,
                    webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y),
                    default(Z));
            }

            return web.Run(
                engine,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName,
                zName);
        }

        public override XTuple<T, U, V, W, X, Y, Z, AA> RunWeb<T, U, V, W, X, Y,
            Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null,
            string aaName = null)
        {
            NamedMethodWebHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.webs,
                    webHolder => webHolder?.Name == webName);
            }

            var web = targetHolder?.Web;
            if (web == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y),
                    default(Z),
                    default(AA));
            }

            return web.Run(
                engine,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName,
                zName,
                aaName);
        }

        protected readonly object locker;
    }
}
