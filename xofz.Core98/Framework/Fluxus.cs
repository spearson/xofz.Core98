namespace xofz.Framework
{
    public interface Fluxus
    {
        T Flux<T>(
            Do<T> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string dependencyName = null);

        XTuple<T, U> Flux<T, U>(
            Do<T, U> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null);

        XTuple<T, U, V> Flux<T, U, V>(
            Do<T, U, V> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null);

        XTuple<T, U, V, W> Flux<T, U, V, W>(
            Do<T, U, V, W> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null);

        XTuple<T, U, V, W, X> Flux<T, U, V, W, X>(
            Do<T, U, V, W, X> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null);

        XTuple<T, U, V, W, X, Y> Flux<T, U, V, W, X, Y>(
            Do<T, U, V, W, X, Y> fluxor = null,
            string leechName = null,
            string locatorName = null,
            string locableName = null,
            string webName = null,
            string tName = null,
            string uName = null,
            string vName = null,
            string wName = null,
            string xName = null,
            string yName = null);

        XTuple<T, U, V, W, X, Y, Z> Flux<T, U, V, W, X, Y, Z>(
            Do<T, U, V, W, X, Y, Z> fluxor = null,
            string leechName = null,
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

        XTuple<T, U, V, W, X, Y, Z, AA> Flux<T, U, V, W, X, Y, Z, AA>(
            Do<T, U, V, W, X, Y, Z, AA> fluxor = null,
            string leechName = null,
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
