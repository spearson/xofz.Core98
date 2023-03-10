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
            var dependencyCount = dependencies.Length;
            foreach (var ctor in ctors)
            {
                var ps = ctor.GetParameters();
                if (ps == null)
                {
                    continue;
                }

                if (ps.Length != dependencyCount)
                {
                    continue;
                }

                var e = ((System
                        .Collections
                        .Generic
                        .IEnumerable<System
                            .Reflection
                            .ParameterInfo>)ps)
                    .GetEnumerator();
                if (e == null)
                {
                    continue;
                }

                bool matches = truth;
                foreach (var d in dependencies)
                {
                    e.MoveNext();

                    if (d == null)
                    {
                        continue;
                    }

                    var c = e.Current;
                    if (c == null)
                    {
                        matches = falsity;
                        break;
                    }

                    var p = c.ParameterType;
                    if (p == null)
                    {
                        matches = falsity;
                        break;
                    }

                    if (!p.IsInstanceOfType(
                            d))
                    {
                        matches = falsity;
                        break;
                    }
                }

                e.Dispose();

                if (matches)
                {
                    matchingCtor = ctor;
                    break;
                }
            }

            if (matchingCtor == null)
            {
                return default;
            }

            return (T)matchingCtor.Invoke(dependencies);
        }

        public virtual bool TryCreate<T>(
            out T creation,
            params object[] dependencies)
        {
            try
            {
                creation = this.Create<T>(dependencies);
                return creation is T _;
            }
            catch
            {
                creation = default;
                return falsity;
            }
            
        }

        protected const bool
            truth = true,
            falsity = false;
    }
}
