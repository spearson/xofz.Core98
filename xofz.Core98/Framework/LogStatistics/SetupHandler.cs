namespace xofz.Framework.LogStatistics
{
    using xofz.Framework.Logging;
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            LogStatistics stats)
        {
            var w = this.web;
            w.Run<DateResetter>(dr =>
            {
                dr.Reset(ui, stats);
            });
        }

        protected readonly MethodWeb web;
    }
}
