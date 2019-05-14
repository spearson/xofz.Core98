// ------------------------------------------------------------------------------------------------
// <copyright file="Timer.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace xofz.Framework
{
    using System;

    public class Timer : IDisposable
    {
        public Timer()
        {
            this.shouldReset = true;

            this.innerTimer = new System.Timers.Timer();
            this.innerTimer.Elapsed += this.innerTimer_Elapsed;

            this.locker = new object();
        }

        public virtual event Do Elapsed;

        public virtual bool AutoReset
        {
            get => this.shouldReset;

            set => this.shouldReset = value;
        }

        public virtual void Start(
            TimeSpan interval)
        {
            this.Start((long)interval.TotalMilliseconds);
        }

        public virtual void Start(
            long intervalMilliseconds)
        {
            lock (this.locker)
            {
                if (this.started)
                {
                    return;
                }

                if (intervalMilliseconds > int.MaxValue)
                {
                    intervalMilliseconds = int.MaxValue;
                }

                var it = this.innerTimer;
                it.Interval = intervalMilliseconds;
                it.Start();
                this.started = true;
            }
        }

        public virtual void Stop()
        {
            lock (this.locker)
            {
                if (!this.started)
                {
                    return;
                }

                this.innerTimer.Stop();
                this.started = false;
            }
        }

        public virtual void Dispose()
        {
            this.innerTimer?.Dispose();
        }

        protected virtual void innerTimer_Elapsed(
            object sender, 
            System.Timers.ElapsedEventArgs e)
        {
            if (!this.AutoReset)
            {
                this.Stop();
            }

            this.Elapsed?.Invoke();
        }

        protected bool shouldReset;
        protected bool started;
        protected readonly System.Timers.Timer innerTimer;
        protected readonly object locker;
    }
}
