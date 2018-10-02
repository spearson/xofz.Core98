namespace xofz.Tests.Framework
{
    using System.Text;
    using xofz.Framework;
    using Ploeh.AutoFixture;
    using Xunit;

    public class BinaryTranslatorTests
    {
        public class Context
        {
            protected Context()
            {
                this.translator = new BinaryTranslator();
                this.fixture = new Fixture();
            }

            protected readonly BinaryTranslator translator;
            protected readonly Fixture fixture;
        }

        public class When_GetBits_for_a_string_is_called : Context
        {
            [Fact]
            public void Returns_empty_enumerable_if_string_is_null()
            {
                var noItems = true;
                foreach (var bit in this.translator.GetBits(
                    null,
                    Encoding.UTF8))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void Returns_empty_enumerable_if_encoding_is_null()
            {
                var noItems = true;
                foreach (var bit in this.translator.GetBits(
                    this.fixture.Create<string>(),
                    null))
                {
                    noItems = false;
                }

                Assert.True(noItems);
            }

            [Fact]
            public void Otherwise_returns_the_bits_of_the_string()
            {
                var s = this.fixture.Create<string>();
                var t = this.translator;
                var e = Encoding.UTF8;
                Assert.Equal(
                    s,
                    t.ReadString(
                        t.GetBits(s, e),
                        e));
                // just gotta check ReadNumber() too
                var n = this.fixture.Create<long>();
                Assert.Equal(
                    n,
                    t.ReadNumber(
                        t.GetBits(n)));
            }
        }
    }
}
