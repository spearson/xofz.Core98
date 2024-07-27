namespace xofz.Framework
{
    public interface Beholder<T>
    {
        void Receive(
            T state);

        T Transform(
            Do<T> transformation);
    }
}
