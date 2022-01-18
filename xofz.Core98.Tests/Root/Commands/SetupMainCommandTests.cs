namespace xofz.Tests.Root.Commands
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Main;
    using xofz.Root.Commands;
    using xofz.UI.Main;
    using Xunit;

    public class SetupMainCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<MainUi>();
                this.web = A.Fake<MethodWeb>();
                this.settings = new SettingsHolder();
                this.command = new SetupMainCommand(
                    this.ui,
                    this.web,
                    this.settings);
            }

            protected readonly MainUi ui;
            protected readonly MethodWeb web;
            protected readonly SettingsHolder settings;
            protected readonly SetupMainCommand command;
        }

        public class When_Execute_is_called : Context
        {
            [Fact]
            public void Registers_settings()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        this.settings,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_ShutdownRequestedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<ShutdownRequestedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }
        }
    }
}
