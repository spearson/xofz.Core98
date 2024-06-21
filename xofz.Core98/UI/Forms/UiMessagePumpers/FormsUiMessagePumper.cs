namespace xofz.UI.Forms.UiMessagePumpers
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