namespace xofz.Framework.Axioses
{
    public class Safer
        : Axios
    {
        public virtual void Process(
            Do behavior)
        {
            try
            {
                behavior?.Invoke();
            }
            catch
            {
                // fizzle
            }
        }

        public virtual void Process(
            Do behavior,
            Do onCatch)
        {
            try
            {
                behavior?.Invoke();
            }
            catch
            {
                onCatch?.Invoke();
            }
        }
    }
}
