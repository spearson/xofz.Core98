namespace xofz.Tests.Framework
{
    using xofz.Framework;
    using xofz.Framework.MethodWebManagers;
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
            protected const string webV3Name = nameof(webV3Name);
            
            protected class WebV3 : MethodWebV2
            {
            }
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

        public class When_generic_Shuffle_is_called : Context
        {
            public When_generic_Shuffle_is_called()
            {
                this.webV3 = new WebV3();
                var m = this.manager1;
                m.AddWeb(
                    new MethodWebV2());
                m.AddWeb(
                    this.webV3,
                    webV3Name);
            }

            [Fact]
            public void If_types_match_returns_the_web()
            {
                Assert.Same(
                    this.webV3,
                    this.manager1.Shuffle<WebV3>());
            }

            [Fact]
            public void Otherwise_returns_default()
            {
                var m = this.manager1;
                m.RemoveWeb(webV3Name);

                Assert.Same(
                    default(WebV3),
                    this.manager1.Shuffle<WebV3>());
            }

            protected readonly MethodWeb webV3;
        }

        public class When_Shuffle_is_called : Context
        {
            public When_Shuffle_is_called()
            {
                this.webV2 = new MethodWebV2();
                var m = this.manager1;
                m.AddWeb(
                    new MethodWeb());
                m.AddWeb(
                    this.webV2,
                    webV2Name);
            }

            [Fact]
            public void If_types_match_returns_the_web()
            {
                Assert.Same(
                    this.webV2,
                    this.manager1.Shuffle<MethodWebV2>());
            }

            [Fact]
            public void Otherwise_returns_default()
            {
                var m = this.manager1;
                m.RemoveWeb(webV2Name);

                Assert.Same(
                    default(MethodWebV2),
                    this.manager1.Shuffle<MethodWebV2>());
            }

            protected readonly MethodWeb webV2;
            protected const string webV2Name = nameof(webV2Name);
        }
    }
}
