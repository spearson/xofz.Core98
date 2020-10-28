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
                this.web = A.Fake<MethodWeb>();
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

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<TextFileLog>.Ignored,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void
                If_no_log_registered_and_secondary_location_nonnull_registers_an_EventLogLog()
            {
                this.settings.SecondaryLogLocation =
                    this.fixture.Create<string>();

                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<EventLogLog>.Ignored,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_no_lotter_registered_registers_one()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<Lotter>.Ignored,
                        DependencyNames.Lotter))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_settings_under_LogDependencyName()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        this.settings,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_ICollection_LogEntry()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<ICollection<LogEntry>>.Ignored,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_FieldHolder()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<FieldHolder>.Ignored,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_stats_enabled_registers_a_LogStatistics()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<LogStatistics>.Ignored,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_stats_enabled_registers_statsUi()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        this.statsUi,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void If_stats_enabled_registers_SettingsHolder()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.SettingsHolder>.Ignored,
                        this.settings.LogDependencyName))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_an_EntryReloader()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<EntryReloader>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_an_EntryConverter()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<EntryConverter>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_FilterChecker()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<FilterChecker>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_DateAndFilterResetter()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<DateAndFilterResetter>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_TimeProvider()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<TimeProvider>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_SetupHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<SetupHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_StartHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<StartHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_an_AddKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<AddKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_an_AccessLevelChangedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<AccessLevelChangedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_DateRangeChangedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<DateRangeChangedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_FilterTextChangedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<FilterTextChangedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_StatisticsKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<StatisticsKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_ClearKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<ClearKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_an_EntryWrittenHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<EntryWrittenHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_Labels()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<Labels>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LabelApplier()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<LabelApplier>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_PreviousWeekKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<PreviousWeekKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_CurrentWeekKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<CurrentWeekKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_NextWeekKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<NextWeekKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_KeyPresser()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<KeyPresser>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_DownKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<DownKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_an_UpKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<UpKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_ResetContentKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<ResetContentKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_ResetTypeKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<ResetTypeKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogEditor_SetupHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogEditor.SetupHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogEditor_TypeChangedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogEditor.TypeChangedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogEditor_AddKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogEditor.AddKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogEditor_Labels()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogEditor.Labels>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogEditor_LabelApplier()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogEditor.LabelApplier>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_SetupHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.SetupHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_StartHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.StartHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_ResetContentKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.ResetContentKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_ResetTypeKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.ResetTypeKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_StatsDisplayer()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.StatsDisplayer>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_FilterSetter()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.FilterSetter>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_an_OverallKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.OverallKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_RangeKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.RangeKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_DateResetter()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.DateResetter>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_Labels()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.Labels>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LogStatistics_LabelApplier()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.LogStatistics.LabelApplier>.Ignored,
                        null))
                    .MustHaveHappened();
            }
        }
    }
}
