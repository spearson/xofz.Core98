namespace xofz.Tests.Framework.Transformation
{
    using System.Collections.Generic;
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableSplicerTests
    {
        public class Context
        {
            protected Context()
            {
                this.splicer = new EnumerableSplicer();
            }

            protected readonly EnumerableSplicer splicer;
            
        }

        public class When_Splice_is_called : Context
        {

            [Fact]
            public void Splices_the_sources()
            {
                var arr1 = new[] {1, 4, 7};
                var arr2 = new[] {2, 5, 8};
                var arr3 = new[] {3, 6, 9};

                byte indexer1 = 0, indexer2 = 0;

                bool checkedAll = false;
                foreach (var item in this.splicer.Splice(
                    new[]
                    {
                        (IEnumerable<int>)arr1, arr2, arr3
                    }))
                {
                    switch (indexer1)
                    {
                        case 0:
                            switch (indexer2)
                            {
                                case 0:
                                    Assert.Equal(1, item);
                                    break;
                                case 1:
                                    Assert.Equal(2, item);
                                    break;
                                case 2:
                                    Assert.Equal(3, item);
                                    break;
                            }

                            break;
                        case 1:
                            switch (indexer2)
                            {
                                case 0:
                                    Assert.Equal(4, item);
                                    break;
                                case 1:
                                    Assert.Equal(5, item);
                                    break;
                                case 2:
                                    Assert.Equal(6, item);
                                    break;
                            }

                            break;
                        case 2:
                            switch (indexer2)
                            {
                                case 0:
                                    Assert.Equal(7, item);
                                    break;
                                case 1:
                                    Assert.Equal(8, item);
                                    break;
                                case 2:
                                    Assert.Equal(9, item);
                                    checkedAll = true;
                                    break;
                            }

                            break;
                    }

                    ++indexer2;
                    if (indexer2 == 3)
                    {
                        ++indexer1;
                        indexer2 = 0;
                    }
                }

                Assert.True(checkedAll);
            }
        }

        public class When_SpliceV2_is_called : Context
        {
            [Fact]
            public void Splices_the_sources()
            {
                var arr1 = new[] { 1, 4, 7 };
                var arr2 = new[] { 2, 5, 8 };
                var arr3 = new[] { 3, 6, 9 };

                byte indexer1 = 0, indexer2 = 0;

                bool checkedAll = false;
                foreach (var item in this.splicer.SpliceV2(
                    new[]
                    {
                        (IEnumerable<int>)arr1, arr2, arr3
                    }))
                {
                    switch (indexer1)
                    {
                        case 0:
                            switch (indexer2)
                            {
                                case 0:
                                    Assert.Equal(1, item);
                                    break;
                                case 1:
                                    Assert.Equal(2, item);
                                    break;
                                case 2:
                                    Assert.Equal(3, item);
                                    break;
                            }

                            break;
                        case 1:
                            switch (indexer2)
                            {
                                case 0:
                                    Assert.Equal(4, item);
                                    break;
                                case 1:
                                    Assert.Equal(5, item);
                                    break;
                                case 2:
                                    Assert.Equal(6, item);
                                    break;
                            }

                            break;
                        case 2:
                            switch (indexer2)
                            {
                                case 0:
                                    Assert.Equal(7, item);
                                    break;
                                case 1:
                                    Assert.Equal(8, item);
                                    break;
                                case 2:
                                    Assert.Equal(9, item);
                                    checkedAll = true;
                                    break;
                            }

                            break;
                    }

                    ++indexer2;
                    if (indexer2 == 3)
                    {
                        ++indexer1;
                        indexer2 = 0;
                    }
                }

                Assert.True(checkedAll);
            }
        }
    }
}
