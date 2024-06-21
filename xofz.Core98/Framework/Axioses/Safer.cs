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

        public virtual void ProcessV2(
            Do behavior,
            Do onCatch)
        {
            try
            {
                behavior?.Invoke();
            }
            catch
            {
                try
                {
                    onCatch?.Invoke();
                }
                catch
                {
                    // fizzle
                }
            }
        }

        public virtual void ProcessFinally(
            Do behavior,
            Do onFinally)
        {
            try
            {
                behavior?.Invoke();
            }
            finally
            {
                onFinally?.Invoke();
            }
        }
    }
}