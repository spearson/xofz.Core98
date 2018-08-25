namespace xofz.UI.Forms
{
    using System.ComponentModel;
    using System.Windows.Forms;

    public class FormUi : Form, Ui
    {
        ISynchronizeInvoke Ui.Root => this;
       
        bool Ui.Disabled
        {
            get => !this.Enabled;

            set => this.Enabled = !value;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormUi
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormUi";
            this.ResumeLayout(false);

        }
    }
}
