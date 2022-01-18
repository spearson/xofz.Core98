namespace xofz.Tests.Root.Commands
{
    using System.Collections.Generic;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.Framework.Logging.Logs;
    using xofz.Root.Commands;
    using xofz.UI;
    using xofz.UI.Log;
    using xofz.UI.LogEditor;
    using xofz.UI.LogStatistics;
    using Xunit;

    public class SetupLogCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<LogUi>();
                this.shell = A.Fake<ShellUi>();
                this.settings = new SettingsHolder();
                this.web = new MethodWeb();
                this.statsUi = A.Fake<LogStatisticsUi>();
                this.editUi = A.Fake<LogEditorUi>();
                this.command = new SetupLogCommand(
                    this.ui,
                    this.shell,
                    this.editUi,
                    this.statsUi,
                    this.settings,
                    this.web);
                this.fixture = new Fixture();
                this.settings.LogLocation = this.fixture.Create<string>();
                this.settings.LogDependencyName = this.fixture.Create<string>();
            }

            protected readonly LogUi ui;
            protected readonly ShellUi shell;
            protected readonly SettingsHolder settings;
            protected readonly MethodWeb web;
            protected readonly LogStatisticsUi statsUi;
            protected readonly LogEditorUi editUi;
            protected readonly SetupLogCommand command;
            protected readonly Fixture fixture;
        }

        public class When_Execute_is_called : Context
        {
            [Fact]
            public void
                If_no_log_registered_and_secondary_location_null_registers_a_TextFileLog()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<TextFileLog>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void
                If_no_log_registered_and_secondary_location_nonnull_registers_an_EventLogLog()
            {
                this.settings.SecondaryLogLocation =
                    this.fixture.Create<string>();

                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<EventLogLog>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void If_no_lotter_registered_registers_one()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<Lotter>(
                        null,
                        DependencyNames.Lotter));
            }

            [Fact]
            public void Registers_settings_under_LogDependencyName()
            {
                this.command.Execute();

                Assert.Same(
                    this.settings,
                    this.web.Run<SettingsHolder>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void Registers_ICollection_LogEntry()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<ICollection<LogEntry>>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void Registers_a_FieldHolder()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<FieldHolder>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void If_stats_enabled_registers_a_LogStatistics()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<LogStatistics>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void If_stats_enabled_registers_statsUi()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<LogStatisticsUi>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void If_stats_enabled_registers_SettingsHolder()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogStatistics.SettingsHolder>(
                        null,
                        this.settings.LogDependencyName));
            }

            [Fact]
            public void Registers_an_EntryReloader()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<EntryReloader>());
            }

            [Fact]
            public void Registers_an_EntryConverter()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<EntryConverter>());
            }

            [Fact]
            public void Registers_a_FilterChecker()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<FilterChecker>());
            }

            [Fact]
            public void Registers_a_DateAndFilterResetter()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<DateAndFilterResetter>());
            }

            [Fact]
            public void Registers_a_TimeProvider()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<TimeProvider>());
            }

            [Fact]
            public void Registers_a_SetupHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<SetupHandler>());
            }

            [Fact]
            public void Registers_a_StartHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<StartHandler>());
            }

            [Fact]
            public void Registers_an_AddKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<AddKeyTappedHandler>());
            }

            [Fact]
            public void Registers_an_AccessLevelChangedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<AccessLevelChangedHandler>());
            }

            [Fact]
            public void Registers_a_DateRangeChangedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<DateRangeChangedHandler>());
            }

            [Fact]
            public void Registers_a_FilterTextChangedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<FilterTextChangedHandler>());
            }

            [Fact]
            public void Registers_a_StatisticsKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<StatisticsKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_ClearKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<ClearKeyTappedHandler>());
            }

            [Fact]
            public void Registers_an_EntryWrittenHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<EntryWrittenHandler>());
            }

            [Fact]
            public void Registers_a_Labels()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<Labels>());
            }

            [Fact]
            public void Registers_a_LabelApplier()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<LabelApplier>());
            }

            [Fact]
            public void Registers_a_PreviousWeekKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<PreviousWeekKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_CurrentWeekKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<CurrentWeekKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_NextWeekKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<NextWeekKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_KeyPresser()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<KeyPresser>());
            }

            [Fact]
            public void Registers_a_DownKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<DownKeyTappedHandler>());
            }

            [Fact]
            public void Registers_an_UpKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<UpKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_ResetContentKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<ResetContentKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_ResetTypeKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<ResetTypeKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_LogEditor_SetupHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogEditor.SetupHandler>());
            }

            [Fact]
            public void Registers_a_LogEditor_TypeChangedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.
                        Run<xofz.Framework.LogEditor.TypeChangedHandler>());
            }

            [Fact]
            public void Registers_a_LogEditor_AddKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.
                        Run<xofz.Framework.LogEditor.AddKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_LogEditor_Labels()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogEditor.Labels>());
            }

            [Fact]
            public void Registers_a_LogEditor_LabelApplier()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogEditor.LabelApplier>());
            }

            [Fact]
            public void Registers_a_LogStatistics_SetupHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogStatistics.SetupHandler>());
            }

            [Fact]
            public void Registers_a_LogStatistics_StartHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogStatistics.StartHandler>());
            }

            [Fact]
            public void Registers_a_LogStatistics_ResetContentKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.
                        Run<xofz.Framework.LogStatistics.
                            ResetContentKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_LogStatistics_ResetTypeKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.
                        Run<xofz.Framework.LogStatistics.
                            ResetTypeKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_LogStatistics_StatsDisplayer()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.
                        Run<xofz.Framework.LogStatistics.StatsDisplayer>());
            }

            [Fact]
            public void Registers_a_LogStatistics_FilterSetter()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogStatistics.FilterSetter>());
            }

            [Fact]
            public void Registers_an_OverallKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.
                        Run<xofz.Framework.LogStatistics.
                            OverallKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_LogStatistics_RangeKeyTappedHandler()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.
                        Run<xofz.Framework.LogStatistics.
                            RangeKeyTappedHandler>());
            }

            [Fact]
            public void Registers_a_LogStatistics_DateResetter()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogStatistics.DateResetter>());
            }

            [Fact]
            public void Registers_a_LogStatistics_Labels()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogStatistics.Labels>());
            }

            [Fact]
            public void Registers_a_LogStatistics_LabelApplier()
            {
                this.command.Execute();

                Assert.NotNull(
                    this.web.Run<xofz.Framework.LogStatistics.LabelApplier>());
            }
        }
    }
}
