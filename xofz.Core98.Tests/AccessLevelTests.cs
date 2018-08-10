namespace xofz.Tests
{
    using Xunit;

    public class AccessLevelTests
    {
        [Fact]
        public void None_comparison_test()
        {
            Assert.True(AccessLevel.None < AccessLevel.Level1);
            Assert.True(AccessLevel.None < AccessLevel.Level2);
            Assert.True(AccessLevel.None < AccessLevel.Level3);
            Assert.True(AccessLevel.None < AccessLevel.Level4);
            Assert.True(AccessLevel.None < AccessLevel.Level5);
            Assert.True(AccessLevel.None < AccessLevel.Level6);
            Assert.True(AccessLevel.None < AccessLevel.Level7);
            Assert.True(AccessLevel.None < AccessLevel.Level8);
            Assert.True(AccessLevel.None < AccessLevel.Level9);
            Assert.True(AccessLevel.None < AccessLevel.Level10);
        }

        [Fact]
        public void One_comparison_test()
        {
            Assert.True(AccessLevel.Level1 > AccessLevel.None);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level2);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level3);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level4);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level5);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level6);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level7);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level8);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level1 < AccessLevel.Level10);
        }

        [Fact]
        public void Two_comparison_test()
        {
            Assert.True(AccessLevel.Level2 > AccessLevel.None);
            Assert.True(AccessLevel.Level2 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level3);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level4);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level5);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level6);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level7);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level8);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level2 < AccessLevel.Level10);
        }

        [Fact]
        public void Three_comparison_test()
        {
            Assert.True(AccessLevel.Level3 > AccessLevel.None);
            Assert.True(AccessLevel.Level3 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level3 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level3 < AccessLevel.Level4);
            Assert.True(AccessLevel.Level3 < AccessLevel.Level5);
            Assert.True(AccessLevel.Level3 < AccessLevel.Level6);
            Assert.True(AccessLevel.Level3 < AccessLevel.Level7);
            Assert.True(AccessLevel.Level3 < AccessLevel.Level8);
            Assert.True(AccessLevel.Level3 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level3 < AccessLevel.Level10);
        }

        [Fact]
        public void Four_comparison_test()
        {
            Assert.True(AccessLevel.Level4 > AccessLevel.None);
            Assert.True(AccessLevel.Level4 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level4 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level4 > AccessLevel.Level3);
            Assert.True(AccessLevel.Level4 < AccessLevel.Level5);
            Assert.True(AccessLevel.Level4 < AccessLevel.Level6);
            Assert.True(AccessLevel.Level4 < AccessLevel.Level7);
            Assert.True(AccessLevel.Level4 < AccessLevel.Level8);
            Assert.True(AccessLevel.Level4 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level4 < AccessLevel.Level10);
        }

        [Fact]
        public void Five_comparison_test()
        {
            Assert.True(AccessLevel.Level5 > AccessLevel.None);
            Assert.True(AccessLevel.Level5 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level5 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level5 > AccessLevel.Level3);
            Assert.True(AccessLevel.Level5 > AccessLevel.Level4);
            Assert.True(AccessLevel.Level5 < AccessLevel.Level6);
            Assert.True(AccessLevel.Level5 < AccessLevel.Level7);
            Assert.True(AccessLevel.Level5 < AccessLevel.Level8);
            Assert.True(AccessLevel.Level5 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level5 < AccessLevel.Level10);
        }

        [Fact]
        public void Six_comparison_test()
        {
            Assert.True(AccessLevel.Level6 > AccessLevel.None);
            Assert.True(AccessLevel.Level6 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level6 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level6 > AccessLevel.Level3);
            Assert.True(AccessLevel.Level6 > AccessLevel.Level4);
            Assert.True(AccessLevel.Level6 > AccessLevel.Level5);
            Assert.True(AccessLevel.Level6 < AccessLevel.Level7);
            Assert.True(AccessLevel.Level6 < AccessLevel.Level8);
            Assert.True(AccessLevel.Level6 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level6 < AccessLevel.Level10);
        }

        [Fact]
        public void Seven_comparison_test()
        {
            Assert.True(AccessLevel.Level7 > AccessLevel.None);
            Assert.True(AccessLevel.Level7 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level7 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level7 > AccessLevel.Level3);
            Assert.True(AccessLevel.Level7 > AccessLevel.Level4);
            Assert.True(AccessLevel.Level7 > AccessLevel.Level5);
            Assert.True(AccessLevel.Level7 > AccessLevel.Level6);
            Assert.True(AccessLevel.Level7 < AccessLevel.Level8);
            Assert.True(AccessLevel.Level7 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level7 < AccessLevel.Level10);
        }

        [Fact]
        public void Eight_comparison_test()
        {
            Assert.True(AccessLevel.Level8 > AccessLevel.None);
            Assert.True(AccessLevel.Level8 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level8 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level8 > AccessLevel.Level3);
            Assert.True(AccessLevel.Level8 > AccessLevel.Level4);
            Assert.True(AccessLevel.Level8 > AccessLevel.Level5);
            Assert.True(AccessLevel.Level8 > AccessLevel.Level6);
            Assert.True(AccessLevel.Level8 > AccessLevel.Level7);
            Assert.True(AccessLevel.Level8 < AccessLevel.Level9);
            Assert.True(AccessLevel.Level8 < AccessLevel.Level10);
        }

        [Fact]
        public void Nine_comparison_test()
        {
            Assert.True(AccessLevel.Level9 > AccessLevel.None);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level3);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level4);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level5);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level6);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level7);
            Assert.True(AccessLevel.Level9 > AccessLevel.Level8);
            Assert.True(AccessLevel.Level9 < AccessLevel.Level10);
        }

        [Fact]
        public void Ten_comparison_test()
        {
            Assert.True(AccessLevel.Level10 > AccessLevel.None);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level1);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level2);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level3);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level4);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level5);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level6);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level7);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level8);
            Assert.True(AccessLevel.Level10 > AccessLevel.Level9);
        }
    }
}
