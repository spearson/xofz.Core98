namespace xofz.Tests
{
    using Ploeh.AutoFixture;
    using Xunit;
    using SH = xofz.StringHelpers;

    public class StringHelpersTests
    {
        public class When_ToEnum_is_called
        {
            [Fact]
            public void Works_as_expected()
            {
                var tester = TestEnum.Zero;
                this.assert(tester);

                tester = TestEnum.One;
                this.assert(tester);

                tester = TestEnum.Two;
                this.assert(tester);
            }

            protected void assert(
                TestEnum tester)
            {
                Assert.Equal(
                    tester,
                    SH.ToEnum<TestEnum>(
                        tester.ToString()));
            }

            protected enum TestEnum
            {
                Zero,
                One,
                Two
            }
        }

        public class When_NullOrEmpty_is_called
        {
            [Fact]
            public void Returns_true_for_null()
            {
                Assert.True(
                    SH.NullOrEmpty(null));
            }

            [Fact]
            public void Returns_true_for_empty()
            {
                Assert.True(
                    SH.NullOrEmpty(string.Empty));
            }

            [Fact]
            public void Returns_false_for_whitespace()
            {
                Assert.False(
                    SH.NullOrEmpty(@" "));
            }

            [Fact]
            public void Returns_false_otherwise()
            {
                var f = new Fixture();
                Assert.False(
                    SH.NullOrEmpty(
                        f.Create<string>()));
            }
        }
    }
}
