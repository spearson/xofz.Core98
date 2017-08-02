namespace xofz
{
    public interface Beholder<in T>
    {
        void Receive(T state);
    }
}
