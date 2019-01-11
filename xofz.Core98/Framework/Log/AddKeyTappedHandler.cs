namespace xofz.Framework.Log
{
    public class AddKeyTappedHandler
    {
        public AddKeyTappedHandler(
            MethodWeb web)
        {
            this.web = web;
        }

        public virtual void Handle(
            Do presentEditor)
        {
            presentEditor?.Invoke();
        }

        protected readonly MethodWeb web;
    }
}
