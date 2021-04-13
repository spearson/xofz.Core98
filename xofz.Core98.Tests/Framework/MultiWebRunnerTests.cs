namespace xofz.Tests.Framework
{
    using FakeItEasy;
    using xofz.Framework;
    using Xunit;

    public class MultiWebRunnerTests
    {
        public class Context
        {
            protected Context()
            {
                this.runner = A.Fake<MultiWebRunner>();
            }

            protected readonly MultiWebRunner runner;
        }

        public class Api_Test : Context
        {
            [Fact]
            public void Go()
            {
                this.runner.RunWeb<
                    object>(
                    null);
                this.runner.RunWeb<
                    object,
                    object>(
                    null);
                this.runner.RunWeb<
                    object,
                    object,
                    object>(
                    null);
                this.runner.RunWeb<
                    object,
                    object,
                    object,
                    object>(
                    null);
                this.runner.RunWeb<
                    object,
                    object,
                    object,
                    object,
                    object>(
                    null);
                this.runner.RunWeb<
                    object,
                    object,
                    object,
                    object,
                    object,
                    object>(
                    null);
                this.runner.RunWeb<
                    object,
                    object,
                    object,
                    object,
                    object,
                    object,
                    object>(
                    null);
                this.runner.RunWeb<
                    object,
                    object,
                    object,
                    object,
                    object,
                    object,
                    object,
                    object>(
                    null);
            }
        }
    }
}
