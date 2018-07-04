namespace xofz.Tests.Framework
{
    using System;
    using System.Diagnostics;
    using FakeItEasy;
    using xofz.Framework;
    using Xunit;

    public class EventSubscriberTests
    {
        public class Context
        {
            protected Context()
            {
                this.subscriber = new EventSubscriber();
            }

            protected readonly EventSubscriber subscriber;
        }

        public class When_Subscribe_is_called : Context
        {
            [Fact]
            public void Actually_subscribes_to_the_event_for_an_action()
            {
                var tester = new TestEventer();
                this.subscriber.Subscribe(
                    tester,
                    nameof(tester.Pinged),
                    this.tester_Pinged);
                this.worked = false;
                new EventRaiser().Raise(
                    tester,
                    nameof(tester.Pinged));
                Assert.True(this.worked);
            }

            private void tester_Pinged()
            {
                this.worked = true;
            }

            private class TestEventer
            {
                public event xofz.Action Pinged;

                public event EventHandler StandardEvent;

                public event EventHandler<TestEventArgs> RequestPossibleGreeting;
            }

            private class TestEventArgs : EventArgs
            {
                public virtual string Greeting { get; set; }
            }

            private bool worked;

            [Fact]
            public void Does_it_throw_if_i_mess_up_the_handler_method_signature()
            {
                var testEventer = new TestEventer();
                Assert.Throws<ArgumentException>(
                    () => this.subscriber.Subscribe<string>(
                        testEventer,
                        nameof(testEventer.Pinged),
                        this.hereWeGo));
                // yes
            }

            private void hereWeGo(string blah)
            {
                Console.WriteLine(@"not here");
            }

            [Fact]
            public void Can_subscribe_to_standard_NET_events_too()
            {
                var testEventer = new TestEventer();
                this.worked = false;
                this.subscriber.Subscribe(
                    testEventer,
                    nameof(testEventer.StandardEvent),
                    this.handleStandardEvent);
                new EventRaiser().Raise(
                    testEventer,
                    nameof(testEventer.StandardEvent),
                    testEventer,
                    EventArgs.Empty);

                Assert.True(this.worked);
            }

            [Fact]
            public void Can_subscribe_to_typed_NET_events_too()
            {
                var testEventer = new TestEventer();
                this.worked = false;
                EventHandler<TestEventArgs> handler =
                    this.handleTypedEventArgsEvent;
                this.subscriber.Subscribe(
                    testEventer,
                    nameof(testEventer.RequestPossibleGreeting),
                    handler);
                var args = new TestEventArgs
                {
                    Greeting = "Hello world!!"
                };
                new EventRaiser().Raise(
                    testEventer,
                    nameof(testEventer.RequestPossibleGreeting),
                    testEventer,
                    args);

                Assert.True(this.worked);
            }

            private void handleStandardEvent(object sender, EventArgs e)
            {
                Assert.True(sender.GetType() == typeof(TestEventer));

                Assert.Equal(e, EventArgs.Empty);

                this.worked = true;
            }

            private void handleTypedEventArgsEvent(object sender,
                TestEventArgs e)
            {
                Trace.WriteLine(e.Greeting);

                this.worked = true;
            }
        }
    }
}
