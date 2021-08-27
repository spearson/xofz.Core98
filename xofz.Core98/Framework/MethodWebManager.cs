namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class MethodWebManager
        : MultiWebRunner
    {
        public MethodWebManager()
            : this(new XLinkedList<NamedMethodWebHolder>())
        {
        }

        protected MethodWebManager(
            ICollection<NamedMethodWebHolder> webs)
        {
            this.webs = webs;
        }

        public virtual Lot<string> WebNames()
        {
            return new LinkedListLot<string>(
                EH.Select(
                    this.webs,
                    webHolder => webHolder?.Name));
        }

        public virtual bool AddWeb(
            MethodWeb web,
            string name = null)
        {
            if (web == null)
            {
                return falsity;
            }

            var ws = this.webs;
            var alreadyAddedWeb = EH.FirstOrNull(
                ws,
                webHolder => ReferenceEquals(
                    web,
                    webHolder?.Web));
            if (alreadyAddedWeb != null)
            {
                return falsity;
            }

            if (EH.Contains(
                EH.Select(
                    ws, 
                    webHolder => webHolder?.Name),
                name))
            {
                return falsity;
            }

            this.add(
                new NamedMethodWebHolder
                {
                    Web = web,
                    Name = name
                });
            return truth;
        }

        protected virtual void add(
            NamedMethodWebHolder holder)
        {
            this.webs?.Add(
                holder);
        }

        public virtual MethodWeb AccessWeb(
            Do<MethodWeb> accessor = null,
            string webName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

            var web = targetHolder?.Web;
            if (web == null)
            {
                return null;
            }

            accessor?.Invoke(web);
            return web;
        }

        public virtual T AccessWeb<T>(
            Do<T> accessor = null,
            string webName = null)
            where T : MethodWeb
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

            var web = targetHolder?.Web as T;
            if (web == null)
            {
                return null;
            }

            accessor?.Invoke(web);
            return web;
        }

        public virtual T RunWeb<T>(
            xofz.Do<T> engine = null,
            string webName = null,
            string dependencyName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

            var web = targetHolder?.Web;
            if (web == null)
            {
                return default;
            }

            return web.Run(
                engine, 
                dependencyName);
        }

        public virtual XTuple<T, U> RunWeb<T, U>(
            Do<T, U> engine = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

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

        public virtual XTuple<T, U, V> RunWeb<T, U, V>(
            Do<T, U, V> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

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

        public virtual XTuple<T, U, V, W> RunWeb<T, U, V, W>(
            Do<T, U, V, W> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

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

        public virtual XTuple<T, U, V, W, X> RunWeb<T, U, V, W, X>(
            Do<T, U, V, W, X> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

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

        public virtual XTuple<T, U, V, W, X, Y> RunWeb<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

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

        public virtual XTuple<T, U, V, W, X, Y, Z> RunWeb<T, U, V, W, X, Y, Z>(
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
            var targetHolder
                = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);

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

        public virtual XTuple<T, U, V, W, X, Y, Z, AA> RunWeb<T, U, V, W, X, Y,
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
            var targetHolder = EH.FirstOrNull(
                this.webs,
                webHolder => webHolder?.Name == webName);
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

        protected readonly ICollection<NamedMethodWebHolder> webs;

        protected class NamedMethodWebHolder
        {
            public virtual MethodWeb Web { get; set; }

            public virtual string Name { get; set; }
        }

        protected const bool
            truth = true,
            falsity = false;
    }
}
