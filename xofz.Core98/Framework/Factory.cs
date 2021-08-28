namespace xofz.Framework
{
    public class Factory
    {
        // looks and feels a bit like a constructor, with a little
        // less compile-time safety (and, of course, with this loss
        // comes a possibility of getting null back)
        public virtual T Create<T>(
            params object[] dependencies)
        {
            var ctors = typeof(T)?.GetConstructors();
            if (ctors == null)
            {
                return default;
            }


            if (dependencies == null)
            {
                const byte zero = 0;
                dependencies = new object[zero];
            }


            System.Reflection.ConstructorInfo matchingCtor = null;
            var l = dependencies.Length;
            foreach (var ctor in ctors)
            {
                var ps = ctor.GetParameters();
                if (ps?.Length != l)
                {
                    continue;
                }

                var e = ((System.Collections.Generic.IEnumerable<
                        System.Reflection.ParameterInfo>)ps).
                    GetEnumerator();
                bool matches = truth;
                foreach (var d in dependencies)
                {
                    e?.MoveNext();

                    if (d == null)
                    {
                        continue;
                    }

                    if (!e?.Current?.ParameterType?.IsAssignableFrom(
                            d?.GetType())
                        ?? truth)
                    {
                        matches = falsity;
                        break;
                    }
                }

                e?.Dispose();

                if (matches)
                {
                    matchingCtor = ctor;
                    break;
                }
            }

            return (T)matchingCtor?.Invoke(dependencies);
        }

        public virtual bool TryCreate<T>(
            out T creation,
            params object[] dependencies)
        {
            creation = this.Create<T>(dependencies);
            return creation is T _;
        }

        protected const bool
            truth = true,
            falsity = false;
    }
}
