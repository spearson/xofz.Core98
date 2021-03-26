namespace xofz.Tests
{
    using Xunit;

    public class ShufflingObjectTests
    {
        public class Context
        {
            protected Context()
            {
                this.object1 = new ShufflingObject();
                this.object2 = new ShufflingObject();
            }

            protected readonly ShufflingObject object1;
            protected readonly ShufflingObject object2;
        }

        public class When_CompareTo_is_called : Context
        {
            [Fact]
            public void Randomizes_the_return_value()
            {
                var o1 = this.object1;
                var o2 = this.object2;
                const byte zero = 0;
                byte
                    tryO1Count = 0,
                    tryO2Count = 0,
                    totalTries = 0xFF;

                for (var i = 0; i < totalTries; ++i)
                {
                    if (tryO1Count > zero && tryO2Count > zero)
                    {
                        break;
                    }

                    var comparedValue = o1?.CompareTo(o2);
                    if (comparedValue > zero)
                    {
                        ++tryO1Count;
                        continue;
                    }

                    if (comparedValue < zero)
                    {
                        ++tryO2Count;
                    }
                }

                Assert.True(
                    tryO1Count > zero);
                Assert.True(
                    tryO2Count > zero);
            }
        }
    }
}
