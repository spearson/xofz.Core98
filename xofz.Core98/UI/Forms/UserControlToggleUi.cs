namespace xofz.UI.Forms
{
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

        public virtual event Do<ToggleUi> Tapped;

        public virtual event Do<ToggleUi> Pressed;

        public virtual event Do<ToggleUi> Released;

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

        protected virtual void key_Click(
            object sender, 
            System.EventArgs e)
        {
            var t = this.Tapped;
            if (t == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => t.Invoke(this));
        }

        protected virtual void key_MouseDown(
            object sender, 
            MouseEventArgs e)
        {
            var p = this.Pressed;
            if (p == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => p.Invoke(this));
        }

        protected virtual void key_MouseUp(
            object sender, 
            MouseEventArgs e)
        {
            var r = this.Released;
            if (r == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => r.Invoke(this));
        }
    }
}
