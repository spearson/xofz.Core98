namespace xofz.Framework
{
    public interface MultiWebRunner
    {
        T RunWeb<T>(
            Do<T> engine,
            string webName = null,
            string dependencyName = null);

        XTuple<T, U> RunWeb<T, U>(
            Do<T, U> engine,
            string webName = null,
            string tName = null,
            string uName = null);

        XTuple<T, U, V> RunWeb<T, U, V>(
            Do<T, U, V> engine,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null);

        XTuple<T, U, V, W> RunWeb<T, U, V, W>(
            Do<T, U, V, W> engine,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null);

        XTuple<T, U, V, W, X> RunWeb<T, U, V, W, X>(
            Do<T, U, V, W, X> engine,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null);

        XTuple<T, U, V, W, X, Y> RunWeb<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> engine,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null);

        XTuple<T, U, V, W, X, Y, Z> RunWeb<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> engine,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null);

        XTuple<T, U, V, W, X, Y, Z, AA> RunWeb<T, U, V, W, X, Y, Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> engine,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null,
            string aaName = null);
    }
}
