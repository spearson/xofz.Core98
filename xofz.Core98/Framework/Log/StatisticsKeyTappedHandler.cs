namespace xofz.Framework.Log
{
    using xofz.UI;

    public class StatisticsKeyTappedHandler
    {
        public StatisticsKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            Do presentStats)
        {
            var r = this.runner;
            presentStats?.Invoke();
        }

        public virtual void Handle(
            Do<string> presentStats,
            string name)
        {
            var r = this.runner;
            presentStats?.Invoke(name);
        }

        public virtual void Handle(
            Do<string> presentStats,
            LogUi ui,
            string name)
        {
            var r = this.runner;
            presentStats?.Invoke(name);
        }

        protected readonly MethodRunner runner;
    }
}
