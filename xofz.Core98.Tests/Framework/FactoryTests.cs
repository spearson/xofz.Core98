namespace xofz.Tests.Framework
{
    using System;
    using xofz.Framework;
    using xofz.Presentation;
    using Xunit;

    public class FactoryTests
    {
        public class Context
        {
            protected Context()
            {
                this.factory = new Factory();
            }

            protected readonly Factory factory;
        }

        public class When_Create_is_called : Context
        {
            [Fact]
            public void Slams_home_a_MethodWebV2_and_NavigatorV2()
            {
                var f = this.factory;
                var web = f.Create<MethodWebV2>();
                var nav = f.Create<NavigatorV2>(web);
                Assert.NotNull(web);
                Assert.NotNull(nav);
            }

            [Fact]
            public void Fails_to_create_NavigatorV2_without_web()
            {
                var f = this.factory;
                var nav = f.Create<NavigatorV2>();
                Assert.Null(nav);
            }
        }
    }
}
