namespace xofz.Framework.Shutdown
{
    using xofz.UI;

    public class StartHandler
    {
        public StartHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle()
        {
            var r = this.runner;
            r?.Run<Do>(cleanup =>
                {
                    var uiFound = false;
                    r.Run<Ui, UiReaderWriter>(
                        (cleanupUi, uiRW) =>
                        {
                            uiFound = true;
                            uiRW.WriteSync(
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

            r?.Run<ProcessKiller>(killer =>
            {
                killer.Kill();
            });
        }

        protected readonly MethodRunner runner;
    }
}
