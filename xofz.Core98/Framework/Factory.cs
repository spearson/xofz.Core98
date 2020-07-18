namespace xofz.Framework
{
    using System;

    public class Factory
    {
        // looks and feels a bit like a constructor, with a little
        // less compile-time safety (and, of course, with this loss
        // comes a possibility of getting null back)
        public virtual T Create<T>(
            params object[] dependencies)
        {
            try
            {
                return (T)Activator.CreateInstance(
                    typeof(T),
                    dependencies);
            }
            catch
            {
                return default;
            }
        }
    }
}
