namespace xofz.Framework
{
    using System.Diagnostics;

    public class KeyboardLoader
    {
        public virtual void Load()
        {
            Process.Start("osk.exe");
        }
    }
}
