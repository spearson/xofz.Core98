namespace xofz.UI
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    public interface Ui
    {
        ISynchronizeInvoke Root { get; }

        AutoResetEvent WriteFinished { get; }

        MarshalByRefObject Referrer { get; }

        void AssertStability();
    }
}
