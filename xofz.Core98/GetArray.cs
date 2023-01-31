namespace xofz
{
    using xofz.Framework.Lots;

    public interface GetArray<T> 
        : Lot<T>
    {
        T this[long index] { get; }
    }

    public class GetArray
    {
        public static GetArray<T> Empty<T>()
        {
            return new ListLot<T>();
        }
    }
}
