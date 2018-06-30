namespace xofz.UI.Forms
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.UI;

    public partial class UserControlToggleUi 
        : UserControlUi, ToggleUi
    {
        public UserControlToggleUi()
        {
            this.InitializeComponent();

            var h = this.Handle;
        }

        public event Action<ToggleUi> Tapped;

        public event Action<ToggleUi> Pressed;

        public event Action<ToggleUi> Released;

        public string Label
        {
            get => this.key.Text;

            set
            {
                this.key.Text = value;
                this.Visible = true;
            }
        }

        bool ToggleUi.Visible
        {
            get => this.Visible;

            set => this.Visible = value;
        }

        bool ToggleUi.Toggled
        {
            get => this.key.BackColor == Color.Lime;

            set => this.key.BackColor = value ? Color.Lime : Color.DimGray;
        }

        private void key_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.Tapped?.Invoke(this));
        }

        private void key_MouseDown(object sender, MouseEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.Pressed?.Invoke(this));
        }

        private void key_MouseUp(object sender, MouseEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.Released?.Invoke(this));
        }
    }
}
