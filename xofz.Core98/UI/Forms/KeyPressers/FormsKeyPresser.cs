namespace xofz.UI.Forms.KeyPressers
{
    using System.Windows.Forms;

    public sealed class FormsKeyPresser
        : KeyPresser
    {
        void KeyPresser.Press(
            string keys)
        {
            SendKeys.Send(keys);
        }
    }
}
