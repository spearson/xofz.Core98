namespace xofz.Framework.Log
{
    using System;

    public class TimeProvider
    {
        public virtual DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
