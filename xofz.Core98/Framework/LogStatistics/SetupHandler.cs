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

            var v2 = ui as LogStatisticsUiV2;
            if (v2 != null)
            {
                r.Run<LabelApplier>(applier =>
                {
                    applier.Apply(v2);
                });
            }
        }

        protected readonly MethodRunner runner;
    }
}
