namespace xofz.UI.Forms.ToggleUis
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public partial class UserControlToggleUi
        : UserControlUi, ToggleUiV2
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

        [Browsable(truth)]
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
            get
            {
                ToggleUiV2 toggle = this;
                var tc = toggle.ToggledColor;
                Color backColor;
                try
                {
                    backColor = Color.FromName(tc);
                }
                catch
                {
                    backColor = defaultToggledColor;
                }

                return this.key.BackColor == backColor;
            }

            set
            {
                Color backColor;
                ToggleUiV2 toggle = this;
                if (value)
                {
                    try
                    {
                        backColor = Color.FromName(
                            toggle.ToggledColor);
                    }
                    catch
                    {
                        backColor = defaultToggledColor;
                    }

                    goto setKeyBackColor;
                }

                try
                {
                    backColor = Color.FromName(
                        toggle.UntoggledColor);
                }
                catch
                {
                    backColor = defaultUntoggledColor;
                }

                setKeyBackColor:
                this.key.BackColor = backColor;
            }
        }

        string ToggleUiV2.ToggledColor { get; set; }
            = defaultToggledColor.Name;

        string ToggleUiV2.UntoggledColor { get; set; }
            = defaultUntoggledColor.Name;

        string ToggleUiV2.PressedColor
        {
            get => this.key.FlatAppearance.MouseDownBackColor.ToString();

            set
            {
                Color color;
                try
                {
                    color = Color.FromName(value);
                }
                catch
                {
                    color = Color.GhostWhite;
                }

                this.key.FlatAppearance.MouseDownBackColor = color;
            }
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

        protected static readonly Color defaultToggledColor
            = Color.Lime;
        protected static readonly Color defaultUntoggledColor 
            = Color.DimGray;

        protected const bool truth = true;
    }
}