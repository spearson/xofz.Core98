namespace xofz.Tests.Framework
{
    using xofz.Framework;
    using Xunit;

    public class ShufflingWebTests
    {
        public class Context
        {
            protected Context()
            {
                this.web1 = new ShufflingWeb();
                this.web2 = new ShufflingWeb();
            }

            protected readonly ShufflingWeb web1;
            protected readonly ShufflingWeb web2;
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
        }
    }
}
