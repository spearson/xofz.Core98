namespace xofz.UI.Forms
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.UI;

    public partial class FormLoginUi 
        : FormUi, LoginUi
    {
        public FormLoginUi(Form shell)
        {
            this.shell = shell;

            this.InitializeComponent();

            this.FormClosing += this.this_FormClosing;
            var h = this.Handle;
        }

        public event Action BackspaceKeyTapped;

        public event Action LoginKeyTapped;

        public event Action CancelKeyTapped;

        public event Action KeyboardKeyTapped;

        string LoginUi.CurrentPassword
        {
            get => this.passwordTextBox.Text;

            set => this.passwordTextBox.Text = value;
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
            get => this.currentAccessLevel;

            set
            {
                this.currentAccessLevel = value;
                this.Text = @"Log In [Current Access Level: "
                            + value + @"]";
                this.levelLabel.Text = value.ToString();
                this.levelLabel.BackColor = this.determineColorForLevel(value);
            }
        }

        void PopupUi.Display()
        {
            this.Location = new Point(
                this.shell.Location.X, 
                this.shell.Location.Y);
            this.Visible = true;
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
            ThreadPool.QueueUserWorkItem(
                o => this.LoginKeyTapped?.Invoke());
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
            ThreadPool.QueueUserWorkItem(
                o => this.BackspaceKeyTapped?.Invoke());
        }

        private void cancelKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.CancelKeyTapped?.Invoke());
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ThreadPool.QueueUserWorkItem(
                o => this.CancelKeyTapped?.Invoke());
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.firstInputKeyPressed = true;
        }

        private void keyboardKey_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(
                o => this.KeyboardKeyTapped?.Invoke());
        }

        private bool firstInputKeyPressed;
        private AccessLevel currentAccessLevel;
        private readonly Form shell;
    }
}
