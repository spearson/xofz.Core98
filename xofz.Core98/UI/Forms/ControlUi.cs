namespace xofz.UI.Forms
{
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ControlUi : Control, Ui
    {
        public ControlUi()
        {
        }

        ISynchronizeInvoke Ui.Root => this;

        bool Ui.Disabled
        {
            get => !this.Enabled;

            set => this.Enabled = !value;
        }
    }
}
