namespace xofz.Tests.Framework.Transformation
{
    using xofz.Framework.Transformation;
    using Xunit;

    public class EnumerableConnectorTests
    {
        public class Context
        {
            protected Context()
            {
                this.connector = new EnumerableConnector();
            }

            protected readonly EnumerableConnector connector;
        }

        public class When_Connect_is_called : Context
        {
            [Fact]
            public void Returns_empty_collection_for_null_array()
            {
                var collection = this.connector.Connect<object>(null);

                Assert.Equal(0, collection.Count);
            }

            [Fact]
            public void Returns_empty_collection_for_empty_array()
            {
                var collection = this.connector.Connect(new object[0]);

                Assert.Equal(0, collection.Count);
            }

            [Fact]
            public void Connects_the_enumerables_together()
            {
                var collection = this.connector.Connect(
                    new[] { 5, 6, 7 },
                    new[] { 11, 500, 22 },
                    new[] { -15, 0, 12, 87 });
                Assert.Equal(10, collection.Count);
                Assert.Contains(5, collection);
                Assert.Contains(6, collection);
                Assert.Contains(7, collection);
                Assert.Contains(11, collection);
                Assert.Contains(500, collection);
                Assert.Contains(22, collection);
                Assert.Contains(-15, collection);
                Assert.Contains(0, collection);
                Assert.Contains(12, collection);
                Assert.Contains(87, collection);
            }
        }
    }
}
