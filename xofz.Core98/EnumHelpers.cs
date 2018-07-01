namespace xofz
{
    using System;
    using System.Collections.Generic;

    public static class EnumHelpers
    {
        public static IEnumerable<T> Iterate<T>()
            where T : struct
        {
            return EnumerableHelpers.Cast<T>(
                Enum.GetValues(typeof(T)));
        }
    }
}
