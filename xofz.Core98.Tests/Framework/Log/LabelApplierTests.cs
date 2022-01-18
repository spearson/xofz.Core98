namespace xofz.Tests.Framework.Log
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using xofz.UI.Log;
    using Xunit;

    public class LabelApplierTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.applier = new LabelApplier(
                    this.web);
                this.ui = A.Fake<LogUiV2>();
                this.uiRW = new UiReaderWriter();
                this.labels = new Labels();

                var w = this.web;
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.labels);
            }

            protected readonly MethodWeb web;
            protected readonly LabelApplier applier;
            protected readonly LogUiV2 ui;
            protected readonly UiReaderWriter uiRW;
            protected readonly Labels labels;
        }

        public class When_Apply_is_called : Context
        {
            [Fact]
            public void Sets_StartLabelLabel_to_StartLabel()
            {
                this.ui.StartLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.StartLabel,
                    this.ui.StartLabelLabel);
            }

            [Fact]
            public void Sets_EndLabelLabel_to_EndLabel()
            {
                this.ui.EndLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.EndLabel,
                    this.ui.EndLabelLabel);
            }

            [Fact]
            public void Sets_ClearKeyLabel_to_ClearKey()
            {
                this.ui.ClearKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.ClearKey,
                    this.ui.ClearKeyLabel);
            }

            [Fact]
            public void Sets_StatsKeyLabel_to_StatsKey()
            {
                this.ui.StatsKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.StatsKey,
                    this.ui.StatsKeyLabel);
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
            public void Sets_PreviousWeekKeyLabel_to_PreviousWeekKey()
            {
                this.ui.PreviousWeekKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.PreviousWeekKey,
                    this.ui.PreviousWeekKeyLabel);
            }

            [Fact]
            public void Sets_NextWeekKeyLabel_to_NextWeekKey()
            {
                this.ui.NextWeekKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.NextWeekKey,
                    this.ui.NextWeekKeyLabel);
            }

            [Fact]
            public void Sets_CurrentWeekKeyLabel_to_CurrentWeekKey()
            {
                this.ui.CurrentWeekKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.PreviousWeekKey,
                    this.ui.PreviousWeekKeyLabel);
            }

            [Fact]
            public void Sets_FilterContentLabelLabel_to_FilterContentLabel()
            {
                this.ui.FilterContentLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.FilterContentLabel,
                    this.ui.FilterContentLabelLabel);
            }

            [Fact]
            public void Sets_FilterTypeLabelLabel_to_FilterTypeLabel()
            {
                this.ui.FilterTypeLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.FilterTypeLabel,
                    this.ui.FilterTypeLabelLabel);
            }

            [Fact]
            public void Sets_ResetContentKeyLabel_to_ResetContentKey()
            {
                this.ui.ResetContentKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.ResetContentKey,
                    this.ui.ResetContentKeyLabel);
            }

            [Fact]
            public void Sets_ResetTypeKeyLabel_to_ResetTypeKey()
            {
                this.ui.ResetTypeKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.ResetTypeKey,
                    this.ui.ResetTypeKeyLabel);
            }

            [Fact]
            public void
                Sets_TimstampColumnHeaderLabel_to_TimestampColumnHeader()
            {
                this.ui.TimestampColumnHeaderLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.TimestampColumnHeader,
                    this.ui.TimestampColumnHeaderLabel);
            }

            [Fact]
            public void Sets_TypeColumnHeaderLabel_to_TypeColumnHeader()
            {
                this.ui.TypeColumnHeaderLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.TypeColumnHeader,
                    this.ui.TypeColumnHeaderLabel);
            }

            [Fact]
            public void Sets_ContentColumnHeaderLabel_to_ContentColumnHeader()
            {
                this.ui.ContentColumnHeaderLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.ContentColumnHeader,
                    this.ui.ContentColumnHeaderLabel);
            }
        }
    }
}
