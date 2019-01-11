namespace xofz.Framework.Login
{
    using System;
    using System.Security;

    public class SettingsHolder
    {
        public virtual SecureString CurrentPassword { get; set; }

        public virtual TimeSpan Duration { get; set; }
    }
}
