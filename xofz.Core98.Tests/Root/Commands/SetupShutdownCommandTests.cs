namespace xofz.Tests.Root.Commands
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Shutdown;
    using xofz.Root.Commands;
    using xofz.UI;
    using Xunit;

    public class SetupShutdownCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.cleanupUi = A.Fake<Ui>();
                this.cleanup = A.Fake<Do>();
                this.web = A.Fake<MethodWeb>();
                this.command = new SetupShutdownCommand(
                    this.cleanupUi,
                    this.cleanup,
                    this.web);
            }

            protected readonly Ui cleanupUi;
            protected readonly Do cleanup;
            protected readonly MethodWeb web;
            protected readonly SetupShutdownCommand command;
        }

        public class When_Execute_is_called : Context
        {
            [Fact]
            public void Registers_the_cleanupUi()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        this.cleanupUi,
                        UiNames.Cleanup))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_cleanup()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        this.cleanup,
                        MethodNames.Cleanup))
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
            public void Registers_a_ProcessKiller()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<ProcessKiller>.Ignored,
                        null))
                    .MustHaveHappened();
            }
        }
    }
}
