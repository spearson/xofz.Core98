namespace xofz.Framework.Shutdown
{
    using System.Diagnostics;
    using xofz.UI;

    public class StartHandler
    {
        public StartHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            Ui mainUi,
            Do cleanup)
        {
            if (cleanup == null)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

            if (mainUi == null)
            {
                cleanup();
                Process.GetCurrentProcess().Kill();
                return;
            }

            UiHelpers.WriteSync(mainUi, cleanup);
            Process.GetCurrentProcess().Kill();
        }

        protected readonly MethodWeb web;
    }
}
