namespace xofz.Tests.Framework
{
    using xofz.Framework;
    using Xunit;

    public class AxiomTests
    {
        public class Context
        {
            protected Context()
            {
                this.axiom = new Axiom<object>();
                this.web = new MethodWebV2();
            }

            protected readonly MethodWebV2 web;
            protected readonly Axiom<object> axiom;
        }

        public class When_Study_is_called : Context
        {
            [Fact]
            public void Returns_the_study()
            {
                var o = new object();
                var a = this.axiom;

                Assert.NotSame(
                    o,
                    a.Study);
                a.Study = o;
                Assert.Same(
                    o,
                    a.Study);
            }
        }

        public class When_W_is_called : Context
        {
            [Fact]
            public void Returns_the_MethodWebV2()
            {
                var a = this.axiom;
                var w = this.web;

                Assert.NotSame(
                    w,
                    a.W);
                a.W = w;
                Assert.Same(
                    w,
                    a.W);
            }
        }

        public class When_Formulate_is_called : Context
        {
            [Fact]
            public void Registers_the_Study()
            {
                var a = this.axiom;
                var w = this.web;
                var o = new object();
                a.Study = o;
                a.W = w;

                Assert.Null(
                    w.Run<object>());

                a.Formulate();

                Assert.NotNull(
                    w.Run<object>());
            }
        }

        public class When_unformulate_is_called : Context
        {
            [Fact]
            public void Unregisters_the_Study()
            {
                var a = this.axiom;
                var w = this.web;
                var o = new object();
                a.Study = o;
                a.W = w;

                a.Formulate();

                Assert.NotNull(
                    w.Run<object>());

                a.Unformulate();

                Assert.Null(
                    w.Run<object>());
            }
        }
    }
}
