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
            this.autoReset = true;

            this.innerTimer = new System.Timers.Timer();
            this.innerTimer.Elapsed += this.innerTimer_Elapsed;

            this.locker = new object();
        }

        public virtual event Action Elapsed;

        public virtual bool AutoReset
        {
            get => this.autoReset;

            set => this.autoReset = value;
        }

        public virtual void Start(TimeSpan interval)
        {
            this.Start((long)interval.TotalMilliseconds);
        }

        public virtual void Start(long intervalMilliseconds)
        {
            lock (this.locker)
            {
                if (this.started)
                {
                    return;
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

        protected volatile bool autoReset;
        protected bool started;
        private readonly System.Timers.Timer innerTimer;
        private readonly object locker;
    }
}
