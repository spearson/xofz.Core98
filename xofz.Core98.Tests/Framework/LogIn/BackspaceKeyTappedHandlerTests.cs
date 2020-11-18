namespace xofz.Tests.Framework.LogIn
{
    using System.Security;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Login;
    using xofz.UI;
    using Xunit;

    public class BackspaceKeyTappedHandlerTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.handler = new BackspaceKeyTappedHandler(
                    this.web);
                this.ui = A.Fake<LoginUi>();
                this.toolSet = A.Fake<SecureStringToolSet>();
                this.uiRW = new UiReaderWriter();
                this.fixture = new Fixture();

                var w = this.web;
                w.RegisterDependency(
                    this.toolSet);
                w.RegisterDependency(
                    this.uiRW);
            }

            protected readonly MethodWeb web;
            protected readonly BackspaceKeyTappedHandler handler;
            protected readonly LoginUi ui;
            protected readonly SecureStringToolSet toolSet;
            protected readonly UiReaderWriter uiRW;
            protected readonly Fixture fixture;
        }

        public class When_Handle_is_called : Context
        {
            [Fact]
            public void Reads_ui_CurrentPassword()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.ui.CurrentPassword)
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_toolSet_Decode()
            {
                var password = this.fixture.Create<SecureString>();
                this.ui.CurrentPassword = password;

                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.toolSet.Decode(
                        password))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_toolSet_Encode_on_backspaced_password()
            {
                var password = this.fixture.Create<string>();
                var spw = this.fixture.Create<SecureString>();
                this.ui.CurrentPassword = spw;
                A
                    .CallTo(() => this.toolSet.Decode(
                        spw))
                    .Returns(password);
                var pwBackspace = StringHelpers.RemoveEndChars(
                    password,
                    1);
                var spwb = new SecureString();
                foreach (var c in pwBackspace)
                {
                    spwb.AppendChar(c);
                }

                A
                    .CallTo(() => this.toolSet.Encode(
                        pwBackspace))
                    .Returns(spwb);

                this.handler.Handle(
                    this.ui);

                Assert.Same(
                    spwb,
                    this.ui.CurrentPassword);
            }

            [Fact]
            public void Calls_ui_FocusPassword()
            {
                this.handler.Handle(
                    this.ui);

                A
                    .CallTo(() => this.ui.FocusPassword())
                    .MustHaveHappened();
            }
        }
    }
}
