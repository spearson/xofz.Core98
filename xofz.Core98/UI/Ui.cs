namespace xofz.UI
{
    using System.ComponentModel;

    public interface Ui
    {
        ISynchronizeInvoke Root { get; }

        bool Disabled { get; set; }
    }
}
