namespace xofz.Framework.Shutdown
{
    using System.Diagnostics;

    public class ProcessKiller
    {
        public virtual void Kill()
        {
            Process.GetCurrentProcess().Kill();
        }

        public virtual void Kill(
            Process p)
        {
            p.Kill();
        }
    }
}
