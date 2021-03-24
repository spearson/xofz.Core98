namespace xofz.Tests.Framework
{
    using xofz.Framework;
    using Xunit;

    public class ShufflingManagerTests
    {
        public class Context
        {
            protected Context()
            {
                this.manager1 = new ShufflingManager();
                this.manager2 = new ShufflingManager();
            }

            protected readonly ShufflingManager
                manager1,
                manager2;
        }

        public class When_CompareTo_is_called : Context
        {
            [Fact]
            public void Randomizes_the_return_value()
            {
                var m1 = this.manager1;
                var m2 = this.manager2;
                const byte zero = 0;
                byte
                    tryM1Count = 0,
                    tryM2Count = 0,
                    totalTries = 0xFF;

                for (var i = 0; i < totalTries; ++i)
                {
                    if (tryM1Count > zero && tryM2Count > zero)
                    {
                        break;
                    }

                    var comparedValue = m1?.CompareTo(m2);
                    if (comparedValue > zero)
                    {
                        ++tryM1Count;
                        continue;
                    }

                    if (comparedValue < zero)
                    {
                        ++tryM2Count;
                    }
                }

                Assert.True(
                    tryM1Count > zero);
                Assert.True(
                    tryM2Count > zero);
            }
        }
    }
}
