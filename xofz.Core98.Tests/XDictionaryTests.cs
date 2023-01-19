namespace xofz.Tests
{
    using System.Collections.Generic;
    using Ploeh.AutoFixture;
    using Xunit;

    public class XDictionaryTests
    {
        [Fact]
        public void Go()
        {
            var d = new XDictionary<string, string>();
            var f = new Fixture();
            var key1 = f.Create<string>();
            var key2 = f.Create<string>();
            var key3 = f.Create<string>();
            var key4 = f.Create<string>();
            var key5 = f.Create<string>();
            var key6 = f.Create<string>();
            var value1 = f.Create<string>();
            var value2 = f.Create<string>();
            var value3 = f.Create<string>();
            var value4 = f.Create<string>();
            var value5 = f.Create<string>();
            var value6 = f.Create<string>();
            d.Add(null, @"blah");
            d.Add(null, @"ohyeah");

            Assert.Equal(
                @"ohyeah",
                d[null]);

            d.Add(key1, value1);
            d.Add(key2, value2);
            d.Add(key3, value3);
            d.Add(key4, value4);
            d.Add(key5, value5);
            d.Add(key6, value6);

            Assert.True(
                d.TryGetValue(
                    key1, out var value));
            Assert.Equal(
                d[key1], value);
            Assert.True(
                d.TryGetValue(
                    key2, out value));
            Assert.Equal(
                d[key2], value);
            Assert.True(
                d.TryGetValue(
                    key3, out value));
            Assert.Equal(
                d[key3], value);
            Assert.True(
                d.TryGetValue(
                    key4, out value));
            Assert.Equal(
                d[key4], value);
            Assert.True(
                d.TryGetValue(
                    key5, out value));
            Assert.Equal(
                d[key5], value);
            Assert.True(
                d.TryGetValue(
                    key6, out value));
            Assert.Equal(
                d[key6], value);

            Assert.True(
                d.ContainsKey(key1));
            Assert.True(
                d.ContainsKey(key2));
            Assert.True(
                d.ContainsKey(key3));
            Assert.True(
                d.ContainsKey(key4));
            Assert.True(
                d.ContainsKey(key5));
            Assert.True(
                d.ContainsKey(key6));

            Assert.True(
                d.Remove(key1));
            Assert.False(
                d.Remove(key1));
            Assert.False(
                d.ContainsKey(key1));
            Assert.Null(d[key1]);

            d.Add(key1, value1);
            var changedValue = f.Create<string>();
            d[key1] = changedValue;
            Assert.Equal(
                changedValue,
                d[key1]);

            var removedOnce = false;
            foreach (var kvp in d)
            {
                if (removedOnce)
                {
                    Assert.True(false);
                }

                Assert.True(
                    d.Remove(kvp));
                removedOnce = true;
            }
        }
    }
}
