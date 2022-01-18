namespace xofz.Tests.Framework.LogEditor
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogEditor;
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.LogEditor;
    using Xunit;

    public class AddKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new AddKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LogEditorUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.editor = A.Fake<LogEditor>();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.editor,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly AddKeyTappedHandler handler;
            protected readonly LogEditorUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly LogEditor editor;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Reads_selected_type()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.SelectedType)
                    .MustHaveHappened();
            }

            [Fact]
            public void If_selected_type_is_custom_reads_custom_type()
            {
                this.ui.SelectedType = DefaultEntryTypes.Custom;

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.CustomType)
                    .MustHaveHappened();
            }

            [Fact]
            public void Reads_content()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.Content)
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_editor_AddEntry_with_type_and_content()
            {
                var type = this.fixture.Create<string>();
                var content = new[]
                {
                    this.fixture.Create<string>(),
                    this.fixture.Create<string>()
                };
                this.ui.SelectedType = type;
                this.ui.Content = content;

                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.editor.AddEntry(
                        type,
                        content))
                    .MustHaveHappened();

            }

            [Fact]
            public void Sets_Content_to_null()
            {
                this.ui.Content = new[]
                {
                    this.fixture.Create<string>()
                };

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.Null(
                    this.ui.Content);
            }

            [Fact]
            public void Sets_selected_type_to_Information()
            {
                this.ui.SelectedType = this.fixture.Create<string>();

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.Equal(
                    DefaultEntryTypes.Information,
                    this.ui.SelectedType);
            }

            [Fact]
            public void Hides_the_ui()
            {
                this.handler.Handle(
                    this.ui,
                    this.name);

                A
                    .CallTo(() => this.ui.Hide())
                    .MustHaveHappened();
            }

        }
    }
}
