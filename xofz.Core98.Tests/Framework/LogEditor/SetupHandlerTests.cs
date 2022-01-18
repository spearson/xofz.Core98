namespace xofz.Tests.Framework.LogEditor
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.LogEditor;
    using xofz.Framework.Logging;
    using xofz.UI;
    using xofz.UI.LogEditor;
    using Xunit;

    public class SetupHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new SetupHandler(
                    this.web);
                this.ui = A.Fake<LogEditorUi>();
                this.uiRW = new UiReaderWriter();
                this.applier = A.Fake<LabelApplier>();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.applier);
            }

            protected readonly MethodWeb web;
            protected readonly SetupHandler handler;
            protected readonly LogEditorUi ui;
            protected readonly UiReaderWriter uiRW;
            protected readonly LabelApplier applier;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Sets_Types()
            {
                this.ui.Types = null;

                this.handler.Handle(
                    this.ui);

                Assert.NotNull(
                    this.ui.Types);
            }

            [Fact]
            public void Sets_selected_type_to_Information()
            {
                this.ui.SelectedType = null;

                this.handler.Handle(
                    this.ui);

                Assert.Equal(
                    DefaultEntryTypes.Information,
                    this.ui.SelectedType);
            }

            [Fact]
            public void Sets_CustomTypeVisible_to_false()
            {
                this.ui.CustomTypeVisible = true;

                this.handler.Handle(
                    this.ui);

                Assert.False(
                    this.ui.CustomTypeVisible);
            }

            [Fact]
            public void If_ui_is_v2_calls_applier_Apply()
            {
                var v2 = A.Fake<LogEditorUiV2>();

                this.handler.Handle(
                    v2);

                A
                    .CallTo(() => this.applier.Apply(
                        v2))
                    .MustHaveHappened();
            }
        }
    }
}
