namespace xofz.Framework
{
    using System.Diagnostics;

    // note: this class requires Windows 2000 or later
    public class KeyboardLoader
    {
        public virtual void Load()
        {
            Process.Start("osk.exe");
        }
    }
}
