namespace xofz.Framework.Leeches
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class UnsyncLeech
        : LocatorLeech
    {
        public UnsyncLeech()
        {
        }

        protected UnsyncLeech(
            ICollection<NamedLocatorHolder> locators)
            : base(locators)
        {
        }

        public override Lot<string> LocatorNames()
        {
            var lll = new XLinkedListLot<string>();
            foreach (var locatorName in EH.Select(
                this.locators,
                locatorHolder => locatorHolder?.Name))
            {
                lll.AddTail(
                    locatorName);
            }

            return lll;
        }

        public override bool AddLocator(
            ManagerLocator locator,
            string name = null)
        {
            if (locator == null)
            {
                return falsity;
            }

            ICollection<NamedLocatorHolder> ls;
            ls = this.locators;

            NamedLocatorHolder sameNameHolder;
            sameNameHolder = EH.FirstOrNull(
                ls,
                locatorHolder => locatorHolder?.Name == name);

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

        protected override void add(
            NamedLocatorHolder holder)
        {
            this.locators?.Add(
                holder);
        }

        public override ManagerLocator AccessLocator(
            Do<ManagerLocator> accessor = null,
            string locatorName = null)
        {
            NamedLocatorHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

            var locator = targetHolder?.Locator;
            if (locator == null)
            {
                return locator;
            }

            accessor?.Invoke(locator);
            return locator;
        }

        public override T AccessLocator<T>(
            Do<T> accessor = null,
            string locatorName = null)
        {
            NamedLocatorHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

            var locator = targetHolder?.Locator as T;
            if (locator == null)
            {
                return locator;
            }

            accessor?.Invoke(locator);
            return locator;
        }

        public override bool RemoveLocator(
            string locatorName)
        {
            ICollection<NamedLocatorHolder> ls;
            NamedLocatorHolder targetHolder;
            bool removed;
            ls = this.locators;
            targetHolder = EH.FirstOrNull(
                ls,
                locatorHolder => locatorHolder?.Name == locatorName);
            removed = ls?.Remove(targetHolder)
                      ?? falsity;

            return removed;
        }

        public override T Siphon<T>(
            Do<T> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string dependencyName = null)
        {
            NamedLocatorHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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

        public override XTuple<T, U> Siphon<T, U>(
            Do<T, U> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            NamedLocatorHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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

        public override XTuple<T, U, V> Siphon<T, U, V>(
            Do<T, U, V> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            NamedLocatorHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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

        public override XTuple<T, U, V, W> Siphon<T, U, V, W>(
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
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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

        public override XTuple<T, U, V, W, X> Siphon<T, U, V, W, X>(
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
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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

        public override XTuple<T, U, V, W, X, Y> Siphon<T, U, V, W, X, Y>(
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
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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

        public override XTuple<T, U, V, W, X, Y, Z> Siphon<T, U, V, W, X, Y, Z>(
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
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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

        public override XTuple<T, U, V, W, X, Y, Z, AA> Siphon<T, U, V, W, X, Y,
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
            targetHolder = EH.FirstOrNull(
                this.locators,
                locatorHolder => locatorHolder?.Name == locatorName);

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
    }
}
