namespace xofz.Framework.LogStatistics
{
    using xofz.UI.LogStatistics;

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
            r?.Run<DateResetter>(dr =>
            {
                dr.Reset(ui, name);
            });

            if (ui is LogStatisticsUiV2 v2)
            {
                r?.Run<LabelApplier>(applier =>
                {
                    applier.Apply(v2);
                });
            }
        }

        protected readonly MethodRunner runner;
    }
}