namespace xofz.Framework.Timers
{
    using System;
    using xofz.Framework.Logging;

    public class LoggingTimer : Timer
    {
        public LoggingTimer(
            MethodWeb web)
        {
            this.web = web;
            this.logName = null;
            this.log = (lt, le) =>
            {
                le.AddEntry(
                    "Information",
                    new[]
                    {
                        "Timer " + this.Name + " elapsed.",
                        "Interval: " + lt.CurrentInterval
                    });
            };
        }

        public LoggingTimer(
            Action<LoggingTimer, LogEditor> log,
            MethodWeb web)
        {
            this.log = log;
            this.web = web;
            this.logName = null;
        }

        public virtual string LogName
        {
            get => this.logName;

            set => this.logName = value;
        }

        public virtual string Name { get; set; }

        public virtual TimeSpan CurrentInterval
        {
            get => this.currentInterval;

            set => this.currentInterval = value;
        }

        public override void Start(TimeSpan interval)
        {
            if (!base.started)
            {
                this.setCurrentInterval(interval);
            }

            base.Start(interval);
        }

        public override void Start(long intervalMilliseconds)
        {
            if (!base.started)
            {
                this.setCurrentInterval(
                    TimeSpan.FromMilliseconds((double)intervalMilliseconds));
            }

            base.Start(intervalMilliseconds);
        }

        protected override void innerTimer_Elapsed(
            object sender,
            System.Timers.ElapsedEventArgs e)
        {
            var w = this.web;
            w.Run<LogEditor>(le => { this.log(this, le); },
                this.LogName);

            base.innerTimer_Elapsed(sender, e);
        }

        private void setCurrentInterval(TimeSpan currentInterval)
        {
            this.currentInterval = currentInterval;
        }

        protected TimeSpan currentInterval;
        protected Action<LoggingTimer, LogEditor> log;
        private string logName;
        private readonly MethodWeb web;
    }
}
