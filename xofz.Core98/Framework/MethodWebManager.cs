namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class MethodWebNameConsts
    {
        // the variable name should be the web name
        public const bool Default = true;
    }

    public class MethodWebManager
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
                    nmwh => nmwh.Name));
        }

        public virtual bool AddWeb(
            MethodWeb web,
            string name = nameof(MethodWebNameConsts.Default))
        {
            if (web == null)
            {
                return false;
            }

            var ws = this.webs;
            var namedWeb = EH.FirstOrDefault(
                ws,
                nmwh => ReferenceEquals(web, nmwh.Web));
            if (namedWeb != default(NamedMethodWebHolder))
            {
                return false;
            }

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
            return true;
        }

        public virtual void AccessWeb(
            Do<MethodWeb> accessor,
            string webName = nameof(MethodWebNameConsts.Default))
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh.Name == webName);
            var innerWeb = targetWeb?.Web;
            if (innerWeb == null)
            {
                return;
            }

            accessor(innerWeb);
        }

        public virtual void AccessWeb<T>(
            Do<T> accessor,
            string webName = nameof(MethodWebNameConsts.Default))
            where T : MethodWeb
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh.Name == webName);

            var innerWeb = targetWeb?.Web as T;
            if (innerWeb == null)
            {
                return;
            }

            accessor(innerWeb);
        }

        public virtual T RunWeb<T>(
            xofz.Do<T> engine = null,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependencyName = null)
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh.Name == webName);
            if (targetWeb == default(NamedMethodWebHolder))
            {
                return default(T);
            }

            return targetWeb.Web.Run(engine, dependencyName);
        }

        public virtual XTuple<T, U> RunWeb<T, U>(
            Do<T, U> engine = null,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null,
            string dependency2Name = null)
        {
            var w = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return XTuple.Create(
                    default(T),
                    default(U));
            }

            return w.Web.Run(
                engine,
                dependency1Name,
                dependency2Name);
        }

        public virtual XTuple<T, U, V> RunWeb<T, U, V>(
            Do<T, U, V> engine = null,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null)
        {
            var w = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return w.Web.Run(
                engine,
                dependency1Name,
                dependency2Name,
                dependency3Name);
        }

        public virtual XTuple<T, U, V, W> RunWeb<T, U, V, W>(
            Do<T, U, V, W> engine = null,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null,
            string dependency4Name = null)
        {
            var w = EH.FirstOrDefault(
                this.webs,
                nmwh => nmwh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return w.Web.Run(
                engine,
                dependency1Name,
                dependency2Name,
                dependency3Name,
                dependency4Name);
        }

        protected readonly ICollection<NamedMethodWebHolder> webs;

        protected class NamedMethodWebHolder
        {
            public virtual MethodWeb Web { get; set; }

            public virtual string Name { get; set; }
        }
    }
}
