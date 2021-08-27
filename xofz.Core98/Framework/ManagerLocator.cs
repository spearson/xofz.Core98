namespace xofz.Framework
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class ManagerLocator
    {
        public ManagerLocator()
            : this(
                new XLinkedList<NamedManagerHolder>(),
                new object())
        {
        }

        protected ManagerLocator(
            ICollection<NamedManagerHolder> managers)
            : this(
                managers,
                new object())
        {
        }

        protected ManagerLocator(
            object locker)
            : this(
                new XLinkedList<NamedManagerHolder>(),
                locker)
        {
        }

        protected ManagerLocator(
            ICollection<NamedManagerHolder> managers,
            object locker)
        {
            this.managers = managers;
            this.locker = locker;
        }

        public virtual Lot<string> ManagerNames()
        {
            var lll = new LinkedListLot<string>();
            lock (this.locker)
            {
                foreach (var managerName in EH.Select(
                    this.managers,
                    nmh => nmh?.Name))
                {
                    lll.AddLast(
                        managerName);
                }
            }

            return lll;
        }

        public virtual bool AddManager(
            MethodWebManager manager,
            string name = null)
        {
            if (manager == null)
            {
                return falsity;
            }

            ICollection<NamedManagerHolder> ms;
            NamedManagerHolder alreadyAddedManager;
            lock (this.locker)
            {
                ms = this.managers;
                alreadyAddedManager = EH.FirstOrNull(
                    ms,
                    nmh => ReferenceEquals(
                        manager,
                        nmh?.Manager));
            }

            if (alreadyAddedManager != null)
            {
                return falsity;
            }

            NamedManagerHolder sameNameHolder;
            lock (this.locker)
            {
                sameNameHolder = EH.FirstOrNull(
                        ms,
                        nmh => nmh?.Name == name);
            }

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

        protected virtual void add(
            NamedManagerHolder holder)
        {
            lock (this.locker)
            {
                this.managers?.Add(
                    holder);
            }
        }

        public virtual MethodWebManager AccessManager(
            Do<MethodWebManager> accessor = null,
            string managerName = null)
        {
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == managerName);
            }

            var manager = targetHolder?.Manager;
            if (manager == null)
            {
                return manager;
            }

            accessor?.Invoke(manager);
            return manager;
        }

        public virtual T AccessManager<T>(
            Do<T> accessor = null,
            string managerName = null)
            where T : MethodWebManager
        {
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == managerName);
            }

            var manager = targetHolder?.Manager as T;
            if (manager == null)
            {
                return manager;
            }

            accessor?.Invoke(manager);
            return manager;
        }

        public virtual bool RemoveManager(
            string managerName)
        {
            ICollection<NamedManagerHolder> ms;
            NamedManagerHolder targetHolder;
            bool removed;
            lock (this.locker)
            {
                ms = this.managers;
                targetHolder = EH.FirstOrNull(
                    ms,
                    managerHolder => managerHolder?.Name == managerName);
                removed = ms?.Remove(targetHolder)
                    ?? falsity;
            }

            return removed;
        }

        public virtual T Locate<T>(
            Do<T> locat = null,
            string locableName = null,
            string webName = null,
            string dependencyName = null)
        {
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        public virtual XTuple<T, U> Locate<T, U>(
            Do<T, U> locat = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        public virtual XTuple<T, U, V> Locate<T, U, V>(
            Do<T, U, V> locat = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        public virtual XTuple<T, U, V, W> Locate<T, U, V, W>(
            Do<T, U, V, W> locat,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        public virtual XTuple<T, U, V, W, X> Locate<T, U, V, W, X>(
            Do<T, U, V, W, X> locat = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        public virtual XTuple<T, U, V, W, X, Y> Locate<T, U, V, W, X, Y>(
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
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        public virtual XTuple<T, U, V, W, X, Y, Z> Locate<T, U, V, W, X, Y, Z>(
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
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        public virtual XTuple<T, U, V, W, X, Y, Z, AA> Locate<T, U, V, W, X, Y,
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
            NamedManagerHolder targetHolder;
            lock (this.locker)
            {
                targetHolder = EH.FirstOrNull(
                    this.managers,
                    managerHolder => managerHolder?.Name == locableName);
            }

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

        protected readonly ICollection<NamedManagerHolder> managers;
        protected readonly object locker;
        protected const bool
            truth = true,
            falsity = false;

        protected class NamedManagerHolder
        {
            public virtual MethodWebManager Manager { get; set; }

            public virtual string Name { get; set; }
        }
    }
}
