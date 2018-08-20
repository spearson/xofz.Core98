namespace xofz.UI
{
    using System.ComponentModel;
    using System.Threading;

    public interface Ui
    {
        ISynchronizeInvoke Root { get; }

        AutoResetEvent WriteFinished { get; }

        bool Disabled { get; set; }
    }
}
