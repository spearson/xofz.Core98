namespace xofz.UI.Forms.KeyPressers
{
    using System.Windows.Forms;

    public sealed class GeneralKeyPresser
        : KeyPresser
    {
        void KeyPresser.Press(
            string keys)
        {
            SendKeys.SendWait(keys);
        }
    }
}