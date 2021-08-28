namespace xofz.Tests.Framework.Factories
{
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Factories;
    using xofz.Presentation;
    using Xunit;

    public class NameableFactoryTests
    {
        public class Context
        {
            protected Context()
            {
                this.nf = new NameableFactory();
                this.fixture = new Fixture();
            }

            protected readonly Factory nf;
            protected readonly Fixture fixture;
        }

        public class When_Create_is_called
            : Context
        {
            [Fact]
            public void Does_not_throw_on_null_argument()
            {
                this.nf.Create<object>(null);
            }

            [Fact]
            public void Sets_Name_of_NamedPresenter()
            {
                var nameToSet = this.fixture.Create<string>();
                var np = this.nf.Create<NamedPresenter>(
                    null,
                    null,
                    nameToSet);

                Assert.NotNull(
                    nameToSet);
                Assert.NotNull(
                    np);
                Assert.Equal(
                    nameToSet, 
                    np.Name);
            }

            [Fact]
            public void Sets_Name_of_TestNameable()
            {
                var nameToSet = this.fixture.Create<string>();
                Nameable tn = this.nf.Create<TestNameable>(
                    nameToSet);

                Assert.NotNull(
                    nameToSet);
                Assert.Equal(
                    nameToSet, 
                    tn.Name);
            }
        }

        public class TestNameable 
            : Nameable
        {
            public virtual string Name { get; set; }
        }
    }
}
