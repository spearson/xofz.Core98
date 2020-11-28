namespace xofz.Tests.Framework
{
    using xofz.Framework.Factories;
    using Xunit;

    public class NameableFactoryTests
    {
        public class Context
        {
            protected Context()
            {
                this.f = new NameableFactory();
            }

            protected readonly NameableFactory f;
        }

        public class When_Create_is_called : Context
        {
            [Fact]
            public void Does_not_throw_on_null_argument()
            {
                this.f.Create<object>(null);
            }
        }
    }
}
