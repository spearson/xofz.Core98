namespace xofz.UI.Forms
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    public class ControlUi : Control, Ui
    {
        public ControlUi()
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
