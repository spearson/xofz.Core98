namespace xofz.UI
{
    public class UiReaderWriter
    {
        public virtual T Read<T>(
            Ui ui,
            Gen<T> read)
        {
            if (read == null)
            {
                return default;
            }

            var root = ui?.Root;
            if (root == null)
            {
                try
                {
                    return read();
                }
                catch
                {
                    return default;
                }
            }

            if (root.InvokeRequired)
            {
                T t = default;
                root.Invoke((Do)(() => t = read()), null);
                return t;
            }

            return read();
        }

        public virtual void Write(
            Ui ui,
            Do write)
        {
            if (write == null)
            {
                return;
            }

            var root = ui?.Root;
            if (root == null)
            {
                try
                {
                    write();
                }
                catch
                {
                    // swallow
                }

                return;
            }

            if (root.InvokeRequired)
            {
                root.BeginInvoke(write, null);
                return;
            }

            write();
        }

        public virtual void WriteSync(
            Ui ui,
            Do write)
        {
            if (write == null)
            {
                return;
            }

            var root = ui?.Root;
            if (root == null)
            {
                try
                {
                    write();
                }
                catch
                {
                    // swallow
                }

                return;
            }

            if (root.InvokeRequired)
            {
                root.Invoke(write, null);
                return;
            }

            write();
        }
    }
}
