namespace xofz.Framework.Login
{
    using System.Security;

    public class SettingsHolder
    {
        public virtual SecureString CurrentPassword { get; set; }

        public virtual System.TimeSpan Duration { get; set; }
    }
}