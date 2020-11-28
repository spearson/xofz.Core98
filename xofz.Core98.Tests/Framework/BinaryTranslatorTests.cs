namespace xofz.Tests.Framework
{
    using System.Collections.Generic;
    using System.Text;
    using FakeItEasy;
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
                this.bits = A.Fake<IEnumerable<bool>>();
                var bll = new LinkedList<bool>();
                bll.AddLast(true);
                bll.AddLast(true);
                bll.AddLast(false);
                bll.AddLast(true);
                bll.AddLast(false);
                bll.AddLast(true);
                bll.AddLast(false);
                bll.AddLast(true);
                bll.AddLast(true);
                bll.AddLast(false);
                bll.AddLast(false);
                bll.AddLast(false);
                bll.AddLast(false);
                bll.AddLast(false);
                bll.AddLast(false);
                bll.AddLast(false);

                A
                    .CallTo(() => this.bits.GetEnumerator())
                    .Returns(bll.GetEnumerator());
            }

            protected readonly BinaryTranslator translator;
            protected readonly Fixture fixture;
            protected readonly IEnumerable<bool> bits;
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

        public class When_GetBits_is_called : Context
        {
            [Fact]
            public void Returns_the_bits_of_the_bytes()
            {
                byte b = 0b11001010;

                var bits = EnumerableHelpers.ToArray(
                    this.translator.GetBits(
                    new[]
                    {
                        b
                    }));

                Assert.True(
                    bits[0]);
                Assert.True(
                    bits[1]);
                Assert.False(
                    bits[2]);
                Assert.False(
                    bits[3]);
                Assert.True(
                    bits[4]);
                Assert.False(
                    bits[5]);
                Assert.True(
                    bits[6]);
                Assert.False(
                    bits[7]);
            }
        }

        public class When_GetBytes_is_called : Context
        {
            [Fact]
            public void Calls_bits_GetEnumerator()
            {
                var bytes = this.translator.GetBytes(
                    this.bits);

                A
                    .CallTo(() => this.bits.GetEnumerator());
            }
        }
    }
}
