namespace xofz
{
    using System.Collections.Generic;

    public static class EnumHelpers
    {
        public static IEnumerable<T> Iterate<T>()
            where T : System.Enum
        {
            System.Array values;
            try
            {
                values = System.Enum.GetValues(typeof(T));
            }
            catch
            {
                return EnumerableHelpers.Empty<T>();
            }

            return EnumerableHelpers.Cast<T>(values);
        }
    }
}