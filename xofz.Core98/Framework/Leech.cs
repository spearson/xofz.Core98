namespace xofz.Framework
{
    public interface Leech
    {
        T Siphon<T>(
            Do<T> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string dependencyName = null);

        XTuple<T, U> Siphon<T, U>(
            Do<T, U> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null);

        XTuple<T, U, V> Siphon<T, U, V>(
            Do<T, U, V> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null);

        XTuple<T, U, V, W> Siphon<T, U, V, W>(
            Do<T, U, V, W> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null);

        XTuple<T, U, V, W, X> Siphon<T, U, V, W, X>(
            Do<T, U, V, W, X> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null);

        XTuple<T, U, V, W, X, Y> Siphon<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null);

        XTuple<T, U, V, W, X, Y, Z> Siphon<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> siphon = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null,
            string zName = null);

        XTuple<T, U, V, W, X, Y, Z, AA> Siphon<T, U, V, W, X, Y, Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> siphon = null,
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
            string aaName = null);
    }
}