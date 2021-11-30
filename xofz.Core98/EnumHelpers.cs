namespace xofz
{
    using System;
    using System.Collections.Generic;

    public static class EnumHelpers
    {
        public static IEnumerable<T> Iterate<T>()
            where T : Enum
        {
            Array values;
            try
            {
                values = Enum.GetValues(typeof(T));
            }
            catch
            {
                return EnumerableHelpers.Empty<T>();
            }

            return EnumerableHelpers.Cast<T>(values);
        }
    }
}
