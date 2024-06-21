namespace xofz.Framework.Axioses
{
    public class Inanimator
        : Axios
    {
        public virtual void Process(
            Do behavior)
        {
            behavior?.Invoke();
        }
    }
}