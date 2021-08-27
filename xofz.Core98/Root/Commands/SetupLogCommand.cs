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

            if (s?.StatisticsEnabled ?? falsity)
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
            var registered = falsity;

            w?.Run<Delayer>(log =>
                {
                    registered = truth;
                });
            if (registered)
            {
                goto checkLog;
            }

            w?.RegisterDependency(
                new Delayer());

            checkLog:
            registered = falsity;
            w?.Run<Log>(log =>
                {
                    registered = truth;
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
            registered = falsity;
            var ln = DependencyNames.Lotter;
            w?.Run<Lotter>(lotter =>
                {
                    registered = truth;
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

            registered = falsity;
            w?.Run<Framework.Log.EntryReloader>(reloader =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.EntryReloader(w));
            }

            registered = falsity;
            w?.Run<Framework.Log.EntryConverter>(converter =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.EntryConverter(w));
            }

            registered = falsity;
            w?.Run<Framework.Log.FilterChecker>(converter =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.FilterChecker(w));
            }

            registered = falsity;
            w?.Run<Framework.Log.DateAndFilterResetter>(resetter =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.DateAndFilterResetter(w));
            }

            registered = falsity;
            w?.Run<Framework.Log.TimeProvider>(
                provider =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.TimeProvider());
            }

            registered = falsity;
            w?.Run<Framework.Log.SetupHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.SetupHandler(w));
            }

            registered = falsity;
            w?.Run<Framework.Log.StartHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.StartHandler(w));
            }

            registered = falsity;
            w?.Run<Framework.Log.AddKeyTappedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Framework.Log.AddKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<AccessLevelChangedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new AccessLevelChangedHandler(w));
            }

            registered = falsity;
            w?.Run<DateRangeChangedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new DateRangeChangedHandler(w));
            }

            registered = falsity;
            w?.Run<FilterTextChangedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new FilterTextChangedHandler(w));
            }

            registered = falsity;
            w?.Run<StatisticsKeyTappedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new StatisticsKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<ClearKeyTappedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new ClearKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<EntryWrittenHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new EntryWrittenHandler(w));
            }

            registered = falsity;
            w?.Run<Labels>(labels =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new Labels());
            }

            registered = falsity;
            w?.Run<LabelApplier>(applier =>
            {
                registered = truth;
            });

            if (!registered)
            {
                w?.RegisterDependency(
                    new LabelApplier(w));
            }

            registered = falsity;
            w?.Run<PreviousWeekKeyTappedHandler>(
                handler =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new PreviousWeekKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<CurrentWeekKeyTappedHandler>(
                handler =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new CurrentWeekKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<NextWeekKeyTappedHandler>(
                handler =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new NextWeekKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<NewestKeyTappedHandler>(
                applier =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new NewestKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<OldestKeyTappedHandler>(
                applier =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new OldestKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<KeyPresser>(
                presser =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new GeneralKeyPresser());
            }

            registered = falsity;
            w?.Run<DownKeyTappedHandler>(
                handler =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new DownKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<UpKeyTappedHandler>(
                handler =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new UpKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<ResetContentKeyTappedHandler>(
                handler =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new ResetContentKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<ResetTypeKeyTappedHandler>(
                handler =>
                {
                    registered = truth;
                });
            if (!registered)
            {
                w?.RegisterDependency(
                    new ResetTypeKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<xofz.Framework.LogEditor.SetupHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.SetupHandler(w));
            }

            registered = falsity;
            w?.Run<xofz.Framework.LogEditor.TypeChangedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.TypeChangedHandler(w));
            }

            registered = falsity;
            w?.Run<xofz.Framework.LogEditor.AddKeyTappedHandler>(handler =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.AddKeyTappedHandler(w));
            }

            registered = falsity;
            w?.Run<xofz.Framework.LogEditor.Labels>(labels =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.Labels());
            }

            registered = falsity;
            w?.Run<xofz.Framework.LogEditor.LabelApplier>(applier =>
            {
                registered = truth;
            });
            if (!registered)
            {
                w?.RegisterDependency(
                    new xofz.Framework.LogEditor.LabelApplier(w));
            }

            if (se)
            {
                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.SetupHandler>(
                    handler =>
                    {
                        registered = truth;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            SetupHandler(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.StartHandler>(
                    handler =>
                    {
                        registered = truth;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.StartHandler(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.
                    ResetContentKeyTappedHandler>(handler =>
                {
                    registered = truth;
                });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            ResetContentKeyTappedHandler(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.ResetTypeKeyTappedHandler>(
                    handler =>
                    {
                        registered = truth;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            ResetTypeKeyTappedHandler(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.StatsDisplayer>(sd =>
                {
                    registered = truth;
                });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.StatsDisplayer(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.FilterSetter>(fs =>
                {
                    registered = truth;
                });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.FilterSetter(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.OverallKeyTappedHandler>(
                    handler =>
                    {
                        registered = truth;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            OverallKeyTappedHandler(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.RangeKeyTappedHandler>(
                    handler =>
                    {
                        registered = truth;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            RangeKeyTappedHandler(w));
                }


                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.DateResetter>(
                    handler =>
                    {
                        registered = truth;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            DateResetter(w));
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.Labels>(
                    labels =>
                    {
                        registered = truth;
                    });
                if (!registered)
                {
                    w?.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            Labels());
                }

                registered = falsity;
                w?.Run<xofz.Framework.LogStatistics.LabelApplier>(
                    applier =>
                    {
                        registered = truth;
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
        protected const bool
            truth = true,
            falsity = false;
    }
}
