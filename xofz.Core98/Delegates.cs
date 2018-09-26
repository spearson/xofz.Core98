namespace xofz
{
    public delegate TResult Gen<out TResult>();

    public delegate TResult Gen<in T, out TResult>(
        T arg);

    public delegate TResult Gen<in T, in U, out TResult>(
        T arg1, 
        U arg2);

    public delegate TResult Gen<in T, in U, in V, out TResult>(
        T arg1,
        U arg2,
        V arg3);

    public delegate void Do();

    public delegate void Do<in T>(
        T arg);

    public delegate void Do<in T, in U>(
        T arg1, 
        U arg2);

    public delegate void Do<in T, in U, in V>(
        T arg1, 
        U arg2, 
        V arg3);

    public delegate void Do<in T, in U, in V, in W>(
        T arg1, 
        U arg2, 
        V arg3,
        W arg4);
}
