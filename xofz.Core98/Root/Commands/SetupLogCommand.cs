namespace xofz.Root.Commands
{
    using System.Collections.Generic;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.Framework.Logging.Logs;
    using xofz.Framework.Lotters;
    using xofz.Presentation;
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
            settings.StatisticsEnabled = false;
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
            settings.StatisticsEnabled = true;
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
            settings.StatisticsEnabled = false;
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
            settings.StatisticsEnabled = true;
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
                }
                .Setup();

            var eui = this.editUi;
            if (eui != null)
            {
                new LogEditorPresenter(
                        eui,
                        w)
                    {
                        Name = n
                    }
                    .Setup();
            }

            if (s?.StatisticsEnabled ?? false)
            {
                new LogStatisticsPresenter(
                        this.statsUi,
                        w)
                    {
                        Name = n
                    }
                    .Setup();
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
            var registered = false;
            w?.Run<Log>(log =>
                {
                    registered = true;
                },
                ldn);
            if (registered)
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
            registered = false;
            var ln = DependencyNames.Lotter;
            w?.Run<Lotter>(lotter =>
                {
                    registered = true;
                },
                ln);
            if (!registered)
            {
                w?.RegisterDependency(
                    new LinkedListLotter(),
                    ln);
            }

            w?.RegisterDependency(
                s, 
                ldn);
            w?.RegisterDependency(
                new LinkedList<LogEntry>(), 
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

            registered = false;
            w?.Run<Framework.Log.EntryReloader>(reloader =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.EntryReloader(w));
            }

            registered = false;
            w?.Run<Framework.Log.EntryConverter>(converter =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.EntryConverter(w));
            }

            registered = false;
            w?.Run<Framework.Log.FilterChecker>(converter =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.FilterChecker(w));
            }

            registered = false;
            w?.Run<Framework.Log.DateAndFilterResetter>(resetter =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.DateAndFilterResetter(w));
            }

            registered = false;
            w?.Run<Framework.Log.TimeProvider>(
                provider =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.TimeProvider());
            }

            registered = false;
            w?.Run<Framework.Log.SetupHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.SetupHandler(w));
            }

            registered = false;
            w?.Run<Framework.Log.StartHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.StartHandler(w));
            }

            registered = false;
            w?.Run<Framework.Log.AddKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.AddKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<AccessLevelChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new AccessLevelChangedHandler(w));
            }

            registered = false;
            w?.Run<DateRangeChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new DateRangeChangedHandler(w));
            }

            registered = false;
            w?.Run<FilterTextChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new FilterTextChangedHandler(w));
            }

            registered = false;
            w?.Run<StatisticsKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new StatisticsKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<ClearKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new ClearKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<EntryWrittenHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new EntryWrittenHandler(w));
            }

            registered = false;
            w?.Run<Labels>(labels =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Labels());
            }

            registered = false;
            w?.Run<LabelApplier>(applier =>
            {
                registered = true;
            });

            if (!registered)
            {
                w?.RegisterDependency(
                    new LabelApplier(w));
            }

            registered = false;
            w?.Run<PreviousWeekKeyTappedHandler>(
                handler =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new PreviousWeekKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<CurrentWeekKeyTappedHandler>(
                handler =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new CurrentWeekKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<NextWeekKeyTappedHandler>(
                handler =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new NextWeekKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<KeyPresser>(
                presser =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new GeneralKeyPresser());
            }

            registered = false;
            w?.Run<DownKeyTappedHandler>(
                handler =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new DownKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<UpKeyTappedHandler>(
                handler =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new UpKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<ResetContentKeyTappedHandler>(
                handler =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new ResetContentKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<ResetTypeKeyTappedHandler>(
                handler =>
                {
                    registered = true;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new ResetTypeKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<xofz.Framework.LogEditor.SetupHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.SetupHandler(w));
            }

            registered = false;
            w?.Run<xofz.Framework.LogEditor.TypeChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.TypeChangedHandler(w));
            }

            registered = false;
            w?.Run<xofz.Framework.LogEditor.AddKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.AddKeyTappedHandler(w));
            }

            registered = false;
            w?.Run<xofz.Framework.LogEditor.Labels>(labels =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.Labels());
            }

            registered = false;
            w?.Run<xofz.Framework.LogEditor.LabelApplier>(applier =>
            {
                registered = true;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.LabelApplier(w));
            }

            if (se)
            {
                registered = false;
                w?.Run<xofz.Framework.LogStatistics.SetupHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            SetupHandler(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.StartHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.StartHandler(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.
                    ResetContentKeyTappedHandler>(handler =>
                {
                    registered = true;
                });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            ResetContentKeyTappedHandler(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.ResetTypeKeyTappedHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            ResetTypeKeyTappedHandler(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.StatsDisplayer>(sd =>
                {
                    registered = true;
                });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.StatsDisplayer(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.FilterSetter>(fs =>
                {
                    registered = true;
                });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.FilterSetter(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.OverallKeyTappedHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            OverallKeyTappedHandler(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.RangeKeyTappedHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            RangeKeyTappedHandler(w));
                }


                registered = false;
                w?.Run<xofz.Framework.LogStatistics.DateResetter>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            DateResetter(w));
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.Labels>(
                    labels =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            Labels());
                }

                registered = false;
                w?.Run<xofz.Framework.LogStatistics.LabelApplier>(
                    applier =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
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
    }
}
