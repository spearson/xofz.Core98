namespace xofz.Framework
{
    public class VncSettingsHolder
    {
        public virtual string Hostname { get; set; }

        public virtual int Port { get; set; }

        public virtual string Password { get; set; }

        public virtual bool ScaleToWindowSize { get; set; }

        public virtual bool ViewOnly { get; set; }

        public virtual bool MaintainConnection { get; set; }
    }
}
