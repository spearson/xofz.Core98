namespace xofz.Tests.Framework.Computation
{
    using System;
    using Ploeh.AutoFixture;
    using xofz.Framework.Computation;
    using Xunit;
    using Xunit.Abstractions;
    using EH = xofz.EnumerableHelpers;

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

            [Fact]
            public void Sorts_ShufflingObjects()
            {
                var o1 = new ShufflingObject();
                var o2 = new ShufflingObject();
                var o3 = new ShufflingObject();
                var o4 = new ShufflingObject();
                var o5 = new ShufflingObject();
                var o6 = new ShufflingObject();
                var o7 = new ShufflingObject();
                var o8 = new ShufflingObject();
                var o9 = new ShufflingObject();
                var o10 = new ShufflingObject();
                var o11 = new ShufflingObject();
                var o12 = new ShufflingObject();
                var array = new[] { o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12 };
                this.sorter.Sort(array);
                o1.O = 1;
                o2.O = 2;
                o3.O = 3;
                o4.O = 4;
                o5.O = 5;
                o6.O = 6;
                o7.O = 7;
                o8.O = 8;
                o9.O = 9;
                o10.O = 10;
                o11.O = 11;
                o12.O = 12;
                foreach (var item in array)
                {
                    this.outputter.WriteLine(item.O.ToString());
                }
            }

            [Fact]
            public void Sorts_with_nulls()
            {
                int? i1 = 4;
                int? i2 = 6;
                int? i3 = null;
                int? i4 = null;
                int? i5 = 1;
                var arr = new IComparable[] { i1, i2, i3, i4, i5 };

                this.sorter.Sort(arr);

                Assert.Contains(
                    null,
                    arr);

                var indexOf1 = EH.IndexOf(
                    arr,
                    1);
                var indexOf4 = EH.IndexOf(
                    arr,
                    4);
                var indexOf6 = EH.IndexOf(
                    arr,
                    6);
                Assert.True(indexOf1 < indexOf4);
                Assert.True(indexOf4 < indexOf6);
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

            [Fact]
            public void Sorts_with_nulls()
            {
                var o1 = new ShufflingObject();
                var o2 = new ShufflingObject();
                var o3 = new ShufflingObject();
                var o4 = new ShufflingObject();
                ShufflingObject o5 = null;
                ShufflingObject o6 = null;
                var arr = new[] { o1, o2, o3, o4, o5, o6 };

                this.sorter.Sort(arr);

                Assert.Equal(
                    2,
                    EH.Count(
                        arr,
                        o => o is null));
            }
        }

        public class When_generic_SortV2_is_called : Context
        {
            public When_generic_SortV2_is_called(
                ITestOutputHelper outputter)
                : base(outputter)
            {
            }

            [Fact]
            public void Sorts_ShufflingObjects()
            {
                var o1 = new ShufflingObject();
                var o2 = new ShufflingObject();
                var o3 = new ShufflingObject();
                var o4 = new ShufflingObject();
                var o5 = new ShufflingObject();
                var o6 = new ShufflingObject();
                var o7 = new ShufflingObject();
                var o8 = new ShufflingObject();
                var o9 = new ShufflingObject();
                var o10 = new ShufflingObject();
                var o11 = new ShufflingObject();
                var o12 = new ShufflingObject();
                var ll = IndexedLinkedList<ShufflingObject>.CreateIndexed(new[]
                    { o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12 });
                this.sorter.SortV2(ll);
                o1.O = 1;
                o2.O = 2;
                o3.O = 3;
                o4.O = 4;
                o5.O = 5;
                o6.O = 6;
                o7.O = 7;
                o8.O = 8;
                o9.O = 9;
                o10.O = 10;
                o11.O = 11;
                o12.O = 12;
                foreach (var item in ll)
                {
                    this.outputter.WriteLine(item.O.ToString());
                }
            }

            [Fact]
            public void Sorts_the_input()
            {
                var f = this.fixture;
                var o = this.outputter;
                var testNumbers = new IndexedLinkedList<IComparable>();
                long i;
                for (i = 0; i < 0x20; ++i)
                {
                    testNumbers.AddTail(
                        f.Create<double>());
                }

                testNumbers[i - 14] = double.NaN;
                testNumbers[i - 9] = testNumbers[i - 1];

                o.WriteLine("Unsorted:");
                o.WriteLine(string.Join(
                    " ",
                    testNumbers));
                o.WriteLine(string.Empty);

                this.sorter.SortV2(testNumbers);

                o.WriteLine("Sorted");
                o.WriteLine(string.Join(
                    " ",
                    testNumbers));

                for (i = 0; i < testNumbers.Count - 1; ++i)
                {
                    Assert.True(testNumbers[i]
                        .CompareTo(testNumbers[i + 1]) <= 0);
                }
            }

            [Fact]
            public void Sorts_with_nulls()
            {
                var o1 = new ShufflingObject();
                var o2 = new ShufflingObject();
                var o3 = new ShufflingObject();
                var o4 = new ShufflingObject();
                ShufflingObject o5 = null;
                ShufflingObject o6 = null;
                var ll = IndexedLinkedList<ShufflingObject>.CreateIndexed(
                    new[] { o1, o2, o3, o4, o5, o6 });

                this.sorter.SortV2(ll);

                Assert.Equal(
                    2,
                    EH.Count(
                        ll,
                        o => o is null));
            }
        }

        public class When_generic_SortV3_is_called : Context
        {
            public When_generic_SortV3_is_called(
                ITestOutputHelper outputter) : base(outputter)
            {
            }
        }
    }
}
