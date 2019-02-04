namespace xofz.Framework
{
    public class Synchronizer<T>
    {
        public Synchronizer(T t)
            : this(t, new object())
        {
        }

        protected Synchronizer(T t, object locker)
        {
            this.t = t;
            this.locker = locker;
        }

        public virtual void RunSync(Do<T> syncAction)
        {
            var actor = this.t;
            lock (this.locker)
            {
                syncAction?.Invoke(actor);
            }
        }

        protected readonly T t;
        protected readonly object locker;
    }
}
