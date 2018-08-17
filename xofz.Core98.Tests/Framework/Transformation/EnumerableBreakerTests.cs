namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableBreakerTests
    {
        public class Context
        {
            protected Context()
            {
                this.breaker = new EnumerableBreaker();
            }

            protected readonly EnumerableBreaker breaker;
        }

        public class When_AddBreak_is_called : Context
        {
            [Fact]
            public void Puts_a_break_when_the_condition_is_true()
            {
                var items = new[] { 5, 7, 8, 14, 1, 12, 888 };
                Func<int, bool> predicate = n => n == 14;
                var expectedCountWithBreak = 3;
                var currentCount = 0;
                foreach (var item in breaker.AddBreak(
                    items, predicate))
                {
                    ++currentCount;
                }

                Assert.Equal(expectedCountWithBreak, currentCount);
            }

            [Fact]
            public void Breaks_if_any_condition_is_true_for_multiple_conditions()
            {
                var items = new[]
                {
                    new object(),
                    new object(),
                    new object(),
                    null,
                    new object(),
                    (object)14
                };

                var expectedCountWithBreak = 3;
                var currentCount = 0;
                foreach (var item in breaker.AddBreak(
                    items,
                    o => o is int,
                    o => o == null))
                {
                    ++currentCount;
                }

                Assert.Equal(
                    expectedCountWithBreak,
                    currentCount);

                expectedCountWithBreak = 5;
                currentCount = 0;
                foreach(var item in breaker.AddBreak(
                    items,
                    o => o is int))
                {
                    ++currentCount;
                }

                Assert.Equal(
                    expectedCountWithBreak,
                    currentCount);
            }
        }
    }
}
