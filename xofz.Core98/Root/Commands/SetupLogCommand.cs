﻿namespace xofz.Root.Commands
{
    using System.Collections.Generic;
    using xofz.Framework;
    using xofz.Framework.Log;
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
            SettingsHolder settings,
            MethodWeb web)
        {
            this.ui = ui;
            this.shell = shell;
            this.settings = settings;
            settings.StatisticsEnabled = false;
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
            this.settings = settings;
            settings.StatisticsEnabled = true;
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
            this.settings = settings;
            settings.StatisticsEnabled = false;
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
            this.settings = settings;
            settings.StatisticsEnabled = true;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();

            var w = this.web;
            var s = this.settings;
            var n = s.LogDependencyName;
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
                        this.editUi,
                        w)
                    {
                        Name = n
                    }
                    .Setup();
            }

            if (s.StatisticsEnabled)
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
            var s = this.settings;
            var ldn = s.LogDependencyName;
            var location = s.LogLocation;
            var se = s.StatisticsEnabled;
            var registered = false;
            w.Run<Log>(log =>
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
                w.RegisterDependency(
                    new TextFileLog(location),
                    ldn);
                goto checkLotter;
            }

            w.RegisterDependency(
                new EventLogLog(location, location2),
                ldn);

        checkLotter:
            registered = false;
            var ln = DependencyNames.Lotter;
            w.Run<Lotter>(lotter =>
                {
                    registered = true;
                },
                ln);
            if (!registered)
            {
                w.RegisterDependency(
                    new LinkedListLotter(),
                    ln);
            }

            w.RegisterDependency(s, ldn);
            w.RegisterDependency(new LinkedList<LogEntry>(), ldn);
            w.RegisterDependency(
                new Framework.Log.FieldHolder(),
                ldn);
            if (se)
            {
                w.RegisterDependency(
                    new LogStatistics(w)
                    {
                        LogDependencyName = ldn
                    },
                    ldn);
                w.RegisterDependency(
                    new Framework.LogStatistics.SettingsHolder(),
                    ldn);
            }

            registered = false;
            w.Run<Framework.Log.EntryReloader>(reloader =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Framework.Log.EntryReloader(w));
            }

            registered = false;
            w.Run<Framework.Log.EntryConverter>(converter =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Framework.Log.EntryConverter(w));
            }

            registered = false;
            w.Run<Framework.Log.FilterChecker>(converter =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Framework.Log.FilterChecker(w));
            }

            registered = false;
            w.Run<Framework.Log.DateAndFilterResetter>(resetter =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Framework.Log.DateAndFilterResetter(w));
            }

            registered = false;
            w.Run<Framework.Log.SetupHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Framework.Log.SetupHandler(w));
            }

            registered = false;
            w.Run<Framework.Log.StartHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Framework.Log.StartHandler(w));
            }

            registered = false;
            w.Run<Framework.Log.AddKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new Framework.Log.AddKeyTappedHandler(w));
            }

            registered = false;
            w.Run<AccessLevelChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new AccessLevelChangedHandler(w));
            }

            registered = false;
            w.Run<DateChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new DateChangedHandler(w));
            }

            registered = false;
            w.Run<FilterTextChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new FilterTextChangedHandler(w));
            }

            registered = false;
            w.Run<StatisticsKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new StatisticsKeyTappedHandler(w));
            }

            registered = false;
            w.Run<ClearKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new ClearKeyTappedHandler(w));
            }

            registered = false;
            w.Run<EntryWrittenHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new EntryWrittenHandler(w));
            }

            registered = false;
            w.Run<xofz.Framework.LogEditor.SetupHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new xofz.Framework.LogEditor.SetupHandler(w));
            }

            registered = false;
            w.Run<xofz.Framework.LogEditor.TypeChangedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new xofz.Framework.LogEditor.TypeChangedHandler(w));
            }

            registered = false;
            w.Run<xofz.Framework.LogEditor.AddKeyTappedHandler>(handler =>
            {
                registered = true;
            });
            if (!registered)
            {
                w.RegisterDependency(
                    new xofz.Framework.LogEditor.AddKeyTappedHandler(w));
            }

            if (se)
            {
                registered = false;
                w.Run<xofz.Framework.LogStatistics.SetupHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            SetupHandler(w));
                }

                registered = false;
                w.Run<xofz.Framework.LogStatistics.StartHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.StartHandler(w));
                }

                registered = false;
                w.Run<xofz.Framework.LogStatistics.
                    ResetContentKeyTappedHandler>(handler =>
                {
                    registered = true;
                });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            ResetContentKeyTappedHandler(w));
                }

                registered = false;
                w.Run<xofz.Framework.LogStatistics.ResetTypeKeyTappedHandler>(
                    handler => { registered = true; });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            ResetTypeKeyTappedHandler(w));
                }

                registered = false;
                w.Run<xofz.Framework.LogStatistics.StatsDisplayer>(sd =>
                {
                    registered = true;
                });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.StatsDisplayer(w));
                }

                registered = false;
                w.Run<xofz.Framework.LogStatistics.FilterSetter>(fs =>
                {
                    registered = true;
                });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.FilterSetter(w));
                }

                registered = false;
                w.Run<xofz.Framework.LogStatistics.OverallKeyTappedHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            OverallKeyTappedHandler(w));
                }

                registered = false;
                w.Run<xofz.Framework.LogStatistics.RangeKeyTappedHandler>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            RangeKeyTappedHandler(w));
                }


                registered = false;
                w.Run<xofz.Framework.LogStatistics.DateResetter>(
                    handler =>
                    {
                        registered = true;
                    });
                if (!registered)
                {
                    w.RegisterDependency(
                        new xofz.Framework.LogStatistics.
                            DateResetter(w));
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
