namespace xofz
{
    using System;
    using System.Collections.Generic;

    public static class EnumHelpers
    {
        public static IEnumerable<T> Iterate<T>()
            where T : struct
        {
            var ll = new LinkedList<T>();
            try
            {
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    ll.AddLast((T)value);
                }
            }
            catch
            {
                // give up
            }

            foreach (var value in ll)
            {
                yield return value;
            }
        }
    }
}
