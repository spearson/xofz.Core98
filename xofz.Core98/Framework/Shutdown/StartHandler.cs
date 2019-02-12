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

        public virtual void Handle()
        {
            var w = this.web;
            w.Run<Do>(cleanup =>
                {
                    var uiFound = false;
                    w.Run<Ui>(cleanupUi =>
                        {
                            uiFound = true;
                            UiHelpers.WriteSync(
                                cleanupUi,
                                cleanup);
                        },
                        UiNames.Cleanup);

                    if (!uiFound)
                    {
                        cleanup();
                    }
                },
                MethodNames.Cleanup);
            Process.GetCurrentProcess().Kill();
        }

        protected readonly MethodWeb web;
    }
}
