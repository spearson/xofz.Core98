namespace xofz.Framework.Login
{
    using System;

    public class SettingsHolder
    {
        public virtual string CurrentPassword { get; set; }

        public virtual TimeSpan Duration { get; set; }
    }
}
