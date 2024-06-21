namespace xofz.Framework.Access
{
    public class SettingsHolder
    {
        public virtual System.TimeSpan DefaultLoginDuration { get; set; }
            = System.TimeSpan.FromMinutes(15);
    }
}