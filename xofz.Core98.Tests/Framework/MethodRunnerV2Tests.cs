namespace xofz.Tests.Framework
{
    using FakeItEasy;
    using xofz.Framework;
    using Xunit;

    public class MethodRunnerV2Tests
    {
        public class Context
        {
            protected Context()
            {
                this.runner = A.Fake<MethodRunnerV2>();
            }

            protected readonly MethodRunnerV2 runner;
        }

        public class Api_Test : Context
        {
            [Fact]
            public void Go()
            {
                this.runner.Run<
                    object,
                    object,
                    object,
                    object,
                    object>();
                this.runner.Run<
                    object,
                    object,
                    object,
                    object,
                    object,
                    object>();
                this.runner.Run<
                    object,
                    object,
                    object,
                    object,
                    object,
                    object,
                    object>();
                this.runner.Run<
                    object,
                    object,
                    object,
                    object,
                    object,
                    object,
                    object,
                    object>();
            }
        }
    }
}
