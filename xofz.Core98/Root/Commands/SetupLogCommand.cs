namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Logging;
    using xofz.Framework.Logging.Logs;
    using xofz.Framework.Lotters;
    using xofz.Presentation;
    using xofz.UI;

    public class SetupLogCommand : Command
    {
        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            MethodWeb web,
            string eventLogLogName,
            string eventLogSourceName,
            string logDependencyName = null,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Gen<string> computeBackupLocation = default(Gen<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.web = web;
            this.eventLogLogName = eventLogLogName;
            this.eventLogSourceName = eventLogSourceName;
            this.logDependencyName = logDependencyName;
            this.textFileLogPath = null;
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
            string eventLogLogName,
            string eventLogSourceName,
            string logDependencyName = null,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false)
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.statisticsUi = statisticsUi;
            this.web = web;
            this.eventLogLogName = eventLogLogName;
            this.eventLogSourceName = eventLogSourceName;
            this.logDependencyName = logDependencyName;
            this.textFileLogPath = null;
            this.clearLevel = clearLevel;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.computeBackupLocation = default(
                Gen<string>);
            this.statisticsEnabled = true;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            LogStatisticsUi statisticsUi,
            MethodWeb web,
            string eventLogLogName,
            string eventLogSourceName,
            string logDependencyName = null,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Gen<string> computeBackupLocation = default(Gen<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.statisticsUi = statisticsUi;
            this.web = web;
            this.eventLogLogName = eventLogLogName;
            this.eventLogSourceName = eventLogSourceName;
            this.textFileLogPath = null;
            this.logDependencyName = logDependencyName;
            this.clearLevel = clearLevel;
            this.editLevel = editLevel;
            this.resetOnStart = resetOnStart;
            this.computeBackupLocation = computeBackupLocation;
            this.statisticsEnabled = true;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editorUi,
            MethodWeb web,
            string textFileLogPath = @"Log.log",
            string logDependencyName = null,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Gen<string> computeBackupLocation = default(Gen<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.web = web;
            this.textFileLogPath = textFileLogPath;
            this.logDependencyName = logDependencyName;
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
            string textFileLogPath = @"Log.log",
            string logDependencyName = null,
            AccessLevel clearLevel = AccessLevel.None,
            AccessLevel editLevel = AccessLevel.None,
            bool resetOnStart = false,
            Gen<string> computeBackupLocation = default(Gen<string>))
        {
            this.ui = ui;
            this.shell = shell;
            this.editorUi = editorUi;
            this.statisticsUi = statisticsUi;
            this.web = web;
            this.textFileLogPath = textFileLogPath;
            this.logDependencyName = logDependencyName;
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
                {
                    Name = this.logDependencyName
                }
                .Setup();

            new LogEditorPresenter(
                    this.editorUi,
                    w)
                {
                    Name = this.logDependencyName
                }
                .Setup();

            if (se)
            {
                new LogStatisticsPresenter(
                        this.statisticsUi,
                        w)
                    {
                        Name = this.logDependencyName
                    }
                    .Setup();
            }
        }

        private void registerDependencies()
        {
            var w = this.web;
            var path = this.textFileLogPath;
            if (path != null)
            {
                w.RegisterDependency(
                    new TextFileLog(path),
                    this.logDependencyName);
                goto finish;
            }

            w.RegisterDependency(
                new EventLogLog(
                    this.eventLogLogName,
                    this.eventLogSourceName),
                this.logDependencyName);

            finish:
            w.RegisterDependency(
                new LinkedListLotter(),
                "LogLotter");
            var se = this.statisticsEnabled;
            w.RegisterDependency(
                new LogSettings
                {
                    EditLevel = this.editLevel,
                    ClearLevel = this.clearLevel,
                    ComputeBackupLocation = this.computeBackupLocation,
                    ResetOnStart = this.resetOnStart,
                    StatisticsEnabled = se
                },
                this.logDependencyName);
            if (se)
            {
                w.RegisterDependency(
                    new LogStatistics(w),
                    this.logDependencyName);
            }
        }

        private readonly LogUi ui;
        private readonly ShellUi shell;
        private readonly LogEditorUi editorUi;
        private readonly LogStatisticsUi statisticsUi;
        private readonly MethodWeb web;
        private readonly string textFileLogPath;
        private readonly string eventLogLogName;
        private readonly string eventLogSourceName;
        private readonly AccessLevel editLevel;
        private readonly AccessLevel clearLevel;
        private readonly bool resetOnStart;
        private readonly Gen<string> computeBackupLocation;
        private readonly bool statisticsEnabled;
        private readonly string logDependencyName;
    }
}
