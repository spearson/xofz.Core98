namespace xofz.Framework
{
    public abstract class Subjugate
    {
        public abstract void Harvest<T>(
            Do<T> engine);

        public abstract void Harvest<T, U>(
            Do<T, U> engine);

        public abstract void Harvest<T, U, V>(
            Do<T, U, V> engine);

        public abstract void Harvest<T, U, V, W>(
            Do<T, U, V, W> engine);

        public abstract void Harvest<T, U, V, W, X>(
            Do<T, U, V, W, X> engine);

        public abstract void Harvest<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> engine);

        public abstract void Harvest<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> engine);

        public abstract void Harvest<T, U, V, W, X, Y, Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> engine);
    }
}
