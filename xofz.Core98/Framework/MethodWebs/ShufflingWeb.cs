namespace xofz.Framework.MethodWebs
{
    using System.Collections.Generic;
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

        protected ShufflingWeb(
            ICollection<Dependency> dependencies,
            object locker)
            : base(dependencies, locker)
        {
        }

        public virtual object Shuffle()
        {
            return EH.FirstOrNull(
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
            var matchingDependencies = new ListLot<
                ShufflingObject<Dependency>>();
            lock (this.locker)
            {
                foreach (var dependency in this.dependencies
                                           ?? EH.Empty<Dependency>())
                {
                    matchingDependencies?.Add(
                        new ShufflingObject<Dependency>(
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
                    so => so.O));
        }
    }
}