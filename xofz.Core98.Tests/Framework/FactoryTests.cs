﻿namespace xofz.Tests.Framework
{
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
                var nav2 = f.Create<NavigatorV2>();
                Assert.NotNull(web);
                Assert.NotNull(nav);
                Assert.NotNull(nav2);
            }
        }

        public class When_TryCreate_is_called : Context
        {
            [Fact]
            public void Returns_true_if_created()
            {
                Assert.True(
                    this.factory.TryCreate(
                        out object creation));
            }

            [Fact]
            public void Returns_false_if_not()
            {
                var garbageDep = new object();
                Assert.False(
                    this.factory.TryCreate(
                        out object creation,
                        garbageDep));
            }
        }
    }
}
