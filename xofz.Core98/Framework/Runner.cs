namespace xofz.Framework
{
    public class Runner<T>
    {
        public virtual T O { get; set; }

        public virtual T Run(
            Do<T> engine)
        {
            var o = this.O;
            engine?.Invoke(o);

            return o;
        }
    }

    public class Runner 
        : Runner<object>
    {
        public virtual T Run<T>(
            Do<T> engine)
        {
            if (this.O is T o)
            {
                engine?.Invoke(o);
                return o;
            }

            return default;
        }
    }
}