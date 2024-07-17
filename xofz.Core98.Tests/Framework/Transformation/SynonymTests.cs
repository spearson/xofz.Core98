namespace xofz.Tests.Framework.Transformation
{
    using Ploeh.AutoFixture;
    using xofz.Framework.Transformation;
    using Xunit;

    public class SynonymTests
    {
        public class Context
        {
            protected Context()
            {
                this.synonym = new Synonym();
                this.fixture = new Fixture();
            }

            protected readonly Synonym synonym;
            protected readonly Fixture fixture;
        }

        public class When_Nominate_is_called : Context
        {
            [Fact]
            public void Works()
            {
                var testLong = this.fixture.Create<long>();
                var testString = this.fixture.Create<string>();
                var testInt = this.fixture.Create<int>();
                var testOb = new object();
                var source = new[]
                {
                    testLong,
                    testString,
                    testInt,
                    testOb
                };

                Gen<object, int> factory = o =>
                {
                    switch (o)
                    {
                        case long l:
                            return (int)l;
                        case string aString:
                            return aString.Length;
                        case int i:
                            return i;
                        case object ob:
                            return ob.GetHashCode();
                    }

                    return default;
                };

                byte indexer = 0xFF;
                const byte zero = 0, one = 1, two = 2, three = 3;
                foreach (var n in this.synonym.Nominate(
                             source,
                             factory))
                {
                    ++indexer;
                    switch (indexer)
                    {
                        case zero:
                            Assert.Equal(
                                (int)testLong,
                                n);
                            break;
                        case one:
                            Assert.Equal(
                                testString.Length,
                                n);
                            break;
                        case two:
                            Assert.Equal(
                                testInt,
                                n);
                            break;
                        case three:
                            Assert.Equal(
                                testOb.GetHashCode(),
                                n);
                            break;
                    }
                }
            }
        }
    }
}
