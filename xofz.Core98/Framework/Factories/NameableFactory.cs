namespace xofz.Framework.Factories
{
    using xofz.Framework;
    using EH = xofz.EnumerableHelpers;

    public class NameableFactory
        : Factory
    {
        // pass the Nameable.Name last, after all constructor arguments
        public override T Create<T>(
            params object[] dependencies)
        {
            var ds = dependencies;
            if (ds == null)
            {
                return base.Create<T>();
            }

            var l = ds.Length;
            if (l < one || !typeof(Nameable).IsAssignableFrom(typeof(T)))
            {
                return base.Create<T>(ds);
            }

            var possibleNameable = base.Create<T>(
                EH.ToArray(
                    EH.Take(
                        ds,
                        l - one)));
            if (possibleNameable is Nameable n &&
                EH.Last(ds) is string name)
            {
                n.Name = name;
            }

            return possibleNameable;
        }

        protected const byte one = 1;
    }
}
