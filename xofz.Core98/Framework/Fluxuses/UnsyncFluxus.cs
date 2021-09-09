namespace xofz.Framework.Fluxuses
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class UnsyncFluxus
        : LeechFluxus
    {
        public UnsyncFluxus()
        {
        }

        protected UnsyncFluxus(
            ICollection<NamedLeechHolder> leeches)
            : base(leeches)
        {
        }

        public override Lot<string> LeechNames()
        {
            var lll = new XLinkedListLot<string>();
            foreach (var leechName in EH.Select(
                this.leeches,
                leechHolder => leechHolder?.Name))
            {
                lll.AddTail(
                    leechName);
            }

            return lll;
        }

        public override bool AddLeech(
            LocatorLeech leech,
            string name = null)
        {
            if (leech == null)
            {
                return falsity;
            }

            ICollection<NamedLeechHolder> ls;
            NamedLeechHolder alreadyAddedLeech;
            ls = this.leeches;
            alreadyAddedLeech = EH.FirstOrNull(
                ls,
                leechHolder => ReferenceEquals(
                    leech,
                    leechHolder?.Leech));

            if (alreadyAddedLeech != null)
            {
                return falsity;
            }

            NamedLeechHolder sameNameHolder;
            lock (this.locker)
            {
                sameNameHolder = EH.FirstOrNull(
                        ls,
                        leechHolder => leechHolder?.Name == name);
            }

            if (sameNameHolder != null)
            {
                return falsity;
            }

            this.add(
                new NamedLeechHolder
                {
                    Leech = leech,
                    Name = name
                });
            return truth;
        }

        protected override void add(
            NamedLeechHolder holder)
        {
            this.leeches?.Add(
                holder);
        }

        public override LocatorLeech AccessLeech(
            Do<LocatorLeech> accessor = null,
            string leechName = null)
        {
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                nmh => nmh?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
            {
                return leech;
            }

            accessor?.Invoke(leech);
            return leech;
        }

        public override T AccessLeech<T>(
            Do<T> accessor = null,
            string leechName = null)
        {
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech as T;
            if (leech == null)
            {
                return leech;
            }

            accessor?.Invoke(leech);
            return leech;
        }

        public override bool RemoveLeech(
            string leechName)
        {
            ICollection<NamedLeechHolder> ls;
            NamedLeechHolder targetHolder;
            bool removed;
            ls = this.leeches;
            targetHolder = EH.FirstOrNull(
                ls,
                leechHolder => leechHolder?.Name == leechName);
            removed = ls?.Remove(targetHolder)
                      ?? falsity;

            return removed;
        }

        public override T Flux<T>(
            Do<T> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string dependencyName = null)
        {
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
            {
                return default;
            }

            return leech.Siphon(
                fluxor,
                locatorName,
                locableName,
                webName,
                dependencyName);
        }

        public override XTuple<T, U> Flux<T, U>(
            Do<T, U> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null)
        {
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U));
            }

            return leech.Siphon(
                fluxor,
                locatorName,
                locableName,
                webName,
                tName,
                uName);
        }

        public override XTuple<T, U, V> Flux<T, U, V>(
            Do<T, U, V> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null)
        {
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            return leech.Siphon(
                fluxor,
                locatorName,
                locableName,
                webName,
                tName,
                uName,
                vName);
        }

        public override XTuple<T, U, V, W> Flux<T, U, V, W>(
            Do<T, U, V, W> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null)
        {
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            return leech.Siphon(
                fluxor,
                locatorName,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName);
        }

        public override XTuple<T, U, V, W, X> Flux<T, U, V, W, X>(
            Do<T, U, V, W, X> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null)
        {
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X));
            }

            return leech.Siphon(
                fluxor,
                locatorName,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName);
        }

        public override XTuple<T, U, V, W, X, Y> Flux<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> fluxor = null,
            string leechName = null,
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
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
            {
                return XTuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W),
                    default(X),
                    default(Y));
            }

            return leech.Siphon(
                fluxor,
                locatorName,
                locableName,
                webName,
                tName,
                uName,
                vName,
                wName,
                xName,
                yName);
        }

        public override XTuple<T, U, V, W, X, Y, Z> Flux<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> fluxor = null,
            string leechName = null,
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
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
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

            return leech.Siphon(
                fluxor,
                locatorName,
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

        public override XTuple<T, U, V, W, X, Y, Z, AA> Flux<T, U, V, W, X, Y,
            Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> fluxor = null,
            string leechName = null,
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
            NamedLeechHolder targetHolder;
            targetHolder = EH.FirstOrNull(
                this.leeches,
                leechHolder => leechHolder?.Name == leechName);

            var leech = targetHolder?.Leech;
            if (leech == null)
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

            return leech.Siphon(
                fluxor,
                locatorName,
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
