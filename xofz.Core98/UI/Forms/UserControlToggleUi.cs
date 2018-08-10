namespace xofz.UI.Forms
{
    using System;
    using System.ComponentModel;
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

        public virtual string Label
        {
            get => this.key.Text;

            set
            {
                this.key.Text = value;
                this.Visible = true;
            }
        }

        [Browsable(true)]
        public virtual Font KeyFont
        {
            get => this.key.Font;

            set => this.key.Font = value;
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
            var t = this.Tapped;
            if (t == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => t.Invoke(this));
        }

        private void key_MouseDown(object sender, MouseEventArgs e)
        {
            var p = this.Pressed;
            if (p == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => p.Invoke(this));
        }

        private void key_MouseUp(object sender, MouseEventArgs e)
        {
            var r = this.Released;
            if (r == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(o => r.Invoke(this));
        }
    }
}
