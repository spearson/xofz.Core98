namespace xofz.Framework
{
    public class Axiom<T>
    {
        public virtual T Study { get; set; }

        public virtual MethodWebV2 W { get; set; }

        public virtual void Formulate()
        {
            this.W?.RegisterDependency(
                this.Study);
        }

        public virtual void Unformulate()
        {
            this.W?.Unregister<T>();
        }
    }
}
