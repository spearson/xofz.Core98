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
                byte numberOfTests = 0x20;
                beginTest:
                var testNumbers = new IComparable[0x1F];
                long i;
                for (i = 0; i < testNumbers.Length; ++i)
                {
                    testNumbers[i] = f.Create<double>();
                }

                testNumbers[i - 9] = testNumbers[i - 1];

                o.WriteLine("Unsorted:");
                o.WriteLine(string.Join<IComparable>(
                    " ", 
                    testNumbers));

                sorter.Sort(testNumbers);

                o.WriteLine("Sorted");
                o.WriteLine(string.Join<IComparable>(
                    " ",
                    testNumbers));
                o.WriteLine(string.Empty);

                for (i = 0; i < testNumbers.Length - 1; ++i)
                {
                    Assert.True(testNumbers[i]
                                    .CompareTo(testNumbers[i + 1]) <= 0);
                }

                --numberOfTests;
                if (numberOfTests > 0)
                {
                    goto beginTest;
                }
            }

            public When_Sort_is_called(
                ITestOutputHelper outputter) 
                : base(outputter)
            {
            }
        }

        public class When_Generic_Sort_is_called : Context
        {
            public When_Generic_Sort_is_called(
                ITestOutputHelper outputter)
                : base(outputter)
            {
            }

            [Fact]
            public void Sorts_the_input()
            {
                var f = this.fixture;
                var o = this.outputter;
                var testNumbers = new double[0x24];
                long i;
                for (i = 0; i < testNumbers.Length; ++i)
                {
                    testNumbers[i] = f.Create<double>();
                }

                testNumbers[i - 14] = double.NaN;
                testNumbers[i - 9] = testNumbers[i - 1];

                o.WriteLine("Unsorted:");
                o.WriteLine(string.Join(
                    " ",
                    testNumbers));
                o.WriteLine(string.Empty);

                this.sorter.Sort(testNumbers);

                o.WriteLine("Sorted");
                o.WriteLine(string.Join(
                    " ",
                    testNumbers));

                for (i = 0; i < testNumbers.Length - 1; ++i)
                {
                    Assert.True(testNumbers[i]
                                    .CompareTo(testNumbers[i + 1]) <= 0);
                }
            }
        }
    }
}
