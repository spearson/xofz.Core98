namespace xofz.UI
{
    public class UiHelper
    {
        public virtual T Read<T>(
            Ui ui,
            Gen<T> read)
        {
            return UiHelpers.Read(
                ui,
                read);
        }

        public virtual void Write(
            Ui ui,
            Do write)
        {
            UiHelpers.Write(
                ui,
                write);
        }

        public virtual void WriteSync(
            Ui ui,
            Do write)
        {
            UiHelpers.WriteSync(
                ui,
                write);
        }
    }
}