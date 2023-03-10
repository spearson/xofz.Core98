namespace xofz.Tests
{
    using System;
    using Xunit;

    public class ReferencerTests
    {
        [Fact]
        public void Go()
        {
            var r = new Referencer();
            r.Generate = () => new TestReferencer();
            object o = r;
            r.Reference(
                ref o);
            if (o is TestReferencer referencer)
            {
                object o2 = null;
                referencer.Reference(ref o2);
            }
        }

        public class TestReferencer : Referencer
        {
            public override Referencer Reference(ref object o)
            {
                var x = base.Reference(ref o);
                if (x is TestReferencer referencer)
                {
                    Console.WriteLine(@"Boo yeah");
                }

                return this;
            }
        }
    }
}
