namespace xofz.Tests.Framework
{
    using System;
    using xofz.Framework;
    using Xunit;

    public class VersionReaderTests
    {
        public class Context
        {
            protected Context()
            {
                this.reader = new VersionReader();
            }

            protected VersionReader reader;
        }

        public class When_ReadAsVersion1_is_called 
            : Context
        {
            [Fact]
            public void Returns_unit_for_null()
            {
                this.reader = new VersionReader(
                    null);

                Assert.Equal(
                    new Version(0, 0, 0, 0),
                    this.reader.ReadAsVersion());
            }
        }

        public class When_ReadAsVersion2_is_called : Context
        {
            [Fact]
            public void Returns_unit_for_null()
            {
                Assert.Equal(
                    new Version(0, 0, 0, 0),
                    this.reader.ReadAsVersion(
                        null));
            }
        }
    }
}
