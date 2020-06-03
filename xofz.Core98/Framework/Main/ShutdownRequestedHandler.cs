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
            var currentLevel = AccessLevel.None;
            var shutdownLevel = AccessLevel.None;
            r.Run<AccessController, SettingsHolder>(
                (ac, s) =>
                {
                    currentLevel = ac.CurrentAccessLevel;
                    shutdownLevel = s.ShutdownLevel;
                });

            if (currentLevel >= shutdownLevel)
            {
                shutdown?.Invoke();
                return;
            }

            logIn?.Invoke();
            r.Run<AccessController>(ac =>
            {
                if (ac.CurrentAccessLevel >= shutdownLevel)
                {
                    shutdown?.Invoke();
                }
            });
        }

        protected readonly MethodRunner runner;
    }
}
