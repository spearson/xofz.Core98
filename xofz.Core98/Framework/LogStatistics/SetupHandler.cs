namespace xofz.Framework.LogStatistics
{
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
            string name)
        {
            var w = this.web;
            w.Run<DateResetter>(dr =>
            {
                dr.Reset(ui, name);
            });
        }

        protected readonly MethodWeb web;
    }
}
