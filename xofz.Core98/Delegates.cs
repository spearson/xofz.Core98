namespace xofz
{
    public delegate TResult Gen<
        out TResult>();

    public delegate TResult Gen<in T, 
        out TResult>(
        T arg);

    public delegate TResult Gen<in T, in U, 
        out TResult>(
        T arg1, 
        U arg2);

    public delegate TResult Gen<in T, in U, in V, 
        out TResult>(
        T arg1,
        U arg2,
        V arg3);

    public delegate TResult Gen<in T, in U, in V, in W, 
        out TResult>(
        T arg1,
        U arg2,
        V arg3,
        W arg4);

    public delegate TResult Gen<in T, in U, in V, in W, in X, 
        out TResult>(
        T arg1,
        U arg2,
        V arg3,
        W arg4,
        X arg5);

    public delegate TResult Gen<in T, in U, in V, in W, in X, in Y,
        out TResult>(
        T arg1,
        U arg2,
        V arg3,
        W arg4,
        X arg5,
        Y arg6);

    public delegate TResult Gen<in T, in U, in V, in W, in X, in Y, in Z,
        out TResult>(
        T arg1,
        U arg2,
        V arg3,
        W arg4,
        X arg5,
        Y arg6,
        Z arg7);

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

    public delegate void Do<in T, in U, in V, in W, in X>(
        T arg1,
        U arg2,
        V arg3,
        W arg4,
        X arg5);

    public delegate void Do<in T, in U, in V, in W, in X, in Y>(
        T arg1,
        U arg2,
        V arg3,
        W arg4,
        X arg5,
        Y arg6);

    public delegate void Do<in T, in U, in V, in W, in X, in Y, in Z>(
        T arg1,
        U arg2,
        V arg3,
        W arg4,
        X arg5,
        Y arg6,
        Z arg7);

    public delegate void Do<in T, in U, in V, in W, in X, in Y, in Z, in AA>(
        T arg1,
        U arg2,
        V arg3,
        W arg4,
        X arg5,
        Y arg6,
        Z arg7,
        AA arg8);
}
