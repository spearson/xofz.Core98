namespace xofz.Tests.Framework
{
    using FakeItEasy;
    using xofz.Framework;
    using Xunit;

    public class MethodRunnerTests
    {
        public class Context
        {
            protected Context()
            {
                this.runner = A.Fake<MethodRunner>();
            }

            protected readonly MethodRunner runner;
        }

        public class Api_Test : Context
        {
            [Fact]
            public void Go()
            {
                this.runner.Run<
                    object>();
                this.runner.Run<
                    object,
                    object>();
                this.runner.Run<
                    object,
                    object,
                    object>();
                this.runner.Run<
                    object,
                    object,
                    object,
                    object>();
            }
        }
    }
}
