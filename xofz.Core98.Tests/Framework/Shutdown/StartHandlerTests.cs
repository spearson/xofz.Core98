namespace xofz.Tests.Framework.Shutdown
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Shutdown;
    using xofz.UI;
    using Xunit;

    public class StartHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new StartHandler(
                    this.web);

                this.cleanup = A.Fake<Do>();
                this.cleanupUi = A.Fake<Ui>();
                this.uiRW = new UiReaderWriter();
                this.killer = A.Fake<ProcessKiller>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.killer);
            }

            protected readonly MethodWeb web;
            protected readonly StartHandler handler;
            protected readonly Do cleanup;
            protected readonly Ui cleanupUi;
            protected readonly UiReaderWriter uiRW;
            protected readonly ProcessKiller killer;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void
                If_cleanupUi_and_cleanup_in_web_invokes_cleanup_on_cleanupUi_thread()
            {
                var w = this.web;
                w.RegisterDependency(
                    this.cleanup,
                    MethodNames.Cleanup);
                w.RegisterDependency(
                    this.cleanupUi,
                    UiNames.Cleanup);

                this.handler.Handle();

                A
                    .CallTo(() => this.cleanup.Invoke())
                    .MustHaveHappened();
            }

            [Fact]
            public void If_cleanup_in_web_invokes_it()
            {
                var w = this.web;
                w.RegisterDependency(
                    this.cleanup,
                    MethodNames.Cleanup);

                this.handler.Handle();

                A
                    .CallTo(() => this.cleanup.Invoke())
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_killer_Kill()
            {
                var w = this.web;

                this.handler.Handle();

                A
                    .CallTo(() => this.killer.Kill())
                    .MustHaveHappened();
            }
        }
    }
}
