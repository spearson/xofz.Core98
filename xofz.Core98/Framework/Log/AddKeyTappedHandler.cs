namespace xofz.Framework.Log
{
    using xofz.UI.Log;

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
            var r = this.runner;
            presentEditor?.Invoke();
        }

        public virtual void Handle(
            Do<string> presentEditor,
            string name)
        {
            var r = this.runner;
            presentEditor?.Invoke(name);
        }

        public virtual void Handle(
            Do<string> presentEditor,
            LogUi ui,
            string name)
        {
            var r = this.runner;
            presentEditor?.Invoke(name);
        }

        protected readonly MethodRunner runner;
    }
}