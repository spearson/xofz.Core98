namespace xofz.Framework.Timers
{
    using System;
    using xofz.Framework.Logging;

    public class LoggingTimer 
        : Timer
    {
        public LoggingTimer(
            MethodRunner runner)
        {
            this.runner = runner;
            this.log = (lt, le) =>
            {
                le?.AddEntry(
                    DefaultEntryTypes.Information,
                    new[]
                    {
                        @"Timer " + this.Name + @" elapsed.",
                        @"Interval: " + lt.CurrentInterval
                    });
            };
        }

        public LoggingTimer(
            Do<LoggingTimer, LogEditor> log,
            MethodRunner runner)
        {
            this.log = log;
            this.runner = runner;
        }

        public virtual string LogName
        {
            get => this.logName;

            set => this.logName = value;
        }

        public virtual string Name { get; set; }

        public virtual TimeSpan CurrentInterval
        {
            get => this.interval;

            set => this.interval = value;
        }

        public override void Start(
            TimeSpan interval)
        {
            if (!base.started)
            {
                this.setCurrentInterval(interval);
            }

            base.Start(interval);
        }

        public override void Start(
            long intervalMilliseconds)
        {
            if (!base.started)
            {
                this.setCurrentInterval(
                    TimeSpan.FromMilliseconds(
                        (double)intervalMilliseconds));
            }

            base.Start(intervalMilliseconds);
        }

        protected override void innerTimer_Elapsed(
            object sender,
            System.Timers.ElapsedEventArgs e)
        {
            var r = this.runner;
            r.Run<LogEditor>(le =>
                {
                    this.log(this, le);
                },
                this.LogName);

            base.innerTimer_Elapsed(sender, e);
        }

        protected virtual void setCurrentInterval(
            TimeSpan currentInterval)
        {
            this.interval = currentInterval;
        }

        protected TimeSpan interval;
        protected Do<LoggingTimer, LogEditor> log;
        protected readonly MethodRunner runner;
        private string logName;
    }
}
