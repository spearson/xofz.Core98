namespace xofz.Tests.Root.Commands
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Access;
    using xofz.Root.Commands;
    using Xunit;

    public class SetupAccessCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.passwords = new PasswordHolder();
                this.web = A.Fake<MethodWeb>();
                this.command = new SetupAccessCommand(
                    this.passwords,
                    this.web);
            }

            protected readonly PasswordHolder passwords;
            protected readonly MethodWeb web;
            protected readonly SetupAccessCommand command;
        }

        public class When_Execute_is_called : Context
        {
            [Fact]
            public void Registers_passwords()
            {
                this.command.Execute();

                A
                    .CallTo(() =>
                        this.web.RegisterDependency(this.passwords, null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_timer()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<xofz.Framework.Timer>.Ignored,
                        DependencyNames.Timer))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_SecureStringToolSet()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<SecureStringToolSet>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_SettingsHolder()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<SettingsHolder>.Ignored, null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_TimeProvider()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<TimeProvider>.Ignored, null))
                    .MustHaveHappened();
            }
        }
    }
}
