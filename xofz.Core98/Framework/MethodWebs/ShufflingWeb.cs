namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using xofz.Framework.Lots;
    using EH = EnumerableHelpers;

    public class ShufflingWeb
        : MethodWebV2, System.IComparable
    {
        public ShufflingWeb()
        {
        }

        protected ShufflingWeb(
            ICollection<Dependency> dependencies)
            : base(dependencies)
        {
        }

        protected ShufflingWeb(
            object locker)
            : base(locker)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen)
        {
        }

        protected ShufflingWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            ICollection<Dependency> dependencies,
            RNGCryptoServiceProvider randomGen)
            : base(dependencies)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen,
            object locker)
            : base(locker)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            RNGCryptoServiceProvider randomGen,
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }

        [System.Obsolete]
        protected ShufflingWeb(
            MethodWeb copy,
            LotterV2 lotter)
            : base(copy, lotter)
        {
        }

        public virtual object Shuffle()
        {
            return EH.FirstOrDefault(
                    this.shuffleDependencies())?.
                Content;
        }

        public virtual T Shuffle<T>()
        {
            var shuffled = this.shuffleDependencies();
            foreach (var shuffledDependency in shuffled ??
                                               EH.Empty<Dependency>())
            {
                if (shuffledDependency?.Content is T matchingContent)
                {
                    return matchingContent;
                }
            }

            return default;
        }

        public virtual int CompareTo(
            object obj)
        {
            var soThis = new ShufflingObject(this);
            var soOther = new ShufflingObject(obj);
            return soThis.CompareTo(soOther);
        }

        protected virtual Lot<Dependency> shuffleDependencies()
        {
            var matchingDependencies = new ListLot<ShufflingObject>();
            lock (this.locker ?? new object())
            {
                foreach (var dependency in this.dependencies
                                           ?? EH.Empty<Dependency>())
                {
                    matchingDependencies?.Add(
                        new ShufflingObject(
                            new Dependency
                            {
                                Content = dependency?.Content,
                                Name = dependency?.Name
                            }));
                }
            }

            matchingDependencies?.Sort();

            return new LinkedListLot<Dependency>(
                EH.Select(
                    matchingDependencies,
                    so => so.O as Dependency));
        }

        [System.Obsolete(@"This field does nothing.  Check ShufflingObject out.")]
        protected readonly RNGCryptoServiceProvider randomGen;

        [System.Obsolete]
        protected class ShufflingDependency
            : ShufflingObject
        {
            public virtual object Content { get; set; }

            public virtual string Name { get; set; }

        }
    }
}