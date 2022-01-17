namespace xofz.Root.Commands
{
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.Framework.Logging.Logs;
    using xofz.Framework.Lotters;
    using xofz.Presentation.Presenters;
    using xofz.UI;
    using xofz.UI.Forms;

    public class SetupLogCommand
        : Command
    {
        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            SettingsHolder settings,
            MethodWeb web)
        {
            this.ui = ui;
            this.shell = shell;
            settings = settings
                       ?? new SettingsHolder();
            settings.StatisticsEnabled = falsity;
            this.settings = settings;
            this.web = web;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogStatisticsUi statsUi,
            SettingsHolder settings,
            MethodWeb web)
        {
            this.ui = ui;
            this.shell = shell;
            settings = settings
                       ?? new SettingsHolder();
            settings.StatisticsEnabled = truth;
            this.settings = settings;
            this.statsUi = statsUi;
            this.web = web;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editUi,
            SettingsHolder settings,
            MethodWeb web)
        {
            this.ui = ui;
            this.shell = shell;
            this.editUi = editUi;
            settings = settings
                       ?? new SettingsHolder();
            settings.StatisticsEnabled = falsity;
            this.settings = settings;
            this.web = web;
        }

        public SetupLogCommand(
            LogUi ui,
            ShellUi shell,
            LogEditorUi editUi,
            LogStatisticsUi statsUi,
            SettingsHolder settings,
            MethodWeb web)
        {
            this.ui = ui;
            this.editUi = editUi;
            this.shell = shell;
            this.statsUi = statsUi;
            settings = settings
                       ?? new SettingsHolder();
            settings.StatisticsEnabled = truth;
            this.settings = settings;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();

            var w = this.web;
            var s = this.settings;
            var n = s?.LogDependencyName;
            new LogPresenter(
                this.ui,
                this.shell,
                w)
            {
                Name = n
            }.Setup();

            var eui = this.editUi;
            if (eui != null)
            {
                new LogEditorPresenter(
                    eui,
                    w)
                {
                    Name = n
                }.Setup();
            }

            if (s?.StatisticsEnabled ?? falsity)
            {
                new LogStatisticsPresenter(
                    this.statsUi,
                    w)
                {
                    Name = n
                }.Setup();
            }
        }

        protected virtual void registerDependencies()
        {
            var w = this.web;
            var s = this.settings ??
                    new SettingsHolder();
            var ldn = s.LogDependencyName;
            var location = s.LogLocation;
            var se = s.StatisticsEnabled;

            if (w.Run<Delayer>() == null)
            {
                w?.RegisterDependency(
                    new Delayer());
            }

            if (w?.Run<Log>(null, ldn) != null)
            {
                goto checkLotter;
            }

            if (location == null)
            {
                return;
            }

            var location2 = s.SecondaryLogLocation;
            if (location2 == null)
            {
                w?.RegisterDependency(
                    new TextFileLog(location),
                    ldn);
                goto checkLotter;
            }

            w?.RegisterDependency(
                new EventLogLog(location, location2),
                ldn);

            checkLotter:
            var ln = DependencyNames.Lotter;
            if (w?.Run<Lotter>(null, ln) == null)
            {
                w?.RegisterDependency(
                    new XLinkedListLotter(),
                    ln);
            }

            w?.RegisterDependency(
                s,
                ldn);
            w?.RegisterDependency(
                new XLinkedList<LogEntry>(),
                ldn);
            w?.RegisterDependency(
                new Framework.Log.FieldHolder(),
                ldn);
            if (se)
            {
                w?.RegisterDependency(
                    new LogStatistics(w)
                    {
                        LogDependencyName = ldn
                    },
                    ldn);
                w?.RegisterDependency(
                    this.statsUi,
                    ldn);
                w?.RegisterDependency(
                    new Framework.LogStatistics.SettingsHolder(),
                    ldn);
            }

            if (w?.Run<EntryReloader>() == null)
            {
                w?.RegisterDependency(
                    new EntryReloader(w));
            }

            if (w?.Run<EntryConverter>() == null)
            {
                w?.RegisterDependency(
                    new EntryConverter(w));
            }

            if (w?.Run<FilterChecker>() == null)
            {
                w?.RegisterDependency(
                    new FilterChecker(w));
            }

            if (w?.Run<DateAndFilterResetter>() == null)
            {
                w?.RegisterDependency(
                    new DateAndFilterResetter(w));
            }

            if (w?.Run<TimeProvider>() == null)
            {
                w?.RegisterDependency(
                    new TimeProvider());
            }

            if (w?.Run<SetupHandler>() == null)
            {
                w?.RegisterDependency(
                    new SetupHandler(w));
            }

            if (w?.Run<StartHandler>() == null)
            {
                w?.RegisterDependency(
                    new StartHandler(w));
            }

            if (w?.Run<AddKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new AddKeyTappedHandler(w));
            }

            if (w?.Run<AccessLevelChangedHandler>() == null)
            {
                w?.RegisterDependency(
                    new AccessLevelChangedHandler(w));
            }

            if (w?.Run<DateRangeChangedHandler>() == null)
            {
                w?.RegisterDependency(
                    new DateRangeChangedHandler(w));
            }

            if (w?.Run<FilterTextChangedHandler>() == null)
            {
                w?.RegisterDependency(
                    new FilterTextChangedHandler(w));
            }

            if (w?.Run<StatisticsKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new StatisticsKeyTappedHandler(w));
            }

            if (w?.Run<ClearKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new ClearKeyTappedHandler(w));
            }

            if (w?.Run<EntryWrittenHandler>() == null)
            {
                w?.RegisterDependency(
                    new EntryWrittenHandler(w));
            }

            if (w?.Run<Labels>() == null)
            {
                w?.RegisterDependency(
                    new Labels());
            }

            if (w?.Run<LabelApplier>() == null)
            {
                w?.RegisterDependency(
                    new LabelApplier(w));
            }

            if (w?.Run<PreviousWeekKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new PreviousWeekKeyTappedHandler(w));
            }

            if (w?.Run<CurrentWeekKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new CurrentWeekKeyTappedHandler(w));
            }

            if (w?.Run<NextWeekKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new NextWeekKeyTappedHandler(w));
            }

            if (w?.Run<NewestKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new NewestKeyTappedHandler(w));
            }

            if (w?.Run<OldestKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new OldestKeyTappedHandler(w));
            }

            if (w?.Run<KeyPresser>() == null)
            {
                w?.RegisterDependency(
                    new GeneralKeyPresser());
            }

            if (w?.Run<DownKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new DownKeyTappedHandler(w));
            }

            if (w?.Run<UpKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new UpKeyTappedHandler(w));
            }

            if (w?.Run<ResetContentKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new ResetContentKeyTappedHandler(w));
            }

            if (w?.Run<ResetTypeKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new ResetTypeKeyTappedHandler(w));
            }

            if (w?.Run<Framework.LogEditor.SetupHandler>() == null)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.SetupHandler(w));
            }

            if (w?.Run<Framework.LogEditor.TypeChangedHandler>() == null)
            {
                w?.RegisterDependency(
                    new Framework.LogEditor.TypeChangedHandler(w));
            }

            if (w?.Run<Framework.LogEditor.AddKeyTappedHandler>() == null)
            {
                w?.RegisterDependency(
                    new Framework.LogEditor.AddKeyTappedHandler(w));
            }

            if (w?.Run<Framework.LogEditor.Labels>() == null)
            {
                w?.RegisterDependency(
                    new Framework.LogEditor.Labels());
            }

            if (w?.Run<Framework.LogEditor.LabelApplier>() == null)
            {
                w?.RegisterDependency(
                    new Framework.LogEditor.LabelApplier(w));
            }

            if (se)
            {
                if (w?.Run<Framework.LogStatistics.SetupHandler>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.SetupHandler(w));
                }

                if (w?.Run<Framework.LogStatistics.StartHandler>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.StartHandler(w));
                }

                if (w?.
                    Run<Framework.LogStatistics.
                        ResetContentKeyTappedHandler>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.
                            ResetContentKeyTappedHandler(w));
                }

                if (w?.
                        Run<Framework.LogStatistics.
                            ResetTypeKeyTappedHandler>() ==
                    null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.
                            ResetTypeKeyTappedHandler(w));
                }

                if (w?.Run<Framework.LogStatistics.StatsDisplayer>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.StatsDisplayer(w));
                }

                if (w?.Run<Framework.LogStatistics.FilterSetter>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.FilterSetter(w));
                }

                if (w?.Run<Framework.LogStatistics.OverallKeyTappedHandler>() ==
                    null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.
                            OverallKeyTappedHandler(w));
                }

                if (w?.Run<Framework.LogStatistics.RangeKeyTappedHandler>() ==
                    null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.
                            RangeKeyTappedHandler(w));
                }


                if (w?.Run<Framework.LogStatistics.DateResetter>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.
                            DateResetter(w));
                }

                if (w?.Run<Framework.LogStatistics.Labels>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.
                            Labels());
                }

                if (w?.Run<Framework.LogStatistics.LabelApplier>() == null)
                {
                    w?.RegisterDependency(
                        new Framework.LogStatistics.
                            LabelApplier(w));
                }
            }
        }

        protected readonly LogUi ui;
        protected readonly LogEditorUi editUi;
        protected readonly ShellUi shell;
        protected readonly LogStatisticsUi statsUi;
        protected readonly SettingsHolder settings;
        protected readonly MethodWeb web;
        protected const bool
            truth = true,
            falsity = false;
    }
}
