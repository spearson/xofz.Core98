namespace xofz.Framework
{
    using xofz.UI;

    public class UiMaker
    {
        public UiMaker(
            MethodRunner runner)
        {
            this.runner = runner;
        }

        public virtual T Construct<T>(
            Ui marshal,
            Gen<T> make)
        {
            T result = default;
            if (make == null)
            {
                return result;
            }

            var r = this.runner;
            r?.Run<UiReaderWriter>(uiRW =>
            {
                uiRW.WriteSync(
                    marshal,
                    () =>
                    {
                        result = make.Invoke();
                    }
                );
            });

            return result;
        }

        protected readonly MethodRunner runner;
    }
}