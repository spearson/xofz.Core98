namespace xofz.Tests.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework.Logging;
    using Xunit;

    public class LogHelpersTests
    {
        public class Context
        {
            protected Context()
            {
                this.editor = A.Fake<LogEditor>();
                this.fixture = new Fixture();
                this.inner = new Exception(
                    this.fixture.Create<string>());
                this.e = new Exception(
                    this.fixture.Create<string>(),
                    this.inner);
            }

            protected readonly LogEditor editor;
            protected readonly Exception e, inner;
            protected readonly Fixture fixture;
        }

        public class When_AddEntry_is_called : Context
        {
            [Fact]
            public void Calls_editor_AddEntry()
            {
                const string errorType = DefaultEntryTypes.Error;
                LogHelpers.AddEntry(
                    this.editor,
                    this.e);

                A
                    .CallTo(() => this.editor.AddEntry(
                        errorType,
                        A<IEnumerable<string>>.That.Contains(
                            this.e.Message)))
                    .MustHaveHappened();
                A
                    .CallTo(() => this.editor.AddEntry(
                        errorType,
                        A<IEnumerable<string>>.That.Contains(
                            this.e.GetType().ToString())))
                    .MustHaveHappened();
                A
                    .CallTo(() => this.editor.AddEntry(
                        errorType,
                        A<IEnumerable<string>>.That.Contains(
                            this.inner.GetType().ToString())))
                    .MustHaveHappened();
                A
                    .CallTo(() => this.editor.AddEntry(
                        errorType,
                        A<IEnumerable<string>>.That.Contains(
                            this.inner.Message)))
                    .MustHaveHappened();
            }
        }
    }
}
