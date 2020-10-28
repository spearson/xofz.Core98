namespace xofz.Tests.Framework.Log
{
    using System.Collections.Generic;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.Framework.Logging;
    using xofz.UI;
    using Xunit;

    public class ClearKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new ClearKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LogUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.settings = new SettingsHolder();
                this.messenger = A.Fake<Messenger>();
                this.labels = new Labels();
                this.uiRW = new UiReaderWriter();
                this.editor = A.Fake<LogEditor>();
                this.reloader = A.Fake<EntryReloader>();
                A
                    .CallTo(() => this.messenger.Question(
                        A<string>.Ignored))
                    .Returns(Response.Yes);

                var w = this.web;
                w.RegisterDependency(
                    this.settings,
                    this.name);
                w.RegisterDependency(
                    this.messenger);
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.editor,
                    this.name);
                w.RegisterDependency(
                    this.reloader);

            }

            protected readonly MethodWeb web;
            protected readonly ClearKeyTappedHandler handler;
            protected readonly LogUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly SettingsHolder settings;
            protected readonly Messenger messenger;
            protected readonly Labels labels;
            protected readonly UiReaderWriter uiRW;
            protected readonly LogEditor editor;
            protected readonly EntryReloader reloader;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void
                If_ComputeBackupLocation_null_asks_ClearNoBackup_question()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.messenger.Question(
                        this.labels.ClearNoBackupQuestion))
                    .MustHaveHappened();
            }

            [Fact]
            public void Otherwise_asks_ClearWithBackup_question()
            {
                this.settings.ComputeBackupLocation = () => string.Empty;

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.messenger.Question(
                        this.labels.ClearWithBackupQuestion))
                    .MustHaveHappened();
            }

            [Fact]
            public void
                If_response_yes_and_computeBackupLocation_nonnull_calls_clear_with_backup_location()
            {
                var l = string.Empty;
                this.settings.ComputeBackupLocation = () => l;

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.editor.Clear(l))
                    .MustHaveHappened();
            }

            [Fact]
            public void Adds_ClearedWithBackup_label_to_log()
            {
                var l = string.Empty;
                this.settings.ComputeBackupLocation = () => l;

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.editor.AddEntry(
                        DefaultEntryTypes.Information,
                        A<IEnumerable<string>>.That.Contains(
                            this.labels.ClearedWithBackup(l))))
                    .MustHaveHappened();
            }

            [Fact]
            public void Reloads_entries()
            {
                this.settings.ComputeBackupLocation = () => string.Empty;

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.reloader.Reload(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void
                If_response_yes_and_computeBackupLocation_null_calls_Clear()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.editor.Clear())
                    .MustHaveHappened();
            }

            [Fact]
            public void If_computeBackupLocation_null_reloads_entries()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.reloader.Reload(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Adds_ClearedNoBackup_label_to_log()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.editor.AddEntry(
                        DefaultEntryTypes.Information,
                        A<IEnumerable<string>>.That.Contains(
                            this.labels.ClearedNoBackup)))
                    .MustHaveHappened();
            }
    }
    }
}
