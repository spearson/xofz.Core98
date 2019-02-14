namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class MethodWebManagerV2 : MethodWebManager
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
            var ll = new LinkedListLot<string>();
            lock (this.locker)
            {
                foreach (var name in EH.Select(
                    this.webs,
                    nmwh => nmwh.Name))
                {
                    ll.AddLast(name);
                }
            }

            return ll;
        }

        public override bool AddWeb(
            MethodWeb web, 
            string name = nameof(MethodWebNameConsts.Default))
        {
            if (web == null)
            {
                return false;
            }

            ICollection<NamedMethodWebHolder> ws;
            NamedMethodWebHolder alreadyAddedWeb;
            lock (this.locker)
            {
                ws = this.webs;
                alreadyAddedWeb = EH.FirstOrDefault(
                    ws,
                    nmwh => ReferenceEquals(web, nmwh.Web));
            }

            if (alreadyAddedWeb != null)
            {
                return false;
            }

            lock (this.locker)
            {
                if (EH.Contains(
                    EH.Select(
                        ws, nmwh => nmwh.Name),
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
            }

            return true;
        }

        public override void AccessWeb(
            Do<MethodWeb> accessor,
            string webName = nameof(MethodWebNameConsts.Default))
        {
            NamedMethodWebHolder targetWeb;
            lock (this.locker)
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return;
            }

            accessor(innerWeb);
        }

        public override void AccessWeb<T>(
            Do<T> accessor, 
            string webName = nameof(MethodWebNameConsts.Default))
        {
            NamedMethodWebHolder targetWeb;
            lock (this.locker)
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh.Name == webName);
            }

            var innerWeb = targetWeb?.Web as T;
            if (innerWeb == null)
            {
                return;
            }

            accessor(innerWeb);
        }

        public virtual bool RemoveWeb(
            string webName)
        {
            ICollection<NamedMethodWebHolder> ws;
            NamedMethodWebHolder targetWeb;
            lock (this.locker)
            {
                ws = this.webs;
                targetWeb = EH.FirstOrDefault(
                    ws,
                    nmwh => nmwh.Name == webName);

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
            string webName = nameof(MethodWebNameConsts.Default),
            string dependencyName = null)
        {
            NamedMethodWebHolder targetWeb;
            lock (this.locker)
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return default(T);
            }

            return innerWeb.Run(engine, dependencyName);
        }

        public override XTuple<T, U> RunWeb<T, U>(
            Do<T, U> engine = null, 
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null, 
            string dependency2Name = null)
        {
            NamedMethodWebHolder targetWeb;
            lock (this.locker)
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U));
            }

            return innerWeb.Run(
                engine,
                dependency1Name,
                dependency2Name);
        }

        public override XTuple<T, U, V> RunWeb<T, U, V>(
            Do<T, U, V> engine = null,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null, string dependency2Name = null,
            string dependency3Name = null)
        {
            NamedMethodWebHolder targetWeb;
            lock (this.locker)
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh.Name == webName);
            }

            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return innerWeb.Run(
                engine,
                dependency1Name,
                dependency2Name,
                dependency3Name);
        }

        public override XTuple<T, U, V, W> RunWeb<T, U, V, W>(
            Do<T, U, V, W> engine = null, 
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null, 
            string dependency2Name = null,
            string dependency3Name = null, 
            string dependency4Name = null)
        {
            NamedMethodWebHolder targetWeb;
            lock (this.locker)
            {
                targetWeb = EH.FirstOrDefault(
                    this.webs,
                    nmwh => nmwh.Name == webName);
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
                dependency1Name,
                dependency2Name,
                dependency3Name,
                dependency4Name);
        }

        protected readonly object locker;
    }
}
