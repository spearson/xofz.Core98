namespace xofz.UI.Forms
{
    using System;
    using System.Threading;
    using xofz.Internal;

    public partial class UserControlVncUi : UserControlUi, VncUi
    {
        static UserControlVncUi()
        {
            AppDomain.CurrentDomain.AssemblyResolve 
                += (sender, e) => EmbeddedAssemblyLoader.Load(e.Name);
        }

        public UserControlVncUi()
        {
            this.InitializeComponent();

            var h = this.Handle;
        }

        public event Action ConnectionLost;

        public event xofz.Action<string> ErrorConnecting;

        void VncUi.Connect(
            string hostName,
            string password,
            bool scaleToWindowSize,
            int port,
            bool viewOnly)
        {
            this.remoteDesktop.GetPassword = () => password;
            this.remoteDesktop.VncPort = port;
            try
            {
                this.remoteDesktop.Connect(
                    hostName,
                    viewOnly,
                    scaleToWindowSize);
            }
            catch (Exception e)
            {
                new Thread(() => this.ErrorConnecting?.Invoke(
                    e.Message)).Start();
            }
        }

        void VncUi.Disconnect()
        {
            this.remoteDesktop.Disconnect();
        }

        private void remoteDesktop_ConnectionLost(
            object sender,
            EventArgs e)
        {
            new Thread(() => this.ConnectionLost?.Invoke()).Start();
        }
    }
}
