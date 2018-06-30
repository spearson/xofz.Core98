namespace xofz
{
    public delegate TResult Func<out TResult>();

    public delegate TResult Func<in T, out TResult>(
        T arg);

    public delegate TResult Func<in T, in U, out TResult>(
        T arg1, 
        U arg2);

    public delegate void Action();

    public delegate void Action<in T>(
        T arg);

    public delegate void Action<in T, in U>(
        T arg1, 
        U arg2);

    public delegate void Action<in T, in U, in V>(
        T arg1, 
        U arg2, 
        V arg3);

    public delegate void Action<in T, in U, in V, in W>(
        T arg1, 
        U arg2, 
        V arg3,
        W arg4);
}
