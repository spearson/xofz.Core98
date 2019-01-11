namespace xofz.Framework.Log
{
    public class StatisticsKeyTappedHandler
    {
        public StatisticsKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            Do presentStats)
        {
            var w = this.web;
            presentStats?.Invoke();
        }

        protected readonly MethodWeb web;
    }
}
