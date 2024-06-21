namespace xofz.UI.Forms
{
    using System.ComponentModel;
    using System.Windows.Forms;

    public class UserControlUi : UserControl, Ui
    {
        ISynchronizeInvoke Ui.Root => this;

        bool Ui.Disabled
        {
            get => !this.Enabled;

            set => this.Enabled = !value;
        }
    }
}