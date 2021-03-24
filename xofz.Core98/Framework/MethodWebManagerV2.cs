namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class MethodWebManagerV2
        : MethodWebManager, System.IComparable
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

        public virtual MethodWebV2 Shuffle()
        {
            var matchingWebs = this.shuffleWebs();
            return EH.FirstOrDefault(
                matchingWebs);
        }

        public virtual T Shuffle<T>()
            where T : MethodWebV2
        {
            foreach (var web in this.shuffleWebs()
                                ?? EH.Empty<MethodWebV2>())
            {
                if (web is T matchingWeb)
                {
                    return matchingWeb;
                }
            }

            return default;
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
                return falsity;
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
                return falsity;
            }

            lock (this.locker ?? new object())
            {
                if (EH.Contains(
                    EH.Select(
                        ws,
                        nmwh => nmwh?.Name),
                    name))
                {
                    return falsity;
                }

                ws?.Add(
                    new NamedMethodWebHolder
                    {
                        Web = web,
                        Name = name
                    });
            }

            return truth;
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
                        nmwh => nmwh?.Name == webName)?.
                    Web;
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
                        nmwh => nmwh?.Name == webName)?.
                    Web as T;
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
            bool removed;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                targetWeb = EH.FirstOrNull(
                    ws,
                    nmwh => nmwh?.Name == webName);

                removed = ws.Remove(targetWeb);
            }

            return removed;
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
                targetWeb = EH.FirstOrNull(
                        ws,
                        nmwh => nmwh?.Name == webName)?.
                    Web;
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
                targetWeb = EH.FirstOrDefault(
                        ws,
                        nmwh => nmwh?.Name == webName)?.
                    Web;
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
            string tName = null,
            string uName = null,
            string vName = null)
        {
            ICollection<NamedMethodWebHolder> ws;
            MethodWeb targetWeb;
            lock (this.locker ?? new object())
            {
                ws = this.webs;
                targetWeb = EH.FirstOrDefault(
                        ws,
                        nmwh => nmwh?.Name == webName)?.
                    Web;
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

        public virtual int CompareTo(
            object obj)
        {
            const short nOne = -1;
            const byte one = 1;
            if (obj is null)
            {
                return one;
            }

            if (obj is MethodWebManagerV2 otherManager)
            {
                const byte zero = 0;
                if (ReferenceEquals(this, otherManager))
                {
                    return zero;
                }

                long?
                    thisCount,
                    otherCount;
                lock (this.locker ?? new object())
                {
                    thisCount = this?.webs?.Count;
                }

                lock (otherManager.locker ?? new object())
                {
                    otherCount = otherManager?.webs?.Count;
                }

                return thisCount > otherCount
                    ? one
                    : otherCount > thisCount
                        ? nOne
                        : zero;
            }

            return one;
        }

        protected readonly object locker;

        protected virtual Lot<MethodWebV2> shuffleWebs()
        {
            ICollection<NamedMethodWebHolder> ws;
            var matchingWebs = new ListLot<MethodWebV2>();

            lock (this.locker ?? new object())
            {
                ws = this.webs;
                foreach (var webHolder in ws
                                          ?? EH.Empty<NamedMethodWebHolder>())
                {
                    if (webHolder?.Web is MethodWebV2 webV2)
                    {
                        matchingWebs.Add(webV2);
                    }
                }
            }

            matchingWebs.Sort();
            return matchingWebs;
        }
    }
}
