namespace xofz
{
    using System.Collections.Generic;

    public class EnumHelper
    {
        public virtual IEnumerable<T> Iterate<T>()
            where T : System.Enum
        {
            return EnumHelpers.Iterate<T>();
        }
    }
}
