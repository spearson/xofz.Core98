namespace xofz.Tests
{
    using System.Collections.Generic;
    using Ploeh.AutoFixture;
    using Xunit;
    using EH = xofz.EnumerableHelpers;

    public class EnumerableHelpersTests
    {
        public class Context
        {
            protected Context()
            {
                this.fixture = new Fixture();
            }

            protected readonly Fixture fixture;
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
            public virtual void Items_in_original_collection_same_with_array()
            {
                var f = this.fixture;
                var items = new List<object>
                {
                    f.Create<object>(),
                    f.Create<object>(),
                    f.Create<object>(),
                    f.Create<object>(),
                    f.Create<object>()
                };

                var array = EH.ToArray(
                    items);
                Assert.Equal(
                    items.Count, 
                    array.Length);
                for (var i = 0; i < array.Length && i < items.Count; ++i)
                {
                    Assert.Same(
                        items[i], 
                        array[i]);
                }
            }
        }

        public class When_ElementAt_is_called : Context
        {
            public When_ElementAt_is_called()
            {
                this.source = new LinkedList<long>();

                var s = this.source;
                s.Add(
                    this.fixture.Create<long>());
                s.Add(
                    this.fixture.Create<long>());
                s.Add(
                    this.fixture.Create<long>());
                s.Add(
                    this.fixture.Create<long>());
                s.Add(
                    this.fixture.Create<long>());
            }

            [Fact]
            public void Returns_default_if_source_null()
            {
                this.source = null;

                Assert.Equal(
                    default,
                    EH.ElementAt(
                        this.source,
                        0));
            }

            [Fact]
            public void Returns_default_if_index_negative()
            {
                Assert.Equal(
                    default,
                    EH.ElementAt(
                        this.source,
                        -1));
            }

            [Fact]
            public void Returns_default_if_index_out_of_range()
            {
                Assert.Equal(
                    default,
                    EH.ElementAt(
                        this.source,
                        -1));
            }

            [Fact]
            public void Otherwise_returns_the_magic_element()
            {
                var bingo = this.fixture.Create<long>();
                this.source.Add(
                    bingo);

                Assert.Equal(
                    bingo,
                    EH.ElementAt(
                        this.source,
                        this.source.Count - 1));
            }

            protected ICollection<long> source;
        }
    }
}
