namespace xofz.UI.Forms
{
    using System;
    using System.Drawing;
    using System.Security;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.UI;

    public partial class FormLoginUi 
        : FormUi, LoginUi
    {
        public FormLoginUi(Form shell)
            : this(shell, new SecureStringToolSet())
        {
        }

        public FormLoginUi(
            Form shell,
            SecureStringToolSet ssts)
        {
            this.shell = shell;
            this.ssts = ssts;

            this.InitializeComponent();

            var h = this.Handle;
        }

        private FormLoginUi()
        {
            this.InitializeComponent();
        }

        public event Do BackspaceKeyTapped;

        public event Do LoginKeyTapped;

        public event Do CancelKeyTapped;

        public event Do KeyboardKeyTapped;

        SecureString LoginUi.CurrentPassword
        {
            get => this.ssts.Encode(
                this.passwordTextBox.Text);

            set
            {
                var ptb = this.passwordTextBox;
                if (value == null)
                {
                    ptb.Text = null;
                    return;
                }

                ptb.Text = this.ssts.Decode(value);
            }
        }

        string LoginUi.TimeRemaining
        {
            get => this.timeRemainingLabel.Text;

            set => this.timeRemainingLabel.Text = value;
        }

        bool LoginUi.KeyboardKeyVisible
        {
            get => this.keyboardKey.Visible;

            set => this.keyboardKey.Visible = value;
        }

        void LoginUi.FocusPassword()
        {
            var ptb = this.passwordTextBox;
            ptb.Focus();
            ptb.Select(ptb.Text.Length, 0);
        }

        AccessLevel LoginUi.CurrentAccessLevel
        {
            get => this.currentLevel;

            set
            {
                this.currentLevel = value;
                this.Text = @"Log In [Current Access Level: "
                            + value + @"]";
                var ll = this.levelLabel;
                ll.Text = value.ToString();
                ll.BackColor = this.determineColorForLevel(value);
            }
        }

        void PopupUi.Display()
        {
            var s = this.shell;
            this.Location = new Point(
                s.Location.X, 
                s.Location.Y);
            if (!this.Visible)
            {
                this.Show(s);
            }

            var ptb = this.passwordTextBox;
            ptb.Focus();
            ptb.SelectAll();
            if (string.IsNullOrEmpty(ptb.Text))
            {
                ptb.Focus();
            }

            this.firstInputKeyPressed = false;
        }

        void PopupUi.Hide()
        {
            this.Visible = false;
        }

        private Color determineColorForLevel(AccessLevel level)
        {
            switch (level)
            {
                case AccessLevel.None:
                    return SystemColors.Control;
                case AccessLevel.Level1:
                    return Color.Tan;
                case AccessLevel.Level2:
                    return SystemColors.ActiveCaption;
                default:
                    return SystemColors.ActiveCaption;
            }
        }

        private void loginKey_Click(object sender, EventArgs e)
        {
            var lkt = this.LoginKeyTapped;
            if (lkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => lkt.Invoke());
        }

        private void numKey_Click(object sender, EventArgs e)
        {
            var key = (Button)sender;
            var ptb = this.passwordTextBox;
            if (!this.firstInputKeyPressed)
            {
                ptb.Text = key.Text;
                this.firstInputKeyPressed = true;
            }
            else
            {
                ptb.Text += key.Text;
            }

            LoginUi ui = this;
            ui.FocusPassword();
        }

        private void clearKey_Click(object sender, EventArgs e)
        {
            this.passwordTextBox.Text = null;

            LoginUi ui = this;
            ui.FocusPassword();
        }

        private void backspaceKey_Click(object sender, EventArgs e)
        {
            var bkt = this.BackspaceKeyTapped;
            if (bkt == null)
            {
                return;
            }

            this.firstInputKeyPressed = true;
            ThreadPool.QueueUserWorkItem(
                o => bkt.Invoke());
        }

        private void cancelKey_Click(object sender, EventArgs e)
        {
            var ckt = this.CancelKeyTapped;
            if (ckt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => ckt.Invoke());
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            var ckt = this.CancelKeyTapped;
            if (ckt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => ckt.Invoke());
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.firstInputKeyPressed = true;
        }

        private void keyboardKey_Click(object sender, EventArgs e)
        {
            var kkt = this.KeyboardKeyTapped;
            if (kkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => kkt.Invoke());
        }

        protected bool firstInputKeyPressed;
        protected AccessLevel currentLevel;
        protected readonly Form shell;
        protected readonly SecureStringToolSet ssts;

    }
}
