namespace xofz.Tests
{
    using System.Collections.Generic;
    using Ploeh.AutoFixture;
    using Xunit;

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
            public virtual void Items_in_original_collection_match_with_array()
            {
                var f = this.fixture;
                var items = new List<long>
                {
                    f.Create<long>(),
                    f.Create<long>(),
                    f.Create<long>(),
                    f.Create<long>(),
                    f.Create<long>()
                };

                var array = EnumerableHelpers.ToArray(
                    items);
                Assert.Equal(items.Count, array.Length);
                for (var i = 0; i < array.Length && i < items.Count; ++i)
                {
                    Assert.Equal(items[i], array[i]);
                }
            }
        }

    }
}
