namespace xofz.Root.Commands
{
    using System;
    using xofz.Framework;
    using xofz.Framework.Logging;
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
            string logName,
            string sourceName,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Func<string> computeBackupLocation = default(Func<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.web = web;
            this.logName = logName;
            this.sourceName = sourceName;
            this.filePath = null;
            this.clearLevel = clearLevel;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.computeBackupLocation = computeBackupLocation;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            LogStatisticsUi statisticsUi,
            MethodWeb web,
            string logName,
            string sourceName,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false)
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.statisticsUi = statisticsUi;
            this.web = web;
            this.logName = logName;
            this.sourceName = sourceName;
            this.filePath = null;
            this.clearLevel = clearLevel;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.computeBackupLocation = default(
                Func<string>);
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            LogStatisticsUi statisticsUi,
            MethodWeb web,
            string logName,
            string sourceName,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Func<string> computeBackupLocation = default(Func<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.statisticsUi = statisticsUi;
            this.web = web;
            this.logName = logName;
            this.sourceName = sourceName;
            this.filePath = null;
            this.clearLevel = clearLevel;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.computeBackupLocation = computeBackupLocation;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            MethodWeb web,
            string filePath = @"Log.log",
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Func<string> computeBackupLocation = default(Func<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.web = web;
            this.filePath = filePath;
            this.clearLevel = clearLevel;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.computeBackupLocation = computeBackupLocation;
            this.statisticsEnabled = false;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            LogStatisticsUi statisticsUi,
            MethodWeb web,
            string filePath = @"Log.log",
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Func<string> computeBackupLocation = default(Func<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.statisticsUi = statisticsUi;
            this.web = web;
            this.filePath = filePath;
            this.editLevel = editLevel;
            this.clearLevel = clearLevel;
            this.resetOnStart = resetOnStart;
            this.computeBackupLocation = computeBackupLocation;
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
                    this.clearLevel,
                    this.computeBackupLocation,
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
            var ln = this.logName;
            if (ln == null)
            {
                w.RegisterDependency(
                    new TextFileLog(
                        this.filePath));
                goto finish;
            }

            w.RegisterDependency(
                new EventLogLog(
                    ln,
                    this.sourceName));

            finish:
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
        private readonly string logName;
        private readonly string sourceName;
        private readonly AccessLevel editLevel;
        private readonly AccessLevel clearLevel;
        private readonly bool resetOnStart;
        private readonly Func<string> computeBackupLocation;
        private readonly bool statisticsEnabled;
    }
}
