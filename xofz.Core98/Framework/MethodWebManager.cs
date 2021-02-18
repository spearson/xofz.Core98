namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class MethodWebManager
        : MultiWebRunner
    {
        public MethodWebManager()
            : this(new LinkedList<NamedMethodWebHolder>())
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
                    nmwh => nmwh?.Name));
        }

        public virtual bool AddWeb(
            MethodWeb web,
            string name = null)
        {
            if (web == null)
            {
                return false;
            }

            var ws = this.webs;
            var alreadyAddedWeb = EH.FirstOrDefault(
                ws,
                nmwh => ReferenceEquals(
                    web,
                    nmwh?.Web));
            if (alreadyAddedWeb != null)
            {
                return false;
            }

            if (EH.Contains(
                EH.Select(
                    ws, nmwh => nmwh?.Name),
                name))
            {
                return false;
            }

            ws.Add(
                new NamedMethodWebHolder
                {
                    Web = web,
                    Name = name
                });
            return true;
        }

        public virtual MethodWeb AccessWeb(
            Do<MethodWeb> accessor = null,
            string webName = null)
        {
            var innerWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (innerWeb == null)
            {
                return null;
            }

            accessor?.Invoke(innerWeb);
            return innerWeb;
        }

        public virtual T AccessWeb<T>(
            Do<T> accessor = null,
            string webName = null)
            where T : MethodWeb
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName);

            var innerWeb = targetWeb?.Web as T;
            if (innerWeb == null)
            {
                return null;
            }

            accessor?.Invoke(innerWeb);
            return innerWeb;
        }

        public virtual T RunWeb<T>(
            xofz.Do<T> engine = null,
            string webName = null,
            string dependencyName = null)
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
            {
                return default;
            }

            return targetWeb.Run(
                engine, 
                dependencyName);
        }

        public virtual XTuple<T, U> RunWeb<T, U>(
            Do<T, U> engine = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U));
            }

            return targetWeb.Run(
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
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return targetWeb.Run(
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
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return targetWeb.Run(
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
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X));
            }

            return targetWeb.Run(
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
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y));
            }

            return targetWeb.Run(
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
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
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

            return targetWeb.Run(
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
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh?.Name == webName)?.Web;
            if (targetWeb == null)
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

            return targetWeb.Run(
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
    }
}
