// ------------------------------------------------------------------------------------------------
// <copyright file="Timer.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace xofz.Framework
{
    using System;

    public class Timer
        : IDisposable
    {
        public Timer()
            : this(new System.Timers.Timer(), new object())
        {
        }

        protected Timer(
            System.Timers.Timer innerTimer)
            : this(innerTimer, new object())
        {
        }

        protected Timer(
            object locker)
            : this(new System.Timers.Timer(), locker)
        {
        }

        protected Timer(
            System.Timers.Timer innerTimer,
            object locker)
        {
            innerTimer = innerTimer
                         ?? new System.Timers.Timer();
            locker = locker
                     ?? new object();
            this.innerTimer = innerTimer;
            this.locker = locker;
            innerTimer.Elapsed += this.innerTimer_Elapsed;
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
            lock (this.locker ?? new object())
            {
                if (this.started)
                {
                    return;
                }

                const int max = int.MaxValue;
                if (intervalMilliseconds > max)
                {
                    intervalMilliseconds = max;
                }

                var it = this.innerTimer;
                if (it != null)
                {
                    it.Interval = intervalMilliseconds;
                }
                
                it?.Start();
                this.started = true;
            }
        }

        public virtual void Stop()
        {
            lock (this.locker ?? new object())
            {
                if (!this.started)
                {
                    return;
                }

                this.innerTimer?.Stop();
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

        protected bool started, shouldReset;
        protected readonly System.Timers.Timer innerTimer;
        protected readonly object locker;
    }
}
