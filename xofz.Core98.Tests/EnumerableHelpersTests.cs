namespace xofz.Tests
{
    using System.Collections.Generic;
    using Ploeh.AutoFixture;
    using Xunit;
    using Xunit.Abstractions;
    using EH = xofz.EnumerableHelpers;

    public class EnumerableHelpersTests
    {
        public class Context
        {
            protected Context()
            {
                var f = new Fixture();
                this.fixture = f;
                this.manifestObject = () => f.Create<object>();
            }

            protected readonly Fixture fixture;
            protected readonly Gen<object> manifestObject;
        }

        public class When_Empty_is_called : Context
        {
            public When_Empty_is_called(
                ITestOutputHelper outputter)
            {
                this.outputter = outputter;
            }

            [Fact]
            public void Not_null()
            {
                Assert.NotNull(
                    EH.Empty<object>());
            }

            [Fact]
            public void Type_test()
            {
                var empty = EH.Empty<object>();

                this.outputter.WriteLine(
                    empty.GetType().ToString());
            }

            protected readonly ITestOutputHelper outputter;
        }

        public class When_ToArray_is_called : Context
        {
            [Fact]
            public void Not_null()
            {
                Assert.NotNull(
                    EH.ToArray(
                        (IEnumerable<object>)null));
                Assert.NotNull(
                    EH.ToArray(
                        (Lot<object>)null));
            }

            [Fact]
            public void Items_in_original_collection_same_with_array()
            {
                var mf = this.manifestObject;
                var sourceList = new List<object>
                {
                    mf(),
                    mf(),
                    mf(),
                    mf(),
                    mf()
                };

                var endArray = EH.ToArray(
                    sourceList);
                var endArrayLength = endArray.Length;
                var sourceListCount = sourceList.Count;
                Assert.Equal(
                    sourceListCount, 
                    endArrayLength);
                for (
                    var indexer = zero; 
                    indexer < endArrayLength && indexer < sourceListCount;
                    ++indexer)
                {
                    Assert.Same(
                        sourceList[indexer], 
                        endArray[indexer]);
                }
            }
        }

        public class When_ElementAt_is_called : Context
        {
            [Fact]
            public void Returns_default_if_source_null()
            {
                IEnumerable<object> source = null;

                Assert.Equal(
                    default,
                    EH.ElementAt(
                        source,
                        zero));
            }

            [Fact]
            public void Returns_default_if_index_negative()
            {
                IEnumerable<object> source = null;

                Assert.Equal(
                    default,
                    EH.ElementAt(
                        source,
                        -one));
            }

            [Fact]
            public void Returns_default_if_index_out_of_range()
            {
                IEnumerable<object> source = EH.Empty<object>();

                Assert.Equal(
                    default,
                    EH.ElementAt(
                        source,
                        -one));
            }

            [Fact]
            public void Otherwise_returns_the_magic_element()
            {
                var magicElement = this.fixture.Create<object>();
                IEnumerable<object> source = new[]
                {
                    null,
                    magicElement
                };

                Assert.Equal(
                    magicElement,
                    EH.ElementAt(
                        source,
                        one));
            }
        }

        public class When_Insert_is_called : Context
        {
            [Fact]
            public void Inserts_at_correct_index()
            {
                var f = this.fixture;
                var itemToInsert = f.Create<long>();
                Gen<long> manifestLong = () => f.Create<long>();
                var longArray = new long[]
                {
                    manifestLong(),
                    manifestLong(),
                    manifestLong(),
                };

                Assert.Equal(
                    EH.Prepend(
                        longArray,
                        itemToInsert),
                    EH.Insert(
                        longArray,
                        itemToInsert,
                        zero));

                const byte three = 3;
                Assert.Equal(
                    EH.Append(
                        longArray,
                        itemToInsert),
                    EH.Insert(
                        longArray,
                        itemToInsert,
                        three));

                const byte two = 2;
                Assert.Equal(
                        new[]
                        {
                            longArray[zero],
                            itemToInsert,
                            longArray[one],
                            longArray[two]
                        },
                    EH.Insert(
                        longArray,
                        itemToInsert,
                        one));
            }
        }

        protected const byte zero = 0;
        protected const byte one = 1;
    }
}
