namespace xofz.Tests.Framework.LogEditor
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.LogEditor;
    using xofz.UI;
    using Xunit;

    public class LabelApplierTests
    {
        public class Context
        {
            protected Context()
            {
                var w = new MethodWeb();
                this.applier = new LabelApplier(
                    w);
                this.ui = A.Fake<LogEditorUiV2>();
                this.labels = new Labels();
                this.uiRW = new UiReaderWriter();
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.uiRW);

                this.runner = w;
            }

            protected readonly MethodRunner runner;
            protected readonly LabelApplier applier;
            protected readonly LogEditorUiV2 ui;
            protected readonly Labels labels;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Apply_is_called : Context
        {
            [Fact]
            public void Sets_EntryTypeLabelLabel_to_EntryTypeLabel()
            {
                this.ui.EntryTypeLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.EntryTypeLabel,
                    this.ui.EntryTypeLabelLabel);
            }

            [Fact]
            public void Sets_EntryContentLabelLabel_to_EntryContentLabel()
            {
                this.ui.EntryContentLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.EntryContentLabel,
                    this.ui.EntryContentLabelLabel);
            }

            [Fact]
            public void Sets_AddKeyLabel_to_AddKey()
            {
                this.ui.AddKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.AddKey,
                    this.ui.AddKeyLabel);
            }

            [Fact]
            public void Sets_TitleLabel_to_Title()
            {
                this.ui.TitleLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.Title,
                    this.ui.TitleLabel);
            }
        }
    }
}
