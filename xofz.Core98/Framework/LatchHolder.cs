namespace xofz.Framework
{
    using System.Threading;

    public class LatchHolder
    {
        public virtual ManualResetEvent Latch { get; set; }

        public virtual AutoResetEvent AutoLatch { get; set; }
    }
}
