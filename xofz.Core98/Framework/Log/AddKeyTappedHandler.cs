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

        public virtual void Handle(
            Do<string> presentEditor,
            string name)
        {
            presentEditor?.Invoke(name);
        }

        protected readonly MethodRunner runner;
    }
}
