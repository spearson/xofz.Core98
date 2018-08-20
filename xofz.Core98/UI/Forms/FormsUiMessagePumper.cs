namespace xofz.UI.Forms
{
    using System.Windows.Forms;

    public sealed class FormsUiMessagePumper : UiMessagePumper
    {
        void UiMessagePumper.Pump()
        {
            Application.DoEvents();
        }
    }
}
