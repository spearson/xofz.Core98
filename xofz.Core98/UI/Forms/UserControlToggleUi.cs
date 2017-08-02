namespace xofz.UI.Forms
{
    using System;
    using System.Drawing;
    using System.Threading;
    using xofz.UI;

    public partial class UserControlToggleUi : UserControlUi, ToggleUi
    {
        public UserControlToggleUi()
        {
            this.InitializeComponent();
        }

        public event Action<ToggleUi> Tapped;

        public string Label
        {
            get { return this.key.Text; }

            set
            {
                this.key.Text = value;
                this.Visible = true;
            }
        }

        bool ToggleUi.Visible
        {
            get { return this.Visible; }

            set { this.Visible = value; }
        }

        bool ToggleUi.Toggled
        {
            get { return this.key.BackColor == Color.Lime; }

            set { this.key.BackColor = value ? Color.Lime : Color.DimGray; }
        }

        private void key_Click(object sender, EventArgs e)
        {
            new Thread(() => this.Tapped?.Invoke(this)).Start();
        }
    }
}
