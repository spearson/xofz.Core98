namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using EH = EnumerableHelpers;

    public class MethodWebNameConsts
    {
        // the variable name should be the web name
        public const bool Default = true;
    }

    public class MethodWebManager
    {
        public MethodWebManager()
        {
            this.webs = new LinkedList<NamedMethodWebHolder>();
        }

        public virtual IEnumerable<string> WebNames()
        {
            return EH.Select(
                this.webs,
                nmwh => nmwh.Name);
        }

        public virtual void AddWeb(
            MethodWeb web,
            string name = nameof(MethodWebNameConsts.Default))
        {
            if (web == null)
            {
                throw new InvalidOperationException(
                    "Cannot add a null web.");
            }

            var ws = this.webs;
            var namedWeb = EH.FirstOrDefault(
                ws,
                nmwh => ReferenceEquals(web, nmwh.Web));
            if (namedWeb != default(NamedMethodWebHolder))
            {
                throw new InvalidOperationException(
                    "That web has already been added with name: "
                    + namedWeb.Name);
            }

            if (EH.Contains(
                    EH.Select(
                        ws, nmwh => nmwh.Name),
                        name))
            {
                throw new InvalidOperationException(
                    "Name \"" + name + "\" is already taken.");
            }

            ws.Add(
                new NamedMethodWebHolder
                {
                    Web = web,
                    Name = name
                });
        }

        public virtual void AccessWeb(
            Action<MethodWeb> accessor,
            string webName = nameof(MethodWebNameConsts.Default))
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nwmh => nwmh.Name == webName);
            if (targetWeb == default(NamedMethodWebHolder))
            {
                return;
            }

            accessor(targetWeb.Web);
        }

        public virtual T RunWeb<T>(
            xofz.Action<T> engine,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependencyName = null)
        {
            var targetWeb = EH.FirstOrDefault(
                this.webs,
                nwmh => nwmh.Name == webName);
            if (targetWeb == default(NamedMethodWebHolder))
            {
                return default(T);
            }

            return targetWeb.Web.Run(engine, dependencyName);
        }

        public virtual Tuple<T, U> RunWeb<T, U>(
            Action<T, U> engine,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null,
            string dependency2Name = null)
        {
            var w = EH.FirstOrDefault(
                this.webs,
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return Tuple.Create(
                    default(T),
                    default(U));
            }

            return w.Web.Run(
                engine,
                dependency1Name,
                dependency2Name);
        }

        public virtual Tuple<T, U, V> RunWeb<T, U, V>(
            Action<T, U, V> engine,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null)
        {
            var w = EH.FirstOrDefault(
                this.webs,
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return Tuple.Create(
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

        public virtual Tuple<T, U, V, W> RunWeb<T, U, V, W>(
            Action<T, U, V, W> engine,
            string webName = nameof(MethodWebNameConsts.Default),
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null,
            string dependency4Name = null)
        {
            var w = EH.FirstOrDefault(
                this.webs,
                nwmh => nwmh.Name == webName);
            if (w == default(NamedMethodWebHolder))
            {
                return Tuple.Create(
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
