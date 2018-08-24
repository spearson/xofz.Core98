namespace xofz.UI
{
    public class UiReaderWriter
    {
        public virtual T Read<T>(
            Ui ui,
            Func<T> read)
        {
            var value = default(T);
            var r = ui.Root;
            if (r == null)
            {
                return value;
            }

            if (r.InvokeRequired)
            {
                r.Invoke((Action)(() => value = read()), new object[0]);
                return value;
            }

            return read();
        }

        public virtual void Write(
            Ui ui,
            Action write)
        {
            var r = ui.Root;
            if (r == null)
            {
                return;
            }

            if (r.InvokeRequired)
            {
                r.BeginInvoke(write, new object[0]);
                return;
            }

            write();
        }

        public virtual void WriteSync(
            Ui ui,
            Action write)
        {
            var r = ui.Root;
            if (r == null)
            {
                return;
            }

            if (r.InvokeRequired)
            {
                r.Invoke(write, new object[0]);
                return;
            }

            write();
        }
    }
}
