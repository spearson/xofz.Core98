namespace xofz.UI.Forms
{
    using System.Windows.Forms;

    public class FormsKeyPresser
        : KeyPresser
    {
        public void Press(
            string keys)
        {
            SendKeys.Send(keys);
        }
    }
}
