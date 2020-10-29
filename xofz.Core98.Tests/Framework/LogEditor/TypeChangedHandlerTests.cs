namespace xofz.Tests.Framework.LogEditor
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.LogEditor;
    using xofz.Framework.Logging;
    using xofz.UI;
    using Xunit;

    public class TypeChangedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new TypeChangedHandler(
                    this.web);
                this.ui = A.Fake<LogEditorUi>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly TypeChangedHandler handler;
            protected readonly LogEditorUi ui;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Handle1_is_called : Context
        {
            [Fact]
            public void If_custom_selected_turns_on_custom_type_field()
            {
                this.ui.CustomTypeVisible = false;
                this.ui.SelectedType = DefaultEntryTypes.Custom;

                this.handler.Handle(
                    this.ui);

                Assert.True(
                    this.ui.CustomTypeVisible);
            }

            [Fact]
            public void Otherwise_turns_it_off()
            {
                this.ui.CustomTypeVisible = true;
                this.ui.SelectedType = this.fixture.Create<string>();

                this.handler.Handle(
                    this.ui);

                Assert.False(
                    this.ui.CustomTypeVisible);
            }
        }

        public class When_Handle2_is_called : Context
        {
            [Fact]
            public void If_custom_selected_turns_on_custom_type_field()
            {
                this.ui.CustomTypeVisible = false;
                this.ui.SelectedType = DefaultEntryTypes.Custom;

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.True(
                    this.ui.CustomTypeVisible);
            }

            [Fact]
            public void Otherwise_turns_it_off()
            {
                this.ui.CustomTypeVisible = true;
                this.ui.SelectedType = this.fixture.Create<string>();

                this.handler.Handle(
                    this.ui,
                    this.name);

                Assert.False(
                    this.ui.CustomTypeVisible);
            }
        }
    }
}
