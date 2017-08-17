namespace xofz
{
    using System.Threading;

    public class Delayer
    {
        public Delayer()
            : this(ThreadPriority.AboveNormal)
        {
        }

        public Delayer(ThreadPriority priority)
        {
            this.priority = priority;
            this.finishedSleeping = new ManualResetEvent(true);
        }

        // please have one delayer per thread
        public virtual void Delay(int milliseconds)
        {
            var fs = this.finishedSleeping;
            fs.Reset();
            ThreadPool.QueueUserWorkItem(
                state =>
                {
                    Thread.Sleep(milliseconds);
                    fs.Set();
                });
            var t = new Thread(() =>
                {
                    Thread.Sleep(milliseconds);
                    fs.Set();
                })
                { Priority = this.priority };
            t.Start();
            fs.WaitOne(milliseconds);
        }

        private readonly ThreadPriority priority;
        private readonly ManualResetEvent finishedSleeping;
    }
}
