namespace xofz.UI.Forms.Login
{
    using System;
    using System.Drawing;
    using System.Security;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.UI.Login;

    public partial class FormLoginUi 
        : FormUi, LoginUiV2
    {
        public FormLoginUi(
            Form shell)
            : this(
                shell, 
                new SecureStringToolSet())
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

        public virtual event Do BackspaceKeyTapped;

        public virtual event Do LoginKeyTapped;

        public virtual event Do CancelKeyTapped;

        public virtual event Do KeyboardKeyTapped;

        string LoginUiV2.PasswordLabel
        {
            get => this.passwordTextBoxLabel.Text;

            set => this.passwordTextBoxLabel.Text = value;
        }

        string LoginUiV2.TimeRemainingLabel
        {
            get => this.durationRemainingLabelLabel.Text;

            set => this.durationRemainingLabelLabel.Text = value;
        }

        string LoginUiV2.BackspaceKeyLabel
        {
            get => this.backspaceKey.Text;

            set => this.backspaceKey.Text = value;
        }

        string LoginUiV2.ClearKeyLabel
        {
            get => this.clearKey.Text;

            set => this.clearKey.Text = value;
        }

        string LoginUiV2.LogInKeyLabel
        {
            get => this.loginKey.Text;

            set => this.loginKey.Text = value;
        }

        string LoginUiV2.CancelKeyLabel
        {
            get => this.cancelKey.Text;

            set => this.cancelKey.Text = value;
        }

        string LoginUiV2.KeyboardKeyLabel
        {
            get => this.keyboardKey.Text;

            set => this.keyboardKey.Text = value;
        }

        string LoginUiV2.Title
        {
            get => this.Text;

            set => this.Text = value;
        }

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
            get => this.durationRemainingLabel.Text;

            set => this.durationRemainingLabel.Text = value;
        }

        bool LoginUi.KeyboardKeyVisible
        {
            get => this.keyboardKey.Visible;

            set => this.keyboardKey.Visible = value;
        }

        void LoginUi.FocusPassword()
        {
            const byte zero = 0;
            var ptb = this.passwordTextBox;
            ptb.Focus();
            ptb.Select(
                ptb.Text.Length, 
                zero);
        }

        AccessLevel LoginUi.CurrentAccessLevel
        {
            get => this.currentLevel;

            set
            {
                this.currentLevel = value;
                var ll = this.levelLabel;
                ll.Text = value.ToString();
                ll.BackColor = this.determineColorForLevel(value);
            }
        }

        void PopupUi.Display()
        {
            var s = this.shell;
            this.Location = s.Location;
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

        protected virtual Color determineColorForLevel(
            AccessLevel level)
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

        protected virtual void loginKey_Click(
            object sender, 
            EventArgs e)
        {
            var lkt = this.LoginKeyTapped;
            if (lkt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => lkt.Invoke());
        }

        protected virtual void numKey_Click(
            object sender, 
            EventArgs e)
        {
            var key = (Button)sender;
            var ptb = this.passwordTextBox;
            if (!this.firstInputKeyPressed)
            {
                ptb.Text = key.Text;
                this.firstInputKeyPressed = true;
                goto focus;
            }

            ptb.Text += key.Text;

            focus:
            LoginUi ui = this;
            ui.FocusPassword();
        }

        protected virtual void clearKey_Click(
            object sender, 
            EventArgs e)
        {
            this.passwordTextBox.Text = null;

            LoginUi ui = this;
            ui.FocusPassword();
        }

        protected virtual void backspaceKey_Click(
            object sender,
            EventArgs e)
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

        protected virtual void cancelKey_Click(
            object sender, 
            EventArgs e)
        {
            var ckt = this.CancelKeyTapped;
            if (ckt == null)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                o => ckt.Invoke());
        }

        protected virtual void this_FormClosing(
            object sender, 
            FormClosingEventArgs e)
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

        protected virtual void passwordTextBox_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            this.firstInputKeyPressed = true;
        }

        protected virtual void keyboardKey_Click(
            object sender, 
            EventArgs e)
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
