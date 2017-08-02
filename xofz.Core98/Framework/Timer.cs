// ------------------------------------------------------------------------------------------------
// <copyright file="Timer.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace xofz.Framework
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public class Timer : IDisposable
    {
        public Timer()
        {
            this.innerTimer = new System.Timers.Timer();
            this.innerTimer.Elapsed += this.innerTimer_Elapsed;
            this.autoReset = true;
            this.priority = ThreadPriority.Normal;
        }

        public virtual event Action Elapsed;

        public virtual bool AutoReset
        {
            get => this.autoReset;

            set => this.autoReset = value;
        }

        public virtual ThreadPriority Priority
        {
            get => this.priority;

            set
            {
                this.priority = value;
                this.threadPrioritySet = false;
            }
        }

        public virtual void Start(int intervalInMs)
        {
            if (Interlocked.CompareExchange(ref this.startedIf1, 1, 0) == 1)
            {
                return;
            }

            this.innerTimer.Interval = intervalInMs;
            this.innerTimer.Start();
        }

        public virtual void Stop()
        {
            if (Interlocked.CompareExchange(ref this.startedIf1, 0, 1) == 0)
            {
                return;
            }

            this.innerTimer.Stop();
        }

        public virtual void Dispose()
        {
            this.innerTimer?.Dispose();
        }

        private void innerTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!this.AutoReset)
            {
                this.Stop();
            }

            if (!this.threadPrioritySet)
            {
                this.changeThreadPriority();
            }

            new Thread(() => this.Elapsed?.Invoke()).Start();
        }

        private void changeThreadPriority()
        {
            Thread.CurrentThread.Priority = this.Priority;
            this.threadPrioritySet = true;
        }

        private bool threadPrioritySet;
        private int startedIf1;
        private ThreadPriority priority;
        private volatile bool autoReset;
        private readonly System.Timers.Timer innerTimer;
    }
}
