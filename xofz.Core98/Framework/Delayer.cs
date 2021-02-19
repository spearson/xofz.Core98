namespace xofz.Framework
{
    public class Delayer
    {
        public virtual void Delay(
            long milliseconds)
        {
            System.Threading.Thread.Sleep(
                (int)milliseconds);
        }

        public virtual void Delay(
            System.TimeSpan delay)
        {
            System.Threading.Thread.Sleep(
                (int)delay.TotalMilliseconds);
        }        
    }
}
