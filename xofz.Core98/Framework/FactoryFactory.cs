namespace xofz.Framework
{
    public class FactoryFactory
    {
        public virtual Factory Create(
            Factory factory)
        {
            if (factory == null)
            {
                return default;
            }

            return factory.Create<Factory>();
        }
    }
}
