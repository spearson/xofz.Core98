namespace xofz.Tests.Framework.Transformation
{
    using System;
    using System.Collections.Generic;
    using Ploeh.AutoFixture;
    using xofz.Framework.Transformation;
    using Xunit;

    public class ReverserTests
    {
        public class TestReverser
            : Reverser
        {
            public virtual void ReverseWithoutHelp<T>(
                IList<T> list)
            {
                if (list == null)
                {
                    return;
                }

                this.reverseAbstract(
                    i => list[(int)i],
                    (item, i) => list[(int)i] = item,
                    list.Count,
                    Environment.ProcessorCount);
            }
        }

        public class Context
        {
            protected Context()
            {
                this.reverser = new TestReverser();
                this.fixture = new Fixture();
            }

            protected readonly TestReverser reverser;
            protected readonly Fixture fixture;
        }

        public class When_Reverse_is_called : Context
        {
            [Fact]
            public void Reverses_arrays_and_lists()
            {
                var r = this.reverser;
                var s = this.fixture.Create<string>();
                var array = s.ToCharArray();
                var l = array.Length;
                r.Reverse(array);
                for (var i = 0; i < l; ++i)
                {
                    Assert.Equal(
                        s[i],
                        array[l - i - 1]);
                }
            }

            [Fact]
            public void Reverses_the_collection_for_all_types()
            {
                var r = this.reverser;
                var s = this.fixture.Create<string>();
                var array = s.ToCharArray();
                var l = array.Length;
                r.ReverseWithoutHelp(array);
                for (var i = 0; i < l; ++i)
                {
                    Assert.Equal(
                        s[i], 
                        array[l - i - 1]);
                }
            }
        }
    }
}
