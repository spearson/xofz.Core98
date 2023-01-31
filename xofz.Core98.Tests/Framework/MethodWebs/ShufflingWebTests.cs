namespace xofz.Tests.Framework
{
    using Ploeh.AutoFixture;
    using xofz.Framework.MethodWebs;
    using Xunit;

    public class ShufflingWebTests
    {
        public class Context
        {
            protected Context()
            {
                this.web1 = new ShufflingWeb();
                this.web2 = new ShufflingWeb();
                this.fixture = new Fixture();
            }

            protected readonly ShufflingWeb web1;
            protected readonly ShufflingWeb web2;
            protected readonly Fixture fixture;
        }

        public class When_CompareTo_is_called : Context
        {
            [Fact]
            public void Randomizes_the_return_value()
            {
                var w1 = this.web1;
                var w2 = this.web2;
                const byte zero = 0;
                byte 
                    tryW1Count = 0,
                    tryW2Count = 0,
                    totalTries = 0xFF;

                for (var i = 0; i < totalTries; ++i)
                {
                    if (tryW1Count > zero && tryW2Count > zero)
                    {
                        break;
                    }

                    var comparedValue = w1?.CompareTo(w2);
                    if (comparedValue > zero)
                    {
                        ++tryW1Count;
                        continue;
                    }

                    if (comparedValue < zero)
                    {
                        ++tryW2Count;
                    }
                }

                Assert.True(
                    tryW1Count > zero);
                Assert.True(
                    tryW2Count > zero);
            }

            public class When_generic_Shuffle_is_called : Context
            {
                [Fact]
                public void Shuffles_correctly()
                {
                    var l = this.fixture.Create<long>();
                    var toShuffle = new[]
                    {
                        new object(),
                        new object(),
                        l,
                        new object()
                    };

                    foreach (var item in toShuffle)
                    {
                        this.web1.RegisterDependency(
                            item);
                    }

                    var shufflee = this.web1.Shuffle<long>();

                    Assert.Equal(
                        l,
                        shufflee);

                    var shufflee2 = this.web1.Shuffle<object>();

                    Assert.Contains(
                        shufflee2,
                        toShuffle);
                }
            }

            public class When_Shuffle_is_called : Context
            {
                [Fact]
                public void Shuffles_correctly()
                {
                    var s = this.fixture.Create<short>();
                    var toShuffle = new[]
                    {
                        s,
                        new object(),
                        new object(),
                        new object()
                    };

                    foreach (var item in toShuffle)
                    {
                        this.web1.RegisterDependency(
                            item);
                    }

                    var shufflee = this.web1.Shuffle();

                    Assert.Contains(
                        shufflee,
                        toShuffle);
                }
            }
        }
    }
}
