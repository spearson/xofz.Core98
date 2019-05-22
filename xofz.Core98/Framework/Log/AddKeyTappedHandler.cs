namespace xofz.Framework.Log
{
    public class AddKeyTappedHandler
    {
        public AddKeyTappedHandler(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual void Handle(
            Do presentEditor)
        {
            presentEditor?.Invoke();
        }

        protected readonly MethodRunner runner;
    }
}
