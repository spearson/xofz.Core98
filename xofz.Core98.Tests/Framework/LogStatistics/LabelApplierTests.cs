namespace xofz.Tests.Framework.LogStatistics
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.LogStatistics;
    using xofz.UI;
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
                this.ui = A.Fake<LogStatisticsUiV2>();
                this.labels = new Labels();
                this.uiRW = new UiReaderWriter();

                var w = this.web;
                w.RegisterDependency(
                    this.labels);
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly LabelApplier applier;
            protected readonly LogStatisticsUiV2 ui;
            protected readonly Labels labels;
            protected readonly UiReaderWriter uiRW;
        }

        public class When_Apply_is_called : Context
        {
            [Fact]
            public void Sets_ui_Label_to_labels_Label()
            {
                this.ui.Label = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.Label);
                Assert.Equal(
                    this.labels.Label,
                    this.ui.Label);
            }

            [Fact]
            public void Sets_ui_StartLabelLabel_to_labels_StartLabel()
            {
                this.ui.StartLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.StartLabelLabel);
                Assert.Equal(
                    this.labels.StartLabel,
                    this.ui.StartLabelLabel);
            }

            [Fact]
            public void Sets_ui_EndLabelLabel_to_labels_EndLabel()
            {
                this.ui.EndLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.EndLabelLabel);
                Assert.Equal(
                    this.labels.EndLabel,
                    this.ui.EndLabelLabel);
            }

            [Fact]
            public void Sets_ui_HideKeyLabel_to_labels_HideKey()
            {
                this.ui.HideKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.HideKeyLabel);
                Assert.Equal(
                    this.labels.HideKey,
                    this.ui.HideKeyLabel);
            }

            [Fact]
            public void Sets_ui_OverallKeyLabel_to_labels_OverallKey()
            {
                this.ui.OverallKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.OverallKeyLabel);
                Assert.Equal(
                    this.labels.OverallKey,
                    this.ui.OverallKeyLabel);
            }

            [Fact]
            public void Sets_ui_RangeKeyLabel_to_labels_RangeKey()
            {
                this.ui.RangeKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.RangeKeyLabel);
                Assert.Equal(
                    this.labels.RangeKey,
                    this.ui.RangeKeyLabel);
            }

            [Fact]
            public void
                Sets_ui_FilterContentLabelLabel_to_labels_FilterContentLabel()
            {
                this.ui.FilterContentLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.FilterContentLabelLabel);
                Assert.Equal(
                    this.labels.FilterContentLabel,
                    this.ui.FilterContentLabelLabel);
            }

            [Fact]
            public void Sets_ui_FilterTypeLabelLabel_to_labels_FilterTypeLabel()
            {
                this.ui.FilterTypeLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.FilterTypeLabelLabel);
                Assert.Equal(
                    this.labels.FilterTypeLabel,
                    this.ui.FilterTypeLabelLabel);
            }

            [Fact]
            public void Sets_ui_ResetContentKeyLabel_to_labels_ResetContentKey()
            {
                this.ui.ResetContentKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.ResetContentKeyLabel);
                Assert.Equal(
                    this.labels.ResetContentKey,
                    this.ui.ResetContentKeyLabel);
            }

            [Fact]
            public void Sets_ui_ResetTypeKeyLabel_to_labels_ResetTypeKey()
            {
                this.ui.ResetTypeKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.ResetTypeKeyLabel);
                Assert.Equal(
                    this.labels.ResetTypeKey,
                    this.ui.ResetTypeKeyLabel);
            }

            [Fact]
            public void Sets_ui_StatsContainerLabel_to_labels_StatsContainer()
            {
                this.ui.StatsContainerLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.StatsContainerLabel);
                Assert.Equal(
                    this.labels.StatsContainer,
                    this.ui.StatsContainerLabel);
            }

            [Fact]
            public void
                Sets_ui_TotalEntryCountLabelLabelLabel_to_labels_TotalEntryCountLabelLabel()
            {
                this.ui.TotalEntryCountLabelLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.TotalEntryCountLabelLabelLabel);
                Assert.Equal(
                    this.labels.TotalEntryCountLabelLabel,
                    this.ui.TotalEntryCountLabelLabelLabel);
            }

            [Fact]
            public void
                Sets_ui_AvgEntriesPerDayLabelLabelLabel_to_labels_AvgEntriesPerDayLabelLabel()
            {
                this.ui.AvgEntriesPerDayLabelLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.AvgEntriesPerDayLabelLabelLabel);
                Assert.Equal(
                    this.labels.AvgEntriesPerDayLabelLabel,
                    this.ui.AvgEntriesPerDayLabelLabelLabel);
            }

            [Fact]
            public void
                Sets_ui_OldestTimestampLabelLabelLabel_to_labels_OldestTimstampLabelLabel()
            {
                this.ui.OldestTimestampLabelLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.OldestTimestampLabelLabelLabel);
                Assert.Equal(
                    this.labels.OldestTimestampLabelLabel,
                    this.ui.OldestTimestampLabelLabelLabel);
            }

            [Fact]
            public void
                Sets_ui_NewestTimestampLabelLabelLabel_to_labels_NewestTimstampLabelLabel()
            {
                this.ui.NewestTimestampLabelLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.NewestTimestampLabelLabelLabel);
                Assert.Equal(
                    this.labels.NewestTimestampLabelLabel,
                    this.ui.NewestTimestampLabelLabelLabel);
            }

            [Fact]
            public void
                Sets_ui_EarliestTimestampLabelLabelLabel_to_labels_EarliestTimestampLabelLabel()
            {
                this.ui.EarliestTimestampLabelLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.EarliestTimestampLabelLabelLabel);
                Assert.Equal(
                    this.labels.EarliestTimestampLabelLabel,
                    this.ui.EarliestTimestampLabelLabelLabel);
            }

            [Fact]
            public void
                Sets_ui_LatestTimestampLabelLabelLabel_labels_LatestTimstampLabelLabel()
            {
                this.ui.LatestTimestampLabelLabelLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.NotNull(
                    this.ui.LatestTimestampLabelLabelLabel);
                Assert.Equal(
                    this.labels.LatestTimestampLabelLabel,
                    this.ui.LatestTimestampLabelLabelLabel);
            }
        }
    }
}
