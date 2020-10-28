namespace xofz.Tests.Root.Commands
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.Root.Commands;
    using xofz.UI;
    using Xunit;

    public class SetupLoginCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<LoginUi>();
                this.web = A.Fake<MethodWeb>();
                this.fixture = new Fixture();
                this.duration = this.fixture.Create<TimeSpan>();
                this.command = new SetupLoginCommand(
                    this.ui,
                    this.web,
                    this.duration);
            }

            protected readonly LoginUi ui;
            protected readonly MethodWeb web;
            protected readonly SetupLoginCommand command;
            protected readonly Fixture fixture;
            protected readonly TimeSpan duration;
        }

        public class When_Execute_is_called : Context
        {
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
            public void Registers_a_SettingsHolder()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<SettingsHolder>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LatchHolder()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<LatchHolder>.Ignored,
                        DependencyNames.Latch))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_KeyboardLoader()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<KeyboardLoader>.Ignored,
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
            public void Registers_a_StopHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<StopHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_BackspaceKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<BackspaceKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_LoginKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<LoginKeyTappedHandler>.Ignored,
                        null))
                    .MustHaveHappened();
            }

            [Fact]
            public void Registers_a_TimerHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<TimerHandler>.Ignored,
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
            public void Registers_a_KeyboardKeyTappedHandler()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<KeyboardKeyTappedHandler>.Ignored,
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
            public void Registers_a_Labels()
            {
                this.command.Execute();

                A
                    .CallTo(() => this.web.RegisterDependency(
                        A<Labels>.Ignored,
                        null))
                    .MustHaveHappened();
            }
        }
    }
}
