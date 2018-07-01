namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;

    public class MethodWeb
    {
        public MethodWeb()
        {
            this.dependencies = new LinkedList<Dependency>();
        }

        public virtual void RegisterDependency(
            object dependency,
            string name = null)
        {
            this.dependencies.AddLast(
                new Dependency
                {
                    Content = dependency,
                    Name = name
                });
        }

        public virtual T Run<T>(
            Action<T> method = null,
            string dependencyName = null)
        {
            var ds = this.dependencies;
            Dependency dependency;
            try
            {
                dependency = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is T),
                    d => d.Name == dependencyName);
            }
            catch
            {
                return default(T);
            }

            var t = (T)dependency.Content;
            method?.Invoke(t);

            return t;
        }

        public virtual Tuple<T, U> Run<T, U>(
            Action<T, U> method = null,
            string dependency1Name = null,
            string dependency2Name = null)
        {
            var ds = this.dependencies;
            Dependency dep1;
            Dependency dep2;
            try
            {
                dep1 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is T),
                    d => d.Name == dependency1Name);
                dep2 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is U),
                    d => d.Name == dependency2Name);
            }
            catch
            {
                return Tuple.Create(
                    default(T),
                    default(U));
            }

            var t = (T)dep1.Content;
            var u = (U)dep2.Content;
            method?.Invoke(t, u);

            return Tuple.Create(t, u);
        }

        public virtual Tuple<T, U, V> Run<T, U, V>(
            Action<T, U, V> method = null,
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null)
        {
            var ds = this.dependencies;
            Dependency dep1;
            Dependency dep2;
            Dependency dep3;
            try
            {
                dep1 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is T),
                    d => d.Name == dependency1Name);
                dep2 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is U),
                    d => d.Name == dependency2Name);
                dep3 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is V),
                    d => d.Name == dependency3Name);
            }
            catch
            {
                return Tuple.Create(
                    default(T),
                    default(U),
                    default(V));
            }

            var t = (T)dep1.Content;
            var u = (U)dep2.Content;
            var v = (V)dep3.Content;
            method?.Invoke(t, u, v);

            return Tuple.Create(t, u, v);
        }

        public virtual Tuple<T, U, V, W> Run<T, U, V, W>(
            Action<T, U, V, W> method = null,
            string dependency1Name = null,
            string dependency2Name = null,
            string dependency3Name = null,
            string dependency4Name = null)
        {
            var ds = this.dependencies;
            Dependency dep1;
            Dependency dep2;
            Dependency dep3;
            Dependency dep4;
            try
            {
                dep1 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is T),
                    d => d.Name == dependency1Name);
                dep2 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is U),
                    d => d.Name == dependency2Name);
                dep3 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is V),
                    d => d.Name == dependency3Name);
                dep4 = EnumerableHelpers.First(
                    EnumerableHelpers.Where(
                        ds,
                        d => d.Content is W),
                    d => d.Name == dependency4Name);
            }
            catch
            {
                return Tuple.Create(
                    default(T),
                    default(U),
                    default(V),
                    default(W));
            }

            var t = (T)dep1.Content;
            var u = (U)dep2.Content;
            var v = (V)dep3.Content;
            var w = (W)dep4.Content;
            method?.Invoke(t, u, v, w);

            return Tuple.Create(t, u, v, w);
        }

        private readonly LinkedList<Dependency> dependencies;

        private class Dependency
        {
            public virtual string Name { get; set; }

            public virtual object Content { get; set; }
        }
    }
}
