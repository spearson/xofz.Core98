namespace xofz.Framework.Impossibility
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Timer = Framework.Timer;

    public class InfiniteLoopTester : IDisposable
    {
        public InfiniteLoopTester(Timer timer)
        {
            this.timer = timer;
            this.timerHandlerFinished = new AutoResetEvent(true);
            this.timer.Elapsed += this.timer_Elapsed;
        }

        public virtual event Action<Thread> InfiniteLooped;

        public virtual void Start(Thread t)
        {
            this.setCurrentThread(t);
            this.timer.Start(250);
        }

        // make sure to call this when inheriting please!
        public virtual void Dispose()
        {
            this.timer.Stop();
            this.timerHandlerFinished.WaitOne();
        }

        private void setCurrentThread(Thread currentThread)
        {
            this.currentThread = currentThread;
        }

        private void timer_Elapsed()
        {
            var processorUsage = getThreadCpuUsage();
            if (processorUsage > .9999)
            {
                this.timer.Elapsed -= this.timer_Elapsed;
                new Thread(() => this.InfiniteLooped?.Invoke(this.currentThread)).Start();
            }
        }

        private double getThreadCpuUsage()
        {
            // todo: get the processor usage of the thread
            return 0d;
        }

        private void setKernelTime(long kernelTime)
        {
            this.kernelTime = kernelTime;
        }

        private Thread currentThread;
        private long kernelTime;
        private readonly Timer timer;
        private readonly AutoResetEvent timerHandlerFinished;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetThreadTimes(
            IntPtr hThread, 
            out long lpCreationTime, 
            out long lpExitTime, 
            out long lpKernelTime, 
            out long lpUserTime);
    }
}
