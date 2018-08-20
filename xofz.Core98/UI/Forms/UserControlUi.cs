namespace xofz.UI.Forms
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    public class UserControlUi : UserControl, Ui
    {
        public UserControlUi()
        {
            this.writeFinished = new AutoResetEvent(false);
        }

        ISynchronizeInvoke Ui.Root => this;

        AutoResetEvent Ui.WriteFinished => this.writeFinished;

        bool Ui.Disabled
        {
            get => !this.Enabled;

            set => this.Enabled = !value;
        }

        private readonly AutoResetEvent writeFinished;
    }
}
