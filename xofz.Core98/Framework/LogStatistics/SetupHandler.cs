namespace xofz.Framework.LogStatistics
{
    using xofz.UI;

    public class SetupHandler
    {
        public SetupHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            LogStatisticsUi ui,
            string name)
        {
            var r = this.runner;
            r.Run<DateResetter>(dr =>
            {
                dr.Reset(ui, name);
            });
        }

        protected readonly MethodRunner runner;
    }
}
