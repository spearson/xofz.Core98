namespace xofz.Tests.Framework
{
    using xofz.Framework;
    using Xunit;

    public class StarTests
    {
        [Fact]
        public void Go()
        {
            var g = new Star
            {
                W = new MethodWeb()
            };
        }
    }
}
