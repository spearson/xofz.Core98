namespace xofz
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using xofz.Framework;
    using xofz.Framework.Materialization;

    public static class MEHelpers
    {
        public static T[] ToArray<T>(MaterializedEnumerable<T> me)
        {
            return EnumerableHelpers.ToArray(me);
        }

        public static T FirstOrDefault<T>(
            MaterializedEnumerable<T> me)
        {
            return EnumerableHelpers.FirstOrDefault(me);
        }

        public static T FirstOrDefault<T>(
            MaterializedEnumerable<T> me,
            Func<T, bool> predicate)
        {
            return EnumerableHelpers.FirstOrDefault(me, predicate);
        }

        public static int Count<T>(
            MaterializedEnumerable<T> me,
            Func<T, bool> predicate)
        {
            return EnumerableHelpers.Count(me, predicate);
        }

        public static MaterializedEnumerable<T> OfType<T>(
            IEnumerable source)
        {
            return new LinkedListMaterializedEnumerable<T>(
                EnumerableHelpers.OfType<T>(source));
        }

        public static MaterializedEnumerable<T> PrivateFieldsOfType<T>(
            object o)
        {
            var ll = new LinkedList<T>();
            foreach (var fieldInfo in o.GetType()
                .GetFields(
                    BindingFlags.Instance | BindingFlags.NonPublic))
            {
                var value = fieldInfo.GetValue(o);
                if (value is T)
                {
                    ll.AddLast((T)value);
                }
            }

            return new LinkedListMaterializedEnumerable<T>(
                ll);
        }

        public static MaterializedEnumerable<T> SafeForEach<T>(
            MaterializedEnumerable<T> me)
        {
            return new LinkedListMaterializedEnumerable<T>(
                EnumerableHelpers.SafeForEach(me));
        }
    }
}
