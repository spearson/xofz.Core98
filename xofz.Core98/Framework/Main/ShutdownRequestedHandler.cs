namespace xofz.Framework.Main
{
    using xofz.UI;

    public class ShutdownRequestedHandler
    {
        public ShutdownRequestedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            MainUi ui,
            Do logIn,
            Do shutdown)
        {
            var r = this.runner;
            var cal = AccessLevel.None;
            var shutdownLevel = AccessLevel.None;
            r.Run<AccessController, SettingsHolder>(
                (ac, s) =>
                {
                    cal = ac.CurrentAccessLevel;
                    shutdownLevel = s.ShutdownLevel;
                });

            if (cal >= shutdownLevel)
            {
                shutdown?.Invoke();
                return;
            }

            logIn?.Invoke();
            r.Run<AccessController>(ac =>
            {
                cal = ac.CurrentAccessLevel;
                if (cal >= shutdownLevel)
                {
                    shutdown?.Invoke();
                }
            });
        }

        protected readonly MethodRunner runner;
    }
}
