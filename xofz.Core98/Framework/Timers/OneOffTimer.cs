namespace xofz.Framework.Timers
{
    public class OneOffTimer
        : Timer
    {
        public OneOffTimer()
        {
            this.shouldReset = falsity;
        }

        public override bool AutoReset
        {
            get => falsity;

            set { }
        }

        protected const bool falsity = false;
    }
}