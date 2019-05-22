namespace xofz.Framework.Log
{
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

        protected readonly MethodRunner runner;
    }
}
