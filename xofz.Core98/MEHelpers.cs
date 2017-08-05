namespace xofz
{
    using System.Collections;
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
    }
}
