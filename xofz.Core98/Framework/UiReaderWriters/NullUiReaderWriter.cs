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
            return read == null 
                ? default 
                : read();
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
