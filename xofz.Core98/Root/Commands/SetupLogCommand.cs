namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Implementation;
    using xofz.Framework.Materialization;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupLogCommand : Command
    {
        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            MethodWeb web,
            string filePath = @"Log.log",
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false)
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.web = web;
            
            this.filePath = filePath;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;

            this.statisticsEnabled = false;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            LogStatisticsUi statisticsUi,
            MethodWeb web,
            string filePath = @"Log.log",
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false)
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.statisticsUi = statisticsUi;
            this.web = web;

            this.filePath = filePath;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;

            this.statisticsEnabled = true;
        }

        public override void Execute()
        {
            this.registerDependencies();
            var w = this.web;
            var se = this.statisticsEnabled;
            var ros = this.resetOnStart;
            new LogPresenter(
                    this.ui,
                    this.shell,
                    w)
                .Setup(
                    this.editLevel,
                    ros,
                    se);

            new LogEditorPresenter(
                    this.editorUi,
                    w)
                .Setup();

            if (se)
            {
                new LogStatisticsPresenter(
                        this.statisticsUi,
                        w)
                    .Setup(ros);
            }
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new TextFileLog(this.filePath));
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                "LogTimer");
            w.RegisterDependency(
                new LinkedListMaterializer(),
                "LogMaterializer");
            if (this.statisticsEnabled)
            {
                w.RegisterDependency(
                    new LogStatistics(w));
            }
        }

        private readonly LogUi ui;
        private readonly ShellUi shell;
        private readonly LogEditorUi editorUi;
        private readonly LogStatisticsUi statisticsUi;
        private readonly MethodWeb web;
        private readonly string filePath;
        private readonly AccessLevel editLevel;
        private readonly bool resetOnStart;
        private readonly bool statisticsEnabled;
    }
}
