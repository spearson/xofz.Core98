namespace xofz
{
    using System;
    using System.Collections.Generic;

    public class EnumHelper
    {
        public virtual IEnumerable<T> Iterate<T>()
            where T : Enum
        {
            return EnumHelpers.Iterate<T>();
        }
    }
}
