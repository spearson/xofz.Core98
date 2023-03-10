namespace xofz
{
    public class Referencer
    {
        public virtual Gen<Referencer> Generate { get; set; }

        public virtual Referencer Reference(
            ref object o)
        {
            if (o is Referencer)
            {
                o = this.Generate?.Invoke();
                return this;
            }

            return this;
        }
    }
}