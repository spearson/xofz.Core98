namespace xofz.Framework.Axioses
{
    public class Raiser
        : Axios
    {
        public virtual event Do Raised;

        public virtual void Process(
            Do behavior)
        {
            this
                ?.GetType()
                ?.GetEvent(nameof(this.Raised))
                ?.AddEventHandler(
                    this,
                    behavior);
        }

        public virtual void Unprocess(
            Do behavior)
        {
            this
                ?.GetType()
                ?.GetEvent(nameof(this.Raised))
                ?.RemoveEventHandler(
                    this,
                    behavior);
        }

        public virtual void Raise()
        {
            this.Raised?.Invoke();
        }
    }
}
