namespace xofz.Framework.Timers
{
    public class OneOffTimer : Timer
    {
        public OneOffTimer()
        {
            this.shouldReset = false;
        }

        public override bool AutoReset
        {
            get => false;

            set { }
        }
    }
}
