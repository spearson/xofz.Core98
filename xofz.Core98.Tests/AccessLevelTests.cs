namespace xofz.Tests
{
    using Xunit;

    public class AccessLevelTests
    {
        protected const AccessLevel none = AccessLevel.None;
        protected const AccessLevel one = AccessLevel.Level1;
        protected const AccessLevel two = AccessLevel.Level2;
        protected const AccessLevel three = AccessLevel.Level3;
        protected const AccessLevel four = AccessLevel.Level4;
        protected const AccessLevel five = AccessLevel.Level5;
        protected const AccessLevel six = AccessLevel.Level6;
        protected const AccessLevel seven = AccessLevel.Level7;
        protected const AccessLevel eight = AccessLevel.Level8;
        protected const AccessLevel nine = AccessLevel.Level9;
        protected const AccessLevel ten = AccessLevel.Level10;

        [Fact]
        public void None_comparison_test()
        {
            Assert.True(none < one);
            Assert.True(none < two);
            Assert.True(none < three);
            Assert.True(none < four);
            Assert.True(none < five);
            Assert.True(none < six);
            Assert.True(none < seven);
            Assert.True(none < eight);
            Assert.True(none < nine);
            Assert.True(none < ten);
        }

        [Fact]
        public void One_comparison_test()
        {
            Assert.True(one > none);
            Assert.True(one < two);
            Assert.True(one < three);
            Assert.True(one < four);
            Assert.True(one < five);
            Assert.True(one < six);
            Assert.True(one < seven);
            Assert.True(one < eight);
            Assert.True(one < nine);
            Assert.True(one < ten);
        }

        [Fact]
        public void Two_comparison_test()
        {
            Assert.True(two > none);
            Assert.True(two > one);
            Assert.True(two < three);
            Assert.True(two < four);
            Assert.True(two < five);
            Assert.True(two < six);
            Assert.True(two < seven);
            Assert.True(two < eight);
            Assert.True(two < nine);
            Assert.True(two < ten);
        }

        [Fact]
        public void Three_comparison_test()
        {
            Assert.True(three > none);
            Assert.True(three > one);
            Assert.True(three > two);
            Assert.True(three < four);
            Assert.True(three < five);
            Assert.True(three < six);
            Assert.True(three < seven);
            Assert.True(three < eight);
            Assert.True(three < nine);
            Assert.True(three < ten);
        }

        [Fact]
        public void Four_comparison_test()
        {
            Assert.True(four > none);
            Assert.True(four > one);
            Assert.True(four > two);
            Assert.True(four > three);
            Assert.True(four < five);
            Assert.True(four < six);
            Assert.True(four < seven);
            Assert.True(four < eight);
            Assert.True(four < nine);
            Assert.True(four < ten);
        }

        [Fact]
        public void Five_comparison_test()
        {
            Assert.True(five > none);
            Assert.True(five > one);
            Assert.True(five > two);
            Assert.True(five > three);
            Assert.True(five > four);
            Assert.True(five < six);
            Assert.True(five < seven);
            Assert.True(five < eight);
            Assert.True(five < nine);
            Assert.True(five < ten);
        }

        [Fact]
        public void Six_comparison_test()
        {
            Assert.True(six > none);
            Assert.True(six > one);
            Assert.True(six > two);
            Assert.True(six > three);
            Assert.True(six > four);
            Assert.True(six > five);
            Assert.True(six < seven);
            Assert.True(six < eight);
            Assert.True(six < nine);
            Assert.True(six < ten);
        }

        [Fact]
        public void Seven_comparison_test()
        {
            Assert.True(seven > none);
            Assert.True(seven > one);
            Assert.True(seven > two);
            Assert.True(seven > three);
            Assert.True(seven > four);
            Assert.True(seven > five);
            Assert.True(seven > six);
            Assert.True(seven < eight);
            Assert.True(seven < nine);
            Assert.True(seven < ten);
        }

        [Fact]
        public void Eight_comparison_test()
        {
            Assert.True(eight > none);
            Assert.True(eight > one);
            Assert.True(eight > two);
            Assert.True(eight > three);
            Assert.True(eight > four);
            Assert.True(eight > five);
            Assert.True(eight > six);
            Assert.True(eight > seven);
            Assert.True(eight < nine);
            Assert.True(eight < ten);
        }

        [Fact]
        public void Nine_comparison_test()
        {
            Assert.True(nine > none);
            Assert.True(nine > one);
            Assert.True(nine > two);
            Assert.True(nine > three);
            Assert.True(nine > four);
            Assert.True(nine > five);
            Assert.True(nine > six);
            Assert.True(nine > seven);
            Assert.True(nine > eight);
            Assert.True(nine < ten);
        }

        [Fact]
        public void Ten_comparison_test()
        {
            Assert.True(ten > none);
            Assert.True(ten > one);
            Assert.True(ten > two);
            Assert.True(ten > three);
            Assert.True(ten > four);
            Assert.True(ten > five);
            Assert.True(ten > six);
            Assert.True(ten > seven);
            Assert.True(ten > eight);
            Assert.True(ten > nine);
        }
    }
}
