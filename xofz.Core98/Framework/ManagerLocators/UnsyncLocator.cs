namespace xofz.Framework.ManagerLocators
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class UnsyncLocator
        : ManagerLocator
    {
        public UnsyncLocator()
        {
        }

        protected UnsyncLocator(
            ICollection<NamedManagerHolder> managers)
            : base(managers)
        {
        }

        public override Lot<string> ManagerNames()
        {
            var lll = new XLinkedListLot<string>();
            foreach (var managerName in EH.Select(
                this.managers,
                nmh => nmh?.Name))
            {
                lll.AddTail(
                    managerName);
            }

            return lll;
        }

        public override bool AddManager(
            MethodWebManager manager,
            string name = null)
        {
            if (manager == null)
            {
                return falsity;
            }

            ICollection<NamedManagerHolder> ms;
            ms = this.managers;

            NamedManagerHolder sameNameHolder;
            sameNameHolder = EH.FirstOrNull(
                ms,
                nmh => nmh.Name == name);

            if (sameNameHolder != null)
            {
                return falsity;
            }

            this.add(
                new NamedManagerHolder
                {
                    Manager = manager,
                    Name = name
                });
            return truth;
        }

        public override MethodWebManager AccessManager(
            Do<MethodWebManager> accessor = null,
            string managerName = null)
        {
            NamedManagerHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == managerName);

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return manager;
            }

            accessor?.Invoke(manager);
            return manager;
        }

        public override T AccessManager<T>(
            Do<T> accessor = null,
            string managerName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == managerName);
            var manager = targetHolder?.Manager as T;
            if (manager == null)
            {
                return manager;
            }

            accessor?.Invoke(manager);
            return manager;
        }

        public override bool RemoveManager(
            string managerName)
        {
            var ms = this.managers;
            if (ms == null)
            {
                return falsity;
            }

            var targetHolder = EH.FirstOrNull(
                ms,
                managerHolder => managerHolder.Name == managerName);
            if (targetHolder == null)
            {
                return falsity;
            }

            return ms.Remove(targetHolder);
        }

        public override T Locate<T>(
            Do<T> locat = null,
            string locableName = null,
            string webName = null,
            string dependencyName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return default;
            }

            return manager.RunWeb(
                locat,
                webName,
                dependencyName);
        }

        public override XTuple<T, U> Locate<T, U>(
            Do<T, U> locat = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U));
            }

            return manager.RunWeb(
                locat,
                webName,
                tName,
                uName);
        }

        public override XTuple<T, U, V> Locate<T, U, V>(
            Do<T, U, V> locat = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return manager.RunWeb(
                locat,
                webName,
                tName,
                uName,
                vName);
        }

        public override XTuple<T, U, V, W> Locate<T, U, V, W>(
            Do<T, U, V, W> locat,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return manager.RunWeb(
                locat,
                webName,
                tName,
                uName,
                vName,
                wName);
        }

        public override XTuple<T, U, V, W, X> Locate<T, U, V, W, X>(
            Do<T, U, V, W, X> locat = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X));
            }

            return manager.RunWeb(
                locat,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName);
        }

        public override XTuple<T, U, V, W, X, Y> Locate<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> locat = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null)
        {
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y));
            }

            return manager.RunWeb(
                locat,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName);
        }

        public override XTuple<T, U, V, W, X, Y, Z> Locate<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> locat = null,
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
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
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

            return manager.RunWeb(
                locat,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName,
                zName);
        }

        public override XTuple<T, U, V, W, X, Y, Z, AA> Locate<T, U, V, W, X, Y,
            Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> locat = null,
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
            var targetHolder = EH.FirstOrNull(
                this.managers,
                managerHolder => managerHolder.Name == locableName);

            var manager = targetHolder?.Manager;
            if (manager == null)
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

            return manager.RunWeb(
                locat,
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
