namespace xofz.Framework.Timers
{
    public class OneOffTimer : Timer
    {
        public OneOffTimer()
        {
            this.autoReset = false;
        }

        public override bool AutoReset
        {
            get => this.autoReset;

            set { }
        }
    }
}
