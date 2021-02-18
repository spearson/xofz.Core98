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
            var lll = new LinkedListLot<string>();
            lock (this.locker ?? new object())
            {
                foreach (var webName in EH.Select(
                    this.webs,
                    nmwh => nmwh?.Name))
                {
                    lll.AddLast(
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
                return false;
            }

            ICollection<NamedMethodWebHolder> ws;
            NamedMethodWebHolder alreadyAddedWeb;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                alreadyAddedWeb = EH.FirstOrDefault(
                    ws,
                    nmwh => ReferenceEquals(web, nmwh?.Web));
            }

            if (alreadyAddedWeb != null)
            {
                return false;
            }

            lock (this.locker ?? new object())
            {
                if (EH.Contains(
                    EH.Select(
                        ws, 
                        nmwh => nmwh?.Name),
                    name))
                {
                    return false;
                }

                ws?.Add(
                    new NamedMethodWebHolder
                    {
                        Web = web,
                        Name = name
                    });
            }

            return true;
        }

        public override MethodWeb AccessWeb(
            Do<MethodWeb> accessor = null,
            string webName = null)
        {
            MethodWeb targetWeb;
            lock (this.locker ?? new object())
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh?.Name == webName)?.Web;
            }

            if (targetWeb == null)
            {
                return null;
            }

            accessor?.Invoke(targetWeb);
            return targetWeb;
        }

        public override T AccessWeb<T>(
            Do<T> accessor = null,
            string webName = null)
        {
            ICollection<NamedMethodWebHolder> ws;
            T targetWeb;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                targetWeb = EH.FirstOrDefault(
                    ws,
                    nmwh => nmwh?.Name == webName)?.Web as T;
            }

            if (targetWeb == null)
            {
                return targetWeb;
            }

            accessor?.Invoke(
                targetWeb);
            return targetWeb;
        }

        public virtual bool RemoveWeb(
            string webName)
        {
            ICollection<NamedMethodWebHolder> ws;
            NamedMethodWebHolder targetWeb;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                targetWeb = EH.FirstOrNull(
                    ws,
                    nmwh => nmwh?.Name == webName);

                if (targetWeb == null)
                {
                    return false;
                }

                ws.Remove(targetWeb);
            }

            return true;
        }

        public override T RunWeb<T>(
            Do<T> engine = null,
            string webName = null,
            string dependencyName = null)
        {
            ICollection<NamedMethodWebHolder> ws;
            MethodWeb targetWeb;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                targetWeb = EH
                    .FirstOrNull(
                        ws,
                        nmwh => nmwh?.Name == webName)
                    ?.Web;
            }

            if (targetWeb == null)
            {
                return default;
            }

            return targetWeb.Run(
                engine,
                dependencyName);
        }

        public override XTuple<T, U> RunWeb<T, U>(
            Do<T, U> engine = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            ICollection<NamedMethodWebHolder> ws;
            MethodWeb targetWeb;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                targetWeb = EH
                    .FirstOrDefault(
                        ws,
                        nmwh => nmwh?.Name == webName)
                    ?.Web;
            }

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

        public override XTuple<T, U, V> RunWeb<T, U, V>(
            Do<T, U, V> engine = null,
            string webName = null,
            string tName = null, string uName = null,
            string vName = null)
        {
            ICollection<NamedMethodWebHolder> ws;
            MethodWeb targetWeb;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                targetWeb = EH.FirstOrDefault(
                    ws,
                    nmwh => nmwh?.Name == webName)
                    ?.Web;
            }

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

        public override XTuple<T, U, V, W> RunWeb<T, U, V, W>(
            Do<T, U, V, W> engine = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            NamedMethodWebHolder targetWeb;
            lock (this.locker ?? new object())
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh?.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return innerWeb.Run(
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
            NamedMethodWebHolder targetWeb;
            lock (this.locker ?? new object())
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh?.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X));
            }

            return innerWeb.Run(
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
            NamedMethodWebHolder targetWeb;
            lock (this.locker ?? new object())
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh?.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y));
            }

            return innerWeb.Run(
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
            NamedMethodWebHolder targetWeb;
            lock (this.locker ?? new object())
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh?.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
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

            return innerWeb.Run(
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
            NamedMethodWebHolder targetWeb;
            lock (this.locker ?? new object())
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh?.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
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

            return innerWeb.Run(
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
