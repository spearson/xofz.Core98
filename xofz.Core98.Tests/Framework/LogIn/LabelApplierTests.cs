namespace xofz.Tests.Framework.Login
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using Xunit;

    public class LabelApplierTests
    {
        public class Context
        {
            protected Context()
            {
                var w = new MethodWeb();
                this.applier = new LabelApplier(w);
                this.ui = A.Fake<LoginUiV2>();
                this.uiRW = new UiReaderWriter();
                this.labels = new Labels();

                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.labels);
                this.runner = w;
            }

            protected readonly MethodRunner runner;
            protected readonly LabelApplier applier;
            protected readonly LoginUiV2 ui;
            protected readonly UiReaderWriter uiRW;
            protected readonly Labels labels;
        }

        public class When_Apply_is_called 
            : Context
        {
            [Fact]
            public void Sets_PasswordLabel_to_Password()
            {
                this.ui.PasswordLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.Password,
                    this.ui.PasswordLabel);
            }

            [Fact]
            public void Sets_TimeRemainingLabel_to_TimeRemaining()
            {
                this.ui.TimeRemainingLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.TimeRemaining,
                    this.ui.TimeRemainingLabel);
            }

            [Fact]
            public void Sets_BackspaceKeyLabel_to_BackspaceKey()
            {
                this.ui.BackspaceKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.BackspaceKey,
                    this.ui.BackspaceKeyLabel);
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
            public void Sets_LoginKeyLabel_to_LoginKey()
            {
                this.ui.LogInKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.LogInKey,
                    this.ui.LogInKeyLabel);
            }

            [Fact]
            public void Sets_CancelKeyLabel_to_CancelKey()
            {
                this.ui.CancelKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.CancelKey,
                    this.ui.CancelKeyLabel);
            }

            [Fact]
            public void Sets_KeyboardKeyLabel_to_KeyboardKey()
            {
                this.ui.KeyboardKeyLabel = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.KeyboardKey,
                    this.ui.KeyboardKeyLabel);
            }

            [Fact]
            public void Sets_Title_to_labels_Title()
            {
                this.ui.Title = null;

                this.applier.Apply(
                    this.ui);

                Assert.Equal(
                    this.labels.Title,
                    this.ui.Title);
            }
        }
    }
}
