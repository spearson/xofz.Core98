namespace xofz.Tests.Framework.Computation
{
    using System;
    using Ploeh.AutoFixture;
    using xofz.Framework.Computation;
    using Xunit;
    using Xunit.Abstractions;

    public class QuickSorterTests
    {
        public class Context
        {
            public Context(ITestOutputHelper outputter)
            {
                this.outputter = outputter;
                this.sorter = new QuickSorter();
                this.fixture = new Fixture();
            }

            protected readonly ITestOutputHelper outputter;
            protected readonly QuickSorter sorter;
            protected readonly Fixture fixture;
        }

        public class When_Sort_is_called : Context
        {
            [Fact]
            public void Sorts_the_input()
            {
                var f = this.fixture;
                var o = this.outputter;
                var testNumbers = new IComparable[0x1F];
                for (var i = 0; i < testNumbers.Length; ++i)
                {
                    testNumbers[i] = f.Create<double>();
                }

                o.WriteLine("Unsorted:");
                o.WriteLine(string.Join<IComparable>(
                    " ", 
                    testNumbers));
                o.WriteLine(string.Empty);

                this.sorter.Sort(testNumbers);

                o.WriteLine("Sorted");
                o.WriteLine(string.Join<IComparable>(
                    " ",
                    testNumbers));

                for (var i = 0; i < testNumbers.Length - 1; ++i)
                {
                    Assert.True(testNumbers[i]
                                    .CompareTo(testNumbers[i + 1]) <= 0);
                }
            }

            public When_Sort_is_called(
                ITestOutputHelper outputter) 
                : base(outputter)
            {
            }
        }
    }
}
