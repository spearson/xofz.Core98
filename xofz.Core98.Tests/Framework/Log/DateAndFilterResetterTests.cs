namespace xofz.Tests.Framework.Log
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Log;
    using xofz.UI;
    using Xunit;

    public class DateAndFilterResetterTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = new MethodWeb();
                this.resetter = new DateAndFilterResetter(
                    this.web);
                this.ui = A.Fake<LogUi>();
                this.unsub = A.Fake<Do>();
                this.sub = A.Fake<Do>();
                this.fixture = new Fixture();
                this.name = this.fixture.Create<string>();
                this.fields = new FieldHolder();
                this.uiRW = new UiReaderWriter();
                this.provider = A.Fake<TimeProvider>();
                this.reloader = A.Fake<EntryReloader>();
                this.now = this.fixture.Create<DateTime>();
                this.fields.startedFirstTimeIf1 = 1;
                this.fields.startedIf1 = 1;

                var w = this.web;
                w.RegisterDependency(
                    this.fields,
                    this.name);
                w.RegisterDependency(
                    this.uiRW);
                w.RegisterDependency(
                    this.provider);
                w.RegisterDependency(
                    this.reloader);

                A
                    .CallTo(() => this.provider.Now())
                    .Returns(this.now);
            }

            protected readonly MethodWeb web;
            protected readonly DateAndFilterResetter resetter;
            protected readonly LogUi ui;
            protected readonly Do unsub;
            protected readonly Do sub;
            protected readonly Fixture fixture;
            protected readonly string name;
            protected readonly FieldHolder fields;
            protected readonly UiReaderWriter uiRW;
            protected readonly TimeProvider provider;
            protected readonly EntryReloader reloader;
            protected readonly DateTime now;
        }

        public class When_Reset_is_called : Context
        {
            [Fact]
            public void If_dates_and_filters_unchanged_does_not_reload()
            {
                this.ui.StartDate = this.now.Date.Subtract(
                    TimeSpan.FromDays(6));
                this.ui.EndDate = this.now.Date;
                this.ui.FilterContent = string.Empty;
                this.ui.FilterType = string.Empty;

                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                A
                    .CallTo(() => this.reloader.Reload(
                        this.ui,
                        this.name))
                    .MustNotHaveHappened();
            }

            [Fact]
            public void Calls_unsub_if_reload_needed()
            {
                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                A
                    .CallTo(() => this.unsub.Invoke())
                    .MustHaveHappened();
            }

            [Fact]
            public void Sets_ui_StartDate()
            {
                this.ui.StartDate = default;

                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                Assert.Equal(
                    this.now.Date.AddDays(-6),
                    this.ui.StartDate);
            }

            [Fact]
            public void Sets_ui_EndDate()
            {
                this.ui.EndDate = default;

                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                Assert.Equal(
                    this.now.Date,
                    this.ui.EndDate);
            }

            [Fact]
            public void Sets_ui_FilterType_to_empty_string()
            {
                this.ui.FilterType = this.fixture.Create<string>();

                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                Assert.Empty(
                    this.ui.FilterType);
            }

            [Fact]
            public void Sets_ui_FilterContent_to_empty_string()
            {
                this.ui.FilterContent = this.fixture.Create<string>();

                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                Assert.Empty(
                    this.ui.FilterContent);
            }

            [Fact]
            public void Calls_reloader_Reload()
            {
                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                A
                    .CallTo(() => this.reloader.Reload(
                        this.ui,
                        this.name))
                    .MustHaveHappened();
            }

            [Fact]
            public void Calls_sub()
            {
                this.resetter.Reset(
                    this.ui,
                    this.unsub,
                    this.sub,
                    this.name);

                A
                    .CallTo(() => this.sub.Invoke())
                    .MustHaveHappened();
            }
        }
    }
}
