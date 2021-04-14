namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class LocatorLeech
        : Leech
    {
        public LocatorLeech()
            : this(
                new LinkedList<NamedLocatorHolder>(),
                new object())
        {
        }

        protected LocatorLeech(
            ICollection<NamedLocatorHolder> locators)
            : this(
                locators,
                new object())
        {
        }

        protected LocatorLeech(
            object locker)
            : this(
                new LinkedList<NamedLocatorHolder>(),
                locker)
        {
        }

        protected LocatorLeech(
            ICollection<NamedLocatorHolder> locators,
            object locker)
        {
            this.locators = locators;
            this.locker = locker;
        }

        public virtual Lot<string> LocatorNames()
        {
            var lll = new LinkedListLot<string>();
            lock (this.locker)
            {
                foreach (var locatorName in EH.Select(
                    this.locators,
                    locatorHolder => locatorHolder?.Name))
                {
                    lll.AddLast(
                        locatorName);
                }
            }

            return lll;
        }

        public virtual bool AddLocator(
            ManagerLocator locator,
            string name = null)
        {
            if (locator == null)
            {
                return falsity;
            }

            ICollection<NamedLocatorHolder> ls;
            NamedLocatorHolder alreadyAddedLocator;
            lock (this.locker)
            {
                ls = this.locators;
                alreadyAddedLocator = EH.FirstOrDefault(
                    ls,
                    locatorHolder => ReferenceEquals(
                        locator,
                        locatorHolder?.Locator));
            }

            if (alreadyAddedLocator != null)
            {
                return falsity;
            }

            NamedLocatorHolder sameNameHolder;
            lock (this.locker)
            {
                sameNameHolder = EH.FirstOrDefault(
                        ls,
                        locatorHolder => locatorHolder?.Name == name);
            }

            if (sameNameHolder != null)
            {
                return falsity;
            }

            this.add(
                new NamedLocatorHolder
                {
                    Locator = locator,
                    Name = name
                });
            return truth;
        }

        protected virtual void add(
            NamedLocatorHolder holder)
        {
            lock (this.locker)
            {
                this.locators?.Add(
                    holder);
            }
        }

        public virtual ManagerLocator AccessLocator(
            Do<ManagerLocator> accessor = null,
            string locatorName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return locator;
            }

            accessor?.Invoke(locator);
            return locator;
        }

        public virtual T AccessLocator<T>(
            Do<T> accessor = null,
            string locatorName = null)
            where T : ManagerLocator
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator as T;
            if (locator == null)
            {
                return locator;
            }

            accessor?.Invoke(locator);
            return locator;
        }

        public virtual bool RemoveLocator(
            string locatorName)
        {
            ICollection<NamedLocatorHolder> ls;
            NamedLocatorHolder targetHolder;
            bool removed;
            lock (this.locker)
            {
                ls = this.locators;
                targetHolder = EH.FirstOrDefault(
                    ls,
                    locatorHolder => locatorHolder?.Name == locatorName);
                removed = ls?.Remove(targetHolder)
                    ?? falsity;
            }

            return removed;
        }

        public virtual T Siphon<T>(
            Do<T> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string dependencyName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return default;
            }

            return locator.Locate(
                siphon,
                locableName,
                webName,
                dependencyName);
        }

        public virtual XTuple<T, U> Siphon<T, U>(
            Do<T, U> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U));
            }

            return locator.Locate(
                siphon,
                locableName,
                webName,
                tName,
                uName);
        }

        public virtual XTuple<T, U, V> Siphon<T, U, V>(
            Do<T, U, V> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return locator.Locate(
                siphon,
                locableName,
                webName,
                tName,
                uName,
                vName);
        }

        public virtual XTuple<T, U, V, W> Siphon<T, U, V, W>(
            Do<T, U, V, W> siphon,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return locator.Locate(
                siphon,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName);
        }

        public virtual XTuple<T, U, V, W, X> Siphon<T, U, V, W, X>(
            Do<T, U, V, W, X> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X));
            }

            return locator.Locate(
                siphon,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName);
        }

        public virtual XTuple<T, U, V, W, X, Y> Siphon<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y));
            }

            return locator.Locate(
                siphon,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName);
        }

        public virtual XTuple<T, U, V, W, X, Y, Z> Siphon<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null)
        {
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
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

            return locator.Locate(
                siphon,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName,
                zName);
        }

        public virtual XTuple<T, U, V, W, X, Y, Z, AA> Siphon<T, U, V, W, X, Y,
            Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> siphon = null,
            string locatorName = null,
            string locableName = null,
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
            NamedLocatorHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrDefault(
                    this.locators,
                    locatorHolder => locatorHolder?.Name == locatorName);
            }

            var locator = targetHolder?.Locator;
            if (locator == null)
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

            return locator.Locate(
                siphon,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName,
                zName,
                aaName);
        }

        protected readonly ICollection<NamedLocatorHolder> locators;
        protected readonly object locker;
        protected const bool
            truth = true,
            falsity = false;

        protected class NamedLocatorHolder
        {
            public virtual ManagerLocator Locator { get; set; }

            public virtual string Name { get; set; }
        }
    }
}
