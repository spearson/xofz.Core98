namespace xofz.Framework.UiReaderWriters
{
    using xofz.UI;

    public class NullUiReaderWriter
        : UiReaderWriter
    {
        public override T Read<T>(
            Ui ui,
            Gen<T> read)
        {
            if (read == null)
            {
                return default;
            }

            return read();
        }

        public override void Write(
            Ui ui,
            Do write)
        {
            write?.Invoke();
        }

        public override void WriteSync(
            Ui ui,
            Do write)
        {
            write?.Invoke();
        }
    }
}
