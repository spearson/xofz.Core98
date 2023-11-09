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
                    this.shuffleDependencies())
                ?.Content;
        }

        public virtual T Shuffle<T>()
        {
            foreach (var d in this.shuffleDependencies())
            {
                if (d?.Content is T matchingContent)
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
            var matches = new List<
                ShufflingObject<Dependency>>();

            lock (this.locker)
            {
                foreach (var dependency in this.dependencies)
                {
                    matches.Add(
                        new ShufflingObject<Dependency>(
                            dependency));
                }
            }

            matches.Sort();

            return new XLinkedListLot<Dependency>(
                XLinkedList<Dependency>.Create(
                    EH.Select(
                        matches,
                        so => so.O)));
        }
    }
}
