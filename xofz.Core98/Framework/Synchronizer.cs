namespace xofz.Framework
{
    public class Synchronizer<T>
    {
        public Synchronizer(
            T t)
            : this(t, new object())
        {
        }

        protected Synchronizer(
            T t, 
            object locker)
        {
            this.t = t;
            this.locker = locker;
        }

        public virtual T O
        {
            get => this.t;

            set => this.t = value;
        }

        public virtual void RunSync(
            Do<T> syncAction)
        {
            var actor = this.t;
            lock (this.locker)
            {
                syncAction?.Invoke(actor);
            }
        }

        protected T t;
        protected readonly object locker;
    }
}
