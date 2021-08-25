namespace xofz.Tests
{
    using Xunit;

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
                    StringHelpers.ToEnum<TestEnum>(
                        tester.ToString()));
            }

            protected enum TestEnum
            {
                Zero,
                One,
                Two
            }
        }
    }
}
