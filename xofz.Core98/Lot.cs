namespace xofz
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public interface Lot<T> : IEnumerable<T>
    {
        long Count { get; }
    }

    public static class Lot
    {
        public static Lot<T> Empty<T>()
        {
            return new LinkedListLot<T>();
        }
    }
}